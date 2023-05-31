using System.Text.Json.Serialization;

namespace WomensTechForum2._0.Models
{
    public class Quote
    {
        [JsonPropertyName("citation")]
        public string Citation { get; set; }
    }
}
