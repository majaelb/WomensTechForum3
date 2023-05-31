using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.AdminScaffold.SubCategoryAdmin
{
    public class DetailsModel : PageModel
    {
        private readonly WomensTechForum2._0.Data.WomensTechForum2_0Context _context;

        public DetailsModel(WomensTechForum2._0.Data.WomensTechForum2_0Context context)
        {
            _context = context;
        }

      public SubCategory SubCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategory == null)
            {
                return NotFound();
            }

            var subcategory = await _context.SubCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }
            else 
            {
                SubCategory = subcategory;
            }
            return Page();
        }
    }
}
