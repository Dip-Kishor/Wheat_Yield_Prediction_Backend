using Azure;
using BackendCodeForML.Data;
using BackendCodeForML.Models;
using CommonServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendCodeForML.Services
{
    public class PredictionService
    {
        private readonly WYPredictionContext _context;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PredictionService(HttpClient httpClient, IConfiguration configuration, WYPredictionContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;

        }

        public async Task<ServiceResult<PredictionResponseModel>> GetPrediction(string district)
        {
            var model = _context.EDistrict.FirstOrDefault(x=>x.District == district);
            if (model == null)
            {
                return new ServiceResult<PredictionResponseModel>
                {
                    Data = null,
                    Message = "District Not Found",
                    Status = ResultStatus.processError
                };
            }
            var requestModel = new PredictionModel
            {
                Rainfall = model.Rainfall,
                AvgTemp = model.AvgTemp,
                RelativeHumidity = model.RelativeHumidity,
                SoilTemp = model.SoilTemp,
                Sand= model.Sand,
                PHLevel=model.PHLevel,
                Phosohorus = model.Phosphorus,
                Potassium = model.Potassium,
                Clay= model.Clay,
                ProductionArea= model.ProductionArea,
            };


            var flaskApiUrl = _configuration["FlaskApiUrl"];



            try
            {
                var jsonRequest = JsonSerializer.Serialize(requestModel);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{flaskApiUrl}/predict", content);


                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var jsonResponse = JsonSerializer.Deserialize<ResponseModel>(responseContent, options);

                if(jsonResponse == null) 
                {
                    return new ServiceResult<PredictionResponseModel>
                    {
                        Data = null,
                        Message="Failed to get the predicted result",
                        Status=ResultStatus.processError
                    };
                }

                var predictionResponse = new PredictionResponseModel
                {
                    InputData = requestModel,
                    PredictionResult = jsonResponse
                };

                return new ServiceResult<PredictionResponseModel>
                {
                    Data = predictionResponse,
                    Message = "Successfully retrieved the predicted result.",
                    Status = ResultStatus.Ok
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<PredictionResponseModel>
                {
                    Data = null,
                    Message = $"Error communicating with Flask API: {ex.Message}",
                    Status = ResultStatus.processError
                };
            }
        }
    }

}
