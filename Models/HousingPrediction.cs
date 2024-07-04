using Microsoft.ML.Data;

namespace MLHouseTaxiPredictor.Models
{
    public class HousingPrediction
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
