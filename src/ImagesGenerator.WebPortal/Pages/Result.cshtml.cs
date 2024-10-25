namespace ImagesGenerator.WebPortal.Pages
{
    public class ResultModel : PageModel
    {
        private readonly AppScope _appScope;

        public ResultModel(AppScope appScope)
        {
            _appScope = appScope;
        }

        public string Api1Url { get; set; } = "";
        public string Api2Url { get; set; } = "";

        public IActionResult OnGet(string? orderUid)
        {           
            var orderData = _appScope.GetOrderData(orderUid: orderUid ?? "");
            if (orderData == null)
            {
                return Page(); // Вернуть пустую страницу или ошибку, если данные не найдены
            }

            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            Api1Url = $"{baseUrl}/api/openai-chat/generate-dalle-queries?orderUid={orderUid}";
            Api2Url = $"{baseUrl}/api/openai-images/generate-image?orderUid={orderUid}";

            return Page();
        }        
    }
}
