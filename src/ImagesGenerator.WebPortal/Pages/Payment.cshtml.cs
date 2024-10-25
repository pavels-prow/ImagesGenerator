using ImagesGenerator.WebPortal.DataObjects;
using Microsoft.Extensions.Caching.Memory;

namespace ImagesGenerator.WebPortal.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly AppScope _appScope;

        public PaymentModel(AppScope appScope)
        {
            _appScope = appScope;
        }

        public string BusinessDescription { get; private set; } = "";
        public string SelectedColor { get; private set; } = "";
        public string SelectedArea { get; private set; } = "";
        public string SuccessUrl { get; private set; } = "";

        public void OnGet(string? orderUid)
        {
            var orderData = _appScope.GetOrderData(orderUid: orderUid ?? "");
            if (orderData != null)
            {
                BusinessDescription = orderData.Description;
                SelectedColor = orderData.Color;
                SelectedArea = orderData.Area;
                SuccessUrl = Url.Result(orderUid: orderData.OrderUid);
            }
        }
    }
}
