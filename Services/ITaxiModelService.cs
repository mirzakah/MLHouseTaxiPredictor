using MLHouseTaxiPredictor.Models;

namespace MLHouseTaxiPredictor.Services
{
    public interface ITaxiModelService
    {
        void TrainModel(string dataPath);
        float Predict(TaxiDataForPrediction input);
    }
}
