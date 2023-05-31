using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.AdminScaffold.PostAdmin
{
    public class CreateModel : PageModel
    {
        private readonly WomensTechForum2._0.Data.WomensTechForum2_0Context _context;

        public CreateModel(WomensTechForum2._0.Data.WomensTechForum2_0Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Post == null || Post == null)
            {
                return Page();
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
