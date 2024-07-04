namespace MLHouseTaxiPredictor.Models
{
    public class TaxiDataForPrediction
    {
        public float vendor_id { get; set; }
        public float passenger_count { get; set; }
        public float pickup_longitude { get; set; }
        public float pickup_latitude { get; set; }
        public float dropoff_longitude { get; set; }
        public float dropoff_latitude { get; set; }
        public float store_and_fwd_flag { get; set; }
    }
}
