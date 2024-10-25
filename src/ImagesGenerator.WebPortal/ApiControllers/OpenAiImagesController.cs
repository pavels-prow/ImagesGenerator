using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImagesGenerator.WebPortal.DataObjects;

namespace ImagesGenerator.WebPortal.ApiControllers
{
    // TODO: add authorisation to prevent unauthorized calls from external users. This is server-to-server api indpoint
    [ApiController]
    public class OpenAiImagesController : ControllerBase
    {
        private readonly AppScope _appScope;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _endpoint;

        public OpenAiImagesController(AppScope appScope, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _appScope = appScope;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration.GetValue<string>("Integrations:OpenAi:ApiKey")!;
            _endpoint = configuration.GetValue<string>("Integrations:OpenAi:Images:Endpoint")!;
        }

        [HttpPost("/api/openai-images/generate-image")]
        public async Task<IActionResult> GenerateImage(string? orderUid)
        {
            var orderData = _appScope.GetOrderData(orderUid: orderUid ?? "");
            if (orderData == null || !orderData.Api1Completed)
            {
                return BadRequest("Request UID not found or invalid.");
            }
            if (orderData.Api2Running)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Request is already being processed.");
            }
            if (orderData.Api2Completed)
            {
                return Ok(new ResultData
                {
                    Images = orderData.Images
                });
            }

            orderData.Api2Running = true;
            _appScope.SetOrderData(orderData);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestBody = new RequestData
            {
                Model = "dall-e-2",
                Prompt = GetPrompt(orderData), // Replace with actual property if different
                N = 4, // Number of images to generate
                Size = "256x256" // Image size 1024x1024
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                // TODO: add orderData API 1 Error
                return StatusCode((int)response.StatusCode, "Error calling OpenAI API.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<ResponseData>(responseBody)!;
            var resultData = new ResultData
            {
                Images = responseData.Data.Select(d => d.Url).ToArray() // Extract the image URLs
            };

            orderData.Images = resultData.Images;

            orderData.Api2Running = false;
            orderData.Api2Completed = true;
            _appScope.SetOrderData(orderData);

            return Ok(resultData);
        }

        #region helper

        public static string GetPrompt(OrderData orderData)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(orderData.Area) ||
                orderData.Keywords == null ||
                orderData.Keywords.Length == 0 ||
                string.IsNullOrWhiteSpace(orderData.Color))
            {
                throw new ArgumentException("Area, color, and keywords must be provided to generate a prompt.");
            }

            // Join keywords into a comma-separated string, assuming keywords are simple words or short phrases
            string keywordsString = string.Join(", ", orderData.Keywords);

            // Use HEX color code directly in the prompt
            string hexColor = orderData.Color.StartsWith("#") ? orderData.Color : "#" + orderData.Color;

            // Generate the prompt with all elements
            string prompt = $"Create a photorealistic image that represents the business area of {orderData.Area} with a white background and an accent in the color {hexColor}. " +
                            $"The image should include the following elements: {keywordsString}. " +
                            $"The design should be professional and convey the business concept clearly, " +
                            $"allowing a person to immediately understand the business area and its associated keywords.";

            return prompt;
        }



        #endregion

        #region nested classes

        public class RequestData
        {
            [JsonPropertyName("model")]
            public string Model { get; set; } = "";

            [JsonPropertyName("prompt")]
            public string Prompt { get; set; } = "";

            [JsonPropertyName("n")]
            public int N { get; set; } = 1;

            [JsonPropertyName("size")]
            public string Size { get; set; } = "1024x1024";
        }

        public class ResponseData
        {
            [JsonPropertyName("created")]
            public long Created { get; set; }

            [JsonPropertyName("data")]
            public ImageData[] Data { get; set; } = Array.Empty<ImageData>();

            public class ImageData
            {
                [JsonPropertyName("url")]
                public string Url { get; set; } = "";
            }
        }

        public class ResultData
        {
            [JsonPropertyName("images")]
            public string[] Images { get; set; } = Array.Empty<string>();
        }

        #endregion
    }
}