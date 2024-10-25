using System.Text.Json.Serialization;

namespace ImagesGenerator.WebPortal.ApiControllers
{
    [ApiController]
    public class ImagesGeneratorController : ControllerBase
    {
        private readonly AppScope _appScope;

        public ImagesGeneratorController(AppScope appScope)
        {
            _appScope = appScope;
        }

        [HttpPost("/api/images-generator/submit-order")]
        public IActionResult SubmitOrder([FromBody] FormDataModel formData)
        {
            // TODO: add validation

            var orderData = _appScope.CreateOrder(description: formData.BusinessDescription,
                color: formData.SelectedColor,
                area: formData.SelectedArea);

            return new JsonResult(new ResponseData
            {
                Success = true,
                RedirectUrl = $"/payment/{orderData.OrderUid}" // TODO: Use UrlExtensions
            });
        }

        public class FormDataModel
        {
            [JsonPropertyName("businessDescription")]
            public string BusinessDescription { get; set; } = "";

            [JsonPropertyName("selectedColor")]
            public string SelectedColor { get; set; } = "";

            [JsonPropertyName("selectedArea")]
            public string SelectedArea { get; set; } = "";
        }

        public class ResponseData
        {
            public bool Success { get; set; }
            public string RedirectUrl { get; set; } = "";
        }
    }
}
