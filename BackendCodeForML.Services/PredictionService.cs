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
            var model = _context.EDistrict.FirstOrDefault(x => x.District == district);
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
                Sand = model.Sand,
                PHLevel = model.PHLevel,
                Phosphorus = model.Phosphorus,
                Potassium = model.Potassium,
                Clay = model.Clay,
                ProductionArea = model.ProductionArea,
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

                if (jsonResponse == null)
                {
                    return new ServiceResult<PredictionResponseModel>
                    {
                        Data = null,
                        Message = "Failed to get the predicted result",
                        Status = ResultStatus.processError
                    };
                }

                var predictionResponse = new PredictionResponseModel
                {
                    DistrictName = district,
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
        public async Task<ServiceResult<PredictionResponseModel>> GetPredictionByCustomData(CustomDataPredictionModel requestModel)
        {
            if (requestModel == null)
            {
                return new ServiceResult<PredictionResponseModel>
                {
                    Data = null,
                    Message = "Input data is required.",
                    Status = ResultStatus.processError
                };
            }

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

                if (jsonResponse == null)
                {
                    return new ServiceResult<PredictionResponseModel>
                    {
                        Data = null,
                        Message = "Failed to get the predicted result.",
                        Status = ResultStatus.processError
                    };
                }
                var inputResponse = new PredictionModel
                {
                    Rainfall = requestModel.Rainfall,
                    AvgTemp = requestModel.AvgTemp,
                    RelativeHumidity = requestModel.RelativeHumidity,
                    SoilTemp = requestModel.SoilTemp,
                    Sand = requestModel.Sand,
                    PHLevel = requestModel.PHLevel,
                    Phosphorus = requestModel.Phosphorus,
                    Potassium = requestModel.Potassium,
                    Clay = requestModel.Clay,
                    ProductionArea = requestModel.ProductionArea,
                };

                var predictionResponse = new PredictionResponseModel
                {
                    InputData = inputResponse,
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
        public async Task<ServiceResult<UserPredictionDetailModel>> SaveUserPredictionDetails(string id,string name,string district, decimal predictionValue)
        {

            if (district != null)
            {
                var model = _context.EDistrict.FirstOrDefault(x => x.District == district);


                var newModel = new UserPredictionDetailModel
                {
                    DistrictId = model.Id,
                    DistrictName = model.District,
                    UserId = int.Parse(id),
                    UserName = name,
                    Rainfall = model.Rainfall,
                    AvgTemp = model.AvgTemp,
                    RelativeHumidity = model.RelativeHumidity,
                    SoilTemp = model.SoilTemp,
                    Sand = model.Sand,
                    PHLevel = model.PHLevel,
                    Phosphorus = model.Phosphorus,
                    Potassium = model.Potassium,
                    Clay = model.Clay,
                    ProductionArea = model.ProductionArea,
                    PredictionResult = predictionValue

                };

                _context.Add(newModel);
                _context.SaveChanges();
            }
            
            return new ServiceResult<UserPredictionDetailModel>
            {
                Data = null,
                Message = "Hello"
            };
        }

        public async Task<ServiceResult<UserPredictionDetailModel>> SaveUserPredictionDetailsCustomData(string id, string name, string district, decimal predictionValue)
        {

            if (district != null)
            {
                var model = _context.EDistrict.FirstOrDefault(x => x.District == district);


                var newModel = new UserPredictionDetailModel
                {
                    DistrictId = model.Id,
                    DistrictName = model.District,
                    UserId = int.Parse(id),
                    UserName = name,
                    Rainfall = model.Rainfall,
                    AvgTemp = model.AvgTemp,
                    RelativeHumidity = model.RelativeHumidity,
                    SoilTemp = model.SoilTemp,
                    Sand = model.Sand,
                    PHLevel = model.PHLevel,
                    Phosphorus = model.Phosphorus,
                    Potassium = model.Potassium,
                    Clay = model.Clay,
                    ProductionArea = model.ProductionArea,
                    PredictionResult = predictionValue

                };

                _context.Add(newModel);
                _context.SaveChanges();
            }

            return new ServiceResult<UserPredictionDetailModel>
            {
                Data = null,
                Message = "Hello"
            };
        }
    }

}
