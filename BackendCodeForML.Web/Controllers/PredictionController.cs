using BackendCodeForML.Models;
using BackendCodeForML.Services;
using BackendCodeForML.Web.Models;
using CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCodeForML.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }
        [Authorize(Policy = "SuperAdmin")]

        [HttpPost("GetPrediction")]
        public async Task<IActionResult> GetPredictionAsync( PredictionRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.District))
            {
                return BadRequest("The districtName field is required.");
            }

            var serviceResult = await _predictionService.GetPrediction(request.District);

            if (serviceResult.Status != ResultStatus.Ok)
            {
                return StatusCode(500, serviceResult.Message);
            }

            return Ok(new
            {
                Message = serviceResult.Message,
                Data = serviceResult.Data
            });
        }


    }
}
