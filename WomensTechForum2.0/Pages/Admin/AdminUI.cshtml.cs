using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages.Admin
{
    
    public class AdminUIModel : PageModel
    {
        public UserManager<WomensTechForum2_0User> _userManager;

        public List<WomensTechForum2_0User> Users { get; set; }
        public List<MainCategory> MainCategories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        [BindProperty]
        public MainCategory NewMainCategory { get; set; }
        [BindProperty]
        public SubCategory NewSubCategory { get; set; }


        private readonly Data.WomensTechForum2_0Context _context;
        private readonly Helpers.AdminManager _adminManager;
        public AdminUIModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager, Helpers.AdminManager adminManager)
        {
            _context = context;
            _userManager = userManager;
            _adminManager = adminManager;
        }

        public async Task<IActionResult> OnGetAsync(int changeMainCatId, int deleteMainCatId, int changeSubCatId, int deleteSubCatId)
        {
            ViewData["MainCategoryId"] = new SelectList(_context.MainCategory, "Id", "Name");

            MainCategories = await _context.MainCategory.ToListAsync();
            SubCategories = await _context.SubCategory.ToListAsync();         
            Users = await _userManager.Users.ToListAsync();
 

            if (changeMainCatId != 0)
            {
                NewMainCategory = MainCategories.FirstOrDefault(c => c.Id == changeMainCatId);
            }
            if (deleteMainCatId != 0)
            {
                await _adminManager.DeleteCategory<MainCategory>(deleteMainCatId);
                return RedirectToPage("./AdminUI");
            }

            if (changeSubCatId != 0)
            {
                NewSubCategory = SubCategories.FirstOrDefault(c => c.Id == changeSubCatId);
            }

            if (deleteSubCatId != 0)
            {
                await _adminManager.DeleteCategory<SubCategory>(deleteSubCatId);
                return RedirectToPage("./AdminUI");
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewMainCategory.Name != null && NewMainCategory.Id != null)
            {
                await _adminManager.SaveCategory(NewMainCategory);
            }

            return RedirectToPage("./AdminUI");
        }

        public async Task<IActionResult> OnPostSubCategoryAsync()
        {
            if (NewSubCategory.Name != null && NewSubCategory.Description != null && NewSubCategory.MainCategoryId != null)
            {
                await _adminManager.SaveSubCategory(NewSubCategory);
            }

            return RedirectToPage("./AdminUI");
        }
    }
}
