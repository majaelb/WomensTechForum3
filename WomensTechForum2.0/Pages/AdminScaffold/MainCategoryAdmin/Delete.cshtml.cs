using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.AdminScaffold.MainCategoryAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly WomensTechForum2._0.Data.WomensTechForum2_0Context _context;

        public DeleteModel(WomensTechForum2._0.Data.WomensTechForum2_0Context context)
        {
            _context = context;
        }

        [BindProperty]
      public MainCategory MainCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MainCategory == null)
            {
                return NotFound();
            }

            var maincategory = await _context.MainCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (maincategory == null)
            {
                return NotFound();
            }
            else 
            {
                MainCategory = maincategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MainCategory == null)
            {
                return NotFound();
            }
            var maincategory = await _context.MainCategory.FindAsync(id);

            if (maincategory != null)
            {
                MainCategory = maincategory;
                _context.MainCategory.Remove(MainCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
