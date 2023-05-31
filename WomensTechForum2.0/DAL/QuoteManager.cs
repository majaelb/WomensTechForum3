using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.DAL
{
    public class QuoteManager
    {
        public static async Task<List<Models.Quote>> GetAllQuotes()
        {
            List<Models.Quote> quotes = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/");
                var response = await client.GetAsync("/json/quote.json");

                var json = await response.Content.ReadAsStringAsync();
                quotes = JsonConvert.DeserializeObject<List<Quote>>(json);

                return quotes;
            }
        }

    }
}
