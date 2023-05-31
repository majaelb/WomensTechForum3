using Azure;
using NewsAPI;
using NewsAPI.Constants;
using System.Text.Json;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.DAL
{
    public class NewsManager
    {
        public static async Task<List<NewsAPI.Models.Article>> GetNews()
        {
            List<NewsAPI.Models.Article> news = new();
            var newsAPIClient = new NewsApiClient("bbe6005d1e2d430290087505dabf9b4d");
            var articleResponse = newsAPIClient.GetEverything(new NewsAPI.Models.EverythingRequest
            {
                Q = "code AND IT AND tech",
                Language = Languages.EN,
                PageSize = 6
            });
            if (articleResponse.Status == Statuses.Ok)
            {
                news = articleResponse.Articles.ToList();              
            }
            return news;
        }
    }
}
