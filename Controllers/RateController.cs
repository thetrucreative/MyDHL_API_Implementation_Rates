using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyDHL_API_Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyDHL_API_Implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly BasicAuthModel _authModel;
        private readonly HttpClient _httpClient;

        public RateController(IOptions<BasicAuthModel> authModel, HttpClient httpClient)
        {
            _authModel = authModel.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://express.api.dhl.com/mydhlapi/test");
        }

        [HttpGet("getDHLRates")]
        public async Task<IActionResult> GetDHLRates([FromQuery] RateDictionaryModel getRateQueryParams)
        {
            //var client = new HttpClient();
            var getRateQueryURL = CreateGetRateQueryString(getRateQueryParams);
            var sandboxRequestUri = $"rates?{getRateQueryURL}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"{sandboxRequestUri}"); 
            request.Headers.Add("accept", "application/json");
            //request.Headers.Add("Message-Reference", "Generate_GUID/UUID_Here");//create a method for UUID generation
            //request.Headers.Add("Message-Reference-Date", "Generate_today_date_here");//create a method for date picker
            request.Headers.Add("Plugin-Name", "MyDHL_API_Implementation");
            request.Headers.Add("Plugin-Version", "1.0");
            request.Headers.Add("Shipping-System-Platform-Name", "MyDHL_API_DotNet_Core_Implementation");
            request.Headers.Add("Shipping-System-Platform-Version", "1.0");
            request.Headers.Add("Webstore-Platform-Name", "MyDHL_API_Webstore");
            request.Headers.Add("Webstore-Platform-Version", "1.0");
            var basicAuthCredentials = GenerateBasicAuthCredentials(_authModel.Username, _authModel.Password);
            request.Headers.Add("Authorization", $"Basic {basicAuthCredentials}");

            try
            {
                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API call failed with status code: {response.StatusCode} and message: {content}");
                }
                return Ok(content);
            }
            catch (Exception ex)
            {

                var errorMessage = $"Unable to run getDHLRates API because: {ex.Message}. Request URI: {sandboxRequestUri}";
                throw new Exception(errorMessage, ex);
            }
        }

        private string GenerateBasicAuthCredentials(string username, string password)
        {
            var basicAuthCredentials = Encoding.UTF8.GetBytes($"{username}:{password}");
            return Convert.ToBase64String(basicAuthCredentials);
        }

        private string CreateGetRateQueryString(RateDictionaryModel getRateQueryParams)
        {
            var getRateQueryURL = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrEmpty(getRateQueryParams.AccountNumber)) getRateQueryURL["accountNumber"] = getRateQueryParams.AccountNumber;
            if (!string.IsNullOrEmpty(getRateQueryParams.OriginCountryCode)) getRateQueryURL["originCountryCode"] = getRateQueryParams.OriginCountryCode;
            if (!string.IsNullOrEmpty(getRateQueryParams.OriginCityName)) getRateQueryURL["originCityName"] = getRateQueryParams.OriginCityName;
            if (!string.IsNullOrEmpty(getRateQueryParams.DestinationCountryCode)) getRateQueryURL["destinationCountryCode"] = getRateQueryParams.DestinationCountryCode;
            if (!string.IsNullOrEmpty(getRateQueryParams.DestinationCityName)) getRateQueryURL["destinationCityName"] = getRateQueryParams.DestinationCityName;
            if (!string.IsNullOrEmpty(getRateQueryParams.Weight)) getRateQueryURL["weight"] = getRateQueryParams.Weight;
            if (!string.IsNullOrEmpty(getRateQueryParams.Length)) getRateQueryURL["length"] = getRateQueryParams.Length;
            if (!string.IsNullOrEmpty(getRateQueryParams.Width)) getRateQueryURL["width"] = getRateQueryParams.Width;
            if (!string.IsNullOrEmpty(getRateQueryParams.Height)) getRateQueryURL["height"] = getRateQueryParams.Height;
            if (!string.IsNullOrEmpty(getRateQueryParams.PlannedShippingDate)) getRateQueryURL["plannedShippingDate"] = getRateQueryParams.PlannedShippingDate;
            if (!string.IsNullOrEmpty(getRateQueryParams.IsCustomsDeclarable)) getRateQueryURL["isCustomsDeclarable"] = getRateQueryParams.IsCustomsDeclarable;
            if (!string.IsNullOrEmpty(getRateQueryParams.UnitOfMeasurement)) getRateQueryURL["unitOfMeasurement"] = getRateQueryParams.UnitOfMeasurement;
            if (!string.IsNullOrEmpty(getRateQueryParams.NextBusinessDay)) getRateQueryURL["nextBusinessDay"] = getRateQueryParams.NextBusinessDay;
            if (!string.IsNullOrEmpty(getRateQueryParams.GetAllValueAddedServices)) getRateQueryURL["getAllValueAddedServices"] = getRateQueryParams.GetAllValueAddedServices;
            if (!string.IsNullOrEmpty(getRateQueryParams.DestinationPostalCode)) getRateQueryURL["destinationPostalCode"] = getRateQueryParams.DestinationPostalCode;
            if (!string.IsNullOrEmpty(getRateQueryParams.RequestEstimatedDeliveryDate)) getRateQueryURL["requestEstimatedDeliveryDate"] = getRateQueryParams.RequestEstimatedDeliveryDate;
            if (!string.IsNullOrEmpty(getRateQueryParams.EstimatedDeliveryDateType)) getRateQueryURL["estimatedDeliveryDateType"] = getRateQueryParams.EstimatedDeliveryDateType;

            return getRateQueryURL.ToString();
        }
    }
}
