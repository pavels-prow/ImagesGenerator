using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ImagesGenerator.WebPortal.ApiControllers
{
    // TODO: add authorisation to prevent unauthorized calls from external users. This is server-to-server api indpoint
    [ApiController]
    public class OpenAiChatController : ControllerBase
    {
        private readonly AppScope _appScope;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _endpoint;
        private readonly string _model;

        public OpenAiChatController(AppScope appScope, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _appScope = appScope;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration.GetValue<string>("Integrations:OpenAi:ApiKey")!;
            _endpoint = configuration.GetValue<string>("Integrations:OpenAi:Chat:Endpoint")!;
            _model = configuration.GetValue<string>("Integrations:OpenAi:Chat:Model")!;
        }

        [HttpPost("/api/openai-chat/generate-dalle-queries")]
        public async Task<IActionResult> GenerateDalleQueries(string? orderUid)
        {
            var orderData = _appScope.GetOrderData(orderUid: orderUid ?? "");
            if (orderData == null)
            {
                return BadRequest("Request UID not found or invalid.");
            }
            if (orderData.Api1Running)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Request is already being processed.");
            }
            if (orderData.Api1Completed)
            {
                return Ok(new ResultData
                {
                    Translation = orderData.Translation,
                    Keywords = orderData.Keywords
                });
            }

            orderData.Api1Running = true;
            _appScope.SetOrderData(orderData);


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestBody = GetOpenApiRequestBody(orderData.Description); // Предполагается реализация этого метода.
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                // TODO: add orderData API 1 Error
                return StatusCode((int)response.StatusCode, "Error calling OpenAI API.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<OpenAiResponse>(responseBody)!;
            var resultJson = responseData.Choices[0].Message.Content;
            var resultData = JsonSerializer.Deserialize<ResultData>(resultJson)!;

            orderData.Translation = resultData.Translation;
            orderData.Keywords = resultData.Keywords;

            orderData.Api1Running = false;
            orderData.Api1Completed = true;
            _appScope.SetOrderData(orderData);

            return Ok(resultData);
        }


        #region helpers

        private dynamic GetOpenApiRequestBody(string userText)
        {
            var userContent = GetGptQuery(userText);

            var messages = new List<object>
            {
                new { role = "system", content = "Process the following user text to translate, extract keywords, and generate a response." },
                new { role = "user", content = userContent }
            };

            var requestBody = new
            {
                model = _model,
                messages = messages
            };
            return requestBody;
        }

        private string GetGptQuery(string userText)
        {
            string userTextFiltered = Regex.Replace(userText, @"\s+", " "); // Заменяем все пробельные символы на пробелы
            userTextFiltered = Regex.Replace(userTextFiltered, @"[^\p{L}\p{N}\p{P} ]+", ""); // Удаляем все кроме букв, цифр, пунктуации и пробелов
            userTextFiltered = Regex.Replace(userTextFiltered, @"\s+", " "); // Удаляем повторяющиеся пробелы

            var prompt = @$"
                Please process the text provided within the square brackets.
                Remove any special characters, numbers, and punctuation that do not form part of standard text.
                You are required to translate the text into English,
                extract relevant keywords suitable for visualization and image creation,
                and provide them as an array of strings.
                The result should be returned in JSON format with clearly defined fields for each step. Do not consider the context of past messages.

                Follow these steps:

                1. Eliminate anything outside the square brackets.
                2. Remove all special characters and numbers, leaving only letters and punctuation.
                3. Translate the text into English.
                4. Extract keywords that are most suitable for visualization.
                5. Form a basic array of keywords from the translated text.
                6. Expand the array by using associations and a creative approach to enhance the keywords.

                The JSON structure for the response should look like this:

                {{
                  ""translation"": ""Translated text in English"",
                  ""keywords"": [""keyword1"", ""keyword2"", ""keyword3"", ""...""]
                }}

                UserText: [{userTextFiltered}]
                ";
            return prompt;
        }

        #endregion

        #region nested classes

        public class OpenAiResponse
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = "";

            [JsonPropertyName("object")]
            public string Object { get; set; } = "";

            [JsonPropertyName("created")]
            public long Created { get; set; }

            [JsonPropertyName("model")]
            public string Model { get; set; } = "";

            [JsonPropertyName("choices")]
            public ChoiceData[] Choices { get; set; } = Array.Empty<ChoiceData>();

            [JsonPropertyName("usage")]
            public UsageData Usage { get; set; } = new();

            public class ChoiceData
            {
                [JsonPropertyName("index")]
                public int Index { get; set; }

                [JsonPropertyName("message")]
                public MessageData Message { get; set; } = new();

                [JsonPropertyName("finish_reason")]
                public string FinishReason { get; set; } = "";
            }

            public class MessageData
            {
                [JsonPropertyName("role")]
                public string Role { get; set; } = "";

                [JsonPropertyName("content")]
                public string Content { get; set; } = "";
            }

            public class UsageData
            {
                [JsonPropertyName("prompt_tokens")]
                public int PromptTokens { get; set; }

                [JsonPropertyName("completion_tokens")]
                public int CompletionTokens { get; set; }

                [JsonPropertyName("total_tokens")]
                public int TotalTokens { get; set; }
            }
        }

        public class ResultData
        {
            [JsonPropertyName("translation")]
            public string Translation { get; set; } = "";

            [JsonPropertyName("keywords")]
            public string[] Keywords { get; set; } = Array.Empty<string>();
        }

        #endregion
    }
}