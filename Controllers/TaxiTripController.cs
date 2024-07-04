using Microsoft.AspNetCore.Mvc;
using MLHouseTaxiPredictor.Services;
using MLHouseTaxiPredictor.Models;

namespace TaxiTripPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly ITaxiModelService _modelService;
        private readonly ILogger<TaxiController> _logger;

        public TaxiController(ITaxiModelService modelService, ILogger<TaxiController> logger)
        {
            _modelService = modelService;
            _logger = logger;
        }

        [HttpPost("predict")]
        public ActionResult<float> Predict([FromBody] TaxiDataForPrediction input)
        {
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
