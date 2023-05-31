using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages.Admin
{
    public class ReportedPostModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;

        public List<Models.Post> ReportedPosts { get; set; }
        public List<Models.PostThread> ReportedPostThreads { get; set; }
        public List<WomensTechForum2_0User> Users { get; set; }


        public ReportedPostModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int changeId, int deleteId, int changePTId, int deletePTId)
        {
            ReportedPosts = _context.Post.Where(p => p.Offensive == true).ToList();
            ReportedPostThreads = _context.PostThread.Where(p => p.Offensive == true).ToList();
            Users = await _userManager.Users.ToListAsync();


            if (deleteId != 0)
            {
                Models.Post post = await _context.Post.FindAsync(deleteId);

                if (post != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + post.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + post.ImageSrc); //Ta bort bilden
                    }
                    _context.Post.Remove(post); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./ReportedPost");//Tillbaka till startsidan
                }
            }

            if (changeId != 0)
            {
                Post offensivePost = await _context.Post.FindAsync(changeId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = false;
                    offensivePost.NoOfReports = 0;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./ReportedPost");
                }
            }

            if (deletePTId != 0)
            {
                Models.PostThread postthread = await _context.PostThread.FindAsync(deletePTId);

                if (postthread != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + postthread.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + postthread.ImageSrc); //Ta bort bilden
                    }
                    _context.PostThread.Remove(postthread); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./ReportedPost");//Tillbaka till startsidan
                }
            }

            if (changePTId != 0)
            {
                PostThread offensivePostThread = await _context.PostThread.FindAsync(changePTId);

                if (offensivePostThread != null)
                {
                    offensivePostThread.Offensive = false;
                    offensivePostThread.NoOfReports = 0;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./ReportedPost");
                }
            }


            return Page();
        }
    }
}
