using Microsoft.ML.Data;

namespace MLHouseTaxiPredictor.Models
{
    public class TaxiPrediction
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
