using MLHouseTaxiPredictor.Models;

namespace MLHouseTaxiPredictor.Services
{
    public interface IHousingModelService
    {
        void TrainModel(string dataPath);
        float Predict(HousingDataPrediction input);
    }
}
