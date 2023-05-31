using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;
using static System.Net.WebRequestMethods;

namespace WomensTechForum2._0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;

        public IndexModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Quote> Quotes { get; set; }
        public Quote RandomQuote { get; set; }
        public List<NewsAPI.Models.Article> News { get; set; }

        public List<Post> Posts { get; set; }
        public List<PostThread> PostThreads { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<WomensTechForum2_0User> Users { get; set; }


        public async Task OnGetAsync()
        {
            Quotes = await DAL.QuoteManager.GetAllQuotes();
            News = await DAL.NewsManager.GetNews();
            Posts = await _context.Post.ToListAsync();
            PostThreads = await _context.PostThread.ToListAsync();
            SubCategories = await _context.SubCategory.ToListAsync();
            Users = await _userManager.Users.ToListAsync();

            Random rnd = new Random();
            var randomIndex = rnd.Next(0, Quotes.Count);
            RandomQuote = Quotes[randomIndex];

        }
    }
}