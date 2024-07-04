using MLHouseTaxiPredictor.Models;
using MLHouseTaxiPredictor.Services;
using MLHouseTaxiPredictor.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MLHouseTaxiPredictor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IHousingModelService _modelService;
        private readonly ILogger<HousingController> _logger;

        public HousingController(IHousingModelService modelService, ILogger<HousingController> logger)
        {
            _modelService = modelService;
            _logger = logger;
        }

        [HttpPost("predict")]
        public ActionResult<float> Predict([FromBody] HousingDataPrediction input)
        {
            if (!ValidationUtils.ValidateHousingData(input, out var validationResults))
            {
                _logger.LogWarning("Invalid input data: {ValidationResults}", validationResults);
                return BadRequest(validationResults);
            }

            try
            {
                var prediction = _modelService.Predict(input);
                return Ok(prediction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during prediction.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
