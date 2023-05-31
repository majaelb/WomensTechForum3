using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.AdminScaffold.PostAdmin
{
    public class IndexModel : PageModel
    {
        private readonly WomensTechForum2._0.Data.WomensTechForum2_0Context _context;

        public IndexModel(WomensTechForum2._0.Data.WomensTechForum2_0Context context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Post != null)
            {
                Post = await _context.Post
                .Include(p => p.SubCategory).ToListAsync();
            }
        }
    }
}
