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
        public AdminUIModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                Models.MainCategory mainCategory = await _context.MainCategory.FindAsync(deleteMainCatId);

                if (mainCategory != null)
                {

                    _context.MainCategory.Remove(mainCategory); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./AdminUI");//Tillbaka till startsidan
                }
            }

            if (changeSubCatId != 0)
            {
                NewSubCategory = SubCategories.FirstOrDefault(c => c.Id == changeSubCatId);
            }
            if (deleteSubCatId != 0)
            {
                Models.SubCategory subCategory = await _context.SubCategory.FindAsync(deleteSubCatId);

                if (subCategory != null)
                {

                    _context.SubCategory.Remove(subCategory); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./AdminUI");//Tillbaka till startsidan
                }
            }


            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewMainCategory.Name != null && NewMainCategory.Id != null)
            {
                await SaveCategory(NewMainCategory);
            }
            //await _context.SaveChangesAsync();
            return RedirectToPage("./AdminUI");
        }

        public async Task<IActionResult> OnPostSubCategoryAsync()
        {
            if (NewSubCategory.Name != null && NewSubCategory.Description != null && NewSubCategory.MainCategoryId != null)
            {
                await SaveSubCategory(NewSubCategory);
            }

            //await _context.SaveChangesAsync();
            return RedirectToPage("./AdminUI");
        }

        public async Task SaveCategory(MainCategory mainCategory)
        {
            var cat = _context.MainCategory.FirstOrDefault(c => c.Id == mainCategory.Id);

            if (cat != null)
            {
                cat.Name = mainCategory.Name;
            }
            else
            {
                _context.Add(mainCategory);
            }
            await _context.SaveChangesAsync();
        }
        public async Task SaveSubCategory(SubCategory subCategory)
        {
            var cat = _context.SubCategory.FirstOrDefault(c => c.Id == subCategory.Id);

            if (cat != null)
            {
                cat.Name = subCategory.Name;
                cat.Description = subCategory.Description;
                cat.MainCategoryId = subCategory.MainCategoryId;
            }
            else
            {
                _context.Add(subCategory);
            }
            await _context.SaveChangesAsync();
        }
    }
}
