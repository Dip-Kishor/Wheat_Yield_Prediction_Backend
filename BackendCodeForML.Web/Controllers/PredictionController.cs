using BackendCodeForML.Models;
using BackendCodeForML.Services;
using BackendCodeForML.Web.Models;
using CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendCodeForML.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }
        //[Authorize(Policy = "SuperAdmin")]

        [HttpPost("GetPredictionByDistrict")]
        public async Task<IActionResult> GetPredictionAsync( PredictionRequestModel request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (request == null || string.IsNullOrEmpty(request.District))
            {
                return BadRequest("The districtName field is required.");
            }

            var serviceResult = await _predictionService.GetPrediction(request.District);

            if(userId == null || userName==null||userRole==null)
            {
                userId = "0";
                userName = "None";
                userRole = "None";
            }
            if (serviceResult.Status != ResultStatus.Ok)
            {
                return StatusCode(500, serviceResult.Message);
            }
           
            var saveData = _predictionService.SaveUserPredictionDetails(userId, userName, request.District,serviceResult.Data.PredictionResult.Prediction);
            return Ok(new
            {
                Message = serviceResult.Message,
                Data = serviceResult.Data
            });
        }
        //[Authorize(Policy = "Admin")]

        [HttpPost("GetPredictionByCustomData")]
        public async Task<IActionResult> GetPredictionByDataAsync(CustomDataVM vm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (vm == null)
            {
                return BadRequest("The information is required.");
            }
            var request = new CustomDataPredictionModel
            {
                Rainfall = vm.Rainfall,
                AvgTemp = vm.AvgTemp,
                RelativeHumidity = vm.RelativeHumidity,
                SoilTemp = vm.SoilTemp,
                Sand = vm.Sand,
                PHLevel = vm.PHLevel,
                Phosphorus = vm.Phosphorus,
                Potassium = vm.Potassium,
                Clay = vm.Clay,
                ProductionArea = vm.ProductionArea,

            };
            var serviceResult = await _predictionService.GetPredictionByCustomData(request);

            if (serviceResult.Status != ResultStatus.Ok)
            {
                return StatusCode(500, serviceResult.Message);
            }
            //var saveData = _predictionService.SaveUserPredictionDetails(userId, userName, request, serviceResult.Data.PredictionResult.Prediction);
            return Ok(new
            {
                Message = serviceResult.Message,
                Data = serviceResult.Data
            });
        }

    }
}
