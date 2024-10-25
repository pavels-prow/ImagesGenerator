using System.Text.Json.Serialization;

namespace ImagesGenerator.WebPortal.DataObjects
{
    public class OrderData
    {
        [JsonPropertyName("orderUid")]
        public string OrderUid { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("color")]
        public string Color { get; set; } = "";

        [JsonPropertyName("area")]
        public string Area { get; set; } = "";

        [JsonPropertyName("translation")]
        public string Translation { get; set; } = "";

        [JsonPropertyName("keywords")]
        public string[] Keywords { get; set; } = Array.Empty<string>();

        [JsonPropertyName("Images")]
        public string[] Images { get; set; } = Array.Empty<string>();

        [JsonPropertyName("api1Running")]
        public bool Api1Running { get; set; }

        [JsonPropertyName("api1Completed")]
        public bool Api1Completed { get; set; }

        [JsonPropertyName("api2Running")]
        public bool Api2Running { get; set; }

        [JsonPropertyName("api2Completed")]
        public bool Api2Completed { get; set; }

        public OrderData()
        { }

        public OrderData(string description, string color, string area)
        {
            Description = description;
            Color = color;
            Area = area;
        }
    }
}