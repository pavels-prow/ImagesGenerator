using ImagesGenerator.WebPortal.DataObjects;
using Microsoft.Extensions.Caching.Memory;

namespace ImagesGenerator.WebPortal
{
    public class AppScope
	{
        private readonly IMemoryCache _memoryCache;

        public AppScope(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public OrderData CreateOrder(string description, string color, string area)
        {
            var orderData = new OrderData(description: description, color: color, area: area);
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(4));
            _memoryCache.Set(orderData.OrderUid, orderData, cacheEntryOptions);
            return orderData;
        }

        public OrderData? GetOrderData(string orderUid)
        {
            if (string.IsNullOrEmpty(orderUid)) { return null; }
            if (!_memoryCache.TryGetValue(orderUid, out OrderData? result)) { return null; }
            return result;
        }

        public void SetOrderData(OrderData orderData)
        {

        }
    }
}