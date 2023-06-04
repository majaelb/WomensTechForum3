using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Helpers
{
    public class AdminManager
    {

        private readonly Data.WomensTechForum2_0Context _context;
        public AdminManager(Data.WomensTechForum2_0Context context)
        {
            _context = context;

        }
        public async Task MarkAsNotOffensive<T>(int changeId) where T : class
        {
            T offensiveObject = await _context.Set<T>().FindAsync(changeId);

            if (offensiveObject != null)
            {
                PropertyInfo offensive = typeof(T).GetProperty("Offensive");
                PropertyInfo noOfReportsProp = typeof(T).GetProperty("NoOfReports");

                if (offensive != null && noOfReportsProp != null)
                {
                    offensive.SetValue(offensiveObject, false);
                    int noOfReports = 0;
                    noOfReportsProp.SetValue(offensiveObject, noOfReports);
                    await _context.SaveChangesAsync();
                }
            }
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

        public async Task DeleteCategory<T>(int id) where T : class
        {
            var category = await _context.Set<T>().FindAsync(id);

            if (category != null)
            {
                _context.Set<T>().Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
