using Microsoft.ML;
using MLHouseTaxiPredictor.Models;
using Microsoft.ML.Transforms;
using Microsoft.ML.Trainers.FastTree;

namespace MLHouseTaxiPredictor.Services
{
    public class HousingModelService: IHousingModelService
    {
        private readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "MLHousingModel.zip");
        private readonly MLContext _mlContext;
        private readonly ILogger<HousingModelService> _logger;
        private ITransformer _trainedModel;

        public HousingModelService(ILogger<HousingModelService> logger, MLContext mlContext)
        {
            _mlContext = mlContext;
            _logger = logger;
            if (File.Exists(_modelPath))
            {
                _trainedModel = _mlContext.Model.Load(_modelPath, out _);
            }
        }

        public void TrainModel(string dataPath)
        {
            var dataView = _mlContext.Data.LoadFromTextFile<HousingData>(dataPath, hasHeader: true, separatorChar: ',');

            var bestR2 = double.MinValue;
            ITransformer bestModel = null;

            DataOperationsCatalog.TrainTestData dataSplit = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            IDataView trainingData = dataSplit.TrainSet;
            IDataView testData = dataSplit.TestSet;

            var hyperparameterOptions = new[]
            {
                new { LearningRate = 0.1, NumLeaves = 10, MinDataPerLeaf = 5 },
                new { LearningRate = 0.2, NumLeaves = 20, MinDataPerLeaf = 10 },
                new { LearningRate = 0.3, NumLeaves = 30, MinDataPerLeaf = 15 },
            };

            foreach (var hp in hyperparameterOptions)
            {
                var pipeline = _mlContext.Transforms.Concatenate("Features",
                    nameof(HousingData.crim), nameof(HousingData.zn), nameof(HousingData.indus),
                    nameof(HousingData.chas), nameof(HousingData.nox), nameof(HousingData.rm),
                    nameof(HousingData.age), nameof(HousingData.dis), nameof(HousingData.rad),
                    nameof(HousingData.tax), nameof(HousingData.ptratio), nameof(HousingData.b),
                    nameof(HousingData.lstat)) 
                    .Append(_mlContext.Transforms.ReplaceMissingValues("Features", replacementMode: MissingValueReplacingEstimator.ReplacementMode.Mean))
                    .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                    .Append(_mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options
                    {
                        LabelColumnName = "medv",
                        FeatureColumnName = "Features",
                        LearningRate = (float)hp.LearningRate,
                        NumberOfLeaves = hp.NumLeaves,
                        MinimumExampleCountPerLeaf = hp.MinDataPerLeaf
                    }));

                var model = pipeline.Fit(trainingData);

                var metrics = _mlContext.Regression.CrossValidate(dataView, pipeline, numberOfFolds: 5, labelColumnName: "medv");

                var avgR2 = metrics.Average(m => m.Metrics.RSquared);

                if (avgR2 > bestR2)
                {
                    bestR2 = avgR2;
                    bestModel = model;
                }
            }

            _trainedModel = bestModel;
            Evaluate(testData);
            _mlContext.Model.Save(_trainedModel, dataView.Schema, _modelPath);
        }

        private void Evaluate(IDataView testData)
        {
            var predictions = _trainedModel.Transform(testData);
            var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "medv");

            if (float.IsNaN((float)metrics.RSquared) || metrics.RSquared < 0)
            {
                _logger.LogWarning("Model's R^2 is NaN or negative, indicating potential issues with training data or pipeline.");
            }

            _logger.LogInformation($"R^2 for Boston Housing: {metrics.RSquared:0.##}");
            _logger.LogInformation($"RMS for Boston Housing: {metrics.RootMeanSquaredError:#.##}");
        }

        private void Evaluate(IDataView data, IEstimator<ITransformer> pipeline)
        {
            var cvResults = _mlContext.Regression.CrossValidate(data, pipeline, numberOfFolds: 5, labelColumnName: "medv");
            var metrics = cvResults.Select(r => r.Metrics);

            var avgR2 = metrics.Average(m => m.RSquared);
            var avgRmse = metrics.Average(m => m.RootMeanSquaredError);

            _logger.LogInformation($"Cross-validated metrics for Boston Housing: R^2: {avgR2:0.##}, RMSE: {avgRmse:0.##}");
        }

        public float Predict(HousingDataPrediction input)
        {
            try
            {
                var predictionEngine = _mlContext.Model.CreatePredictionEngine<HousingDataPrediction, HousingPrediction>(_trainedModel);
                return predictionEngine.Predict(input).Score;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during prediction.");
                throw;
            }
        }
    }
}
