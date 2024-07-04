using Microsoft.ML.Data;

namespace MLHouseTaxiPredictor.Models
{
    public class HousingData
    {
        [LoadColumn(0), ColumnName("crim")]
        public float crim { get; set; }

        [LoadColumn(1), ColumnName("zn")]
        public float zn { get; set; }

        [LoadColumn(2), ColumnName("indus")]
        public float indus { get; set; }

        [LoadColumn(3), ColumnName("chas")]
        public float chas { get; set; }

        [LoadColumn(4), ColumnName("nox")]
        public float nox { get; set; }

        [LoadColumn(5), ColumnName("rm")]
        public float rm { get; set; }

        [LoadColumn(6), ColumnName("age")]
        public float age { get; set; }

        [LoadColumn(7), ColumnName("dis")]
        public float dis { get; set; }

        [LoadColumn(8), ColumnName("rad")]
        public float rad { get; set; }

        [LoadColumn(9), ColumnName("tax")]
        public float tax { get; set; }

        [LoadColumn(10), ColumnName("ptratio")]
        public float ptratio { get; set; }

        [LoadColumn(11), ColumnName("b")]
        public float b { get; set; }

        [LoadColumn(12), ColumnName("lstat")]
        public float lstat { get; set; }

        [LoadColumn(13), ColumnName("medv")]
        public float medv { get; set; }
    }
}
