using Microsoft.ML.Data;

namespace MLHouseTaxiPredictor.Models
{
    public class TaxiData
    {
        [LoadColumn(0)]
        public float vendor_id { get; set; }

        [LoadColumn(1)]
        public float passenger_count { get; set; }

        [LoadColumn(2)]
        public float pickup_longitude { get; set; }

        [LoadColumn(3)]
        public float pickup_latitude { get; set; }

        [LoadColumn(4)]
        public float dropoff_longitude { get; set; }

        [LoadColumn(5)]
        public float dropoff_latitude { get; set; }

        [LoadColumn(6)]
        public float store_and_fwd_flag { get; set; }

        [LoadColumn(7)]
        public float trip_duration { get; set; }
    }
}
