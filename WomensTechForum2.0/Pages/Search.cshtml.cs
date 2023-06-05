using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Helpers;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages
{
    public class SearchModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;

        public SearchModel(Data.WomensTechForum2_0Context context)
        {
            _context = context;
  
        }
        public List<Post> PostsSearch { get; set; }
        public List<PostThread> PostThreadsSearch { get; set; }
        public List<SubCategory> SubCategoriesSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public async Task<IActionResult> OnGet(string searchString)
        {

            PostsSearch = await _context.Post.Where(p => p.Header.Contains(searchString) || p.Text.Contains(searchString)).ToListAsync();
            PostThreadsSearch = await _context.PostThread.Where(p => p.Text.Contains(searchString)).ToListAsync();
            SubCategoriesSearch = await _context.SubCategory.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString)).ToListAsync();

            return Page();

        }
    }
}
