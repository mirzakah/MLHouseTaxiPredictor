using MLHouseTaxiPredictor.Models;

namespace MLHouseTaxiPredictor.Utils
{
    public static class ValidationUtils
    {
        public static bool ValidateHousingData(HousingDataPrediction input, out List<string> validationResults)
        {
            validationResults = new List<string>();

            if (input.crim < 0) validationResults.Add("crim must be non-negative.");
            if (input.zn < 0) validationResults.Add("zn must be non-negative.");
            if (input.indus < 0) validationResults.Add("indus must be non-negative.");
            if (input.chas < 0 || input.chas > 1) validationResults.Add("chas must be 0 or 1.");
            if (input.nox < 0) validationResults.Add("nox must be non-negative.");
            if (input.rm < 0) validationResults.Add("rm must be non-negative.");
            if (input.age < 0) validationResults.Add("age must be non-negative.");
            if (input.dis < 0) validationResults.Add("dis must be non-negative.");
            if (input.rad < 0) validationResults.Add("rad must be non-negative.");
            if (input.tax < 0) validationResults.Add("tax must be non-negative.");
            if (input.ptratio < 0) validationResults.Add("ptratio must be non-negative.");
            if (input.b < 0) validationResults.Add("b must be non-negative.");
            if (input.lstat < 0) validationResults.Add("lstat must be non-negative.");

            return validationResults.Count == 0;
        }
    }
}
