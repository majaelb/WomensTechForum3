using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Helpers;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages.Admin
{
    public class ReportedPostModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;
        private readonly ForumManager _forumManager;
        private readonly AdminManager _adminManager;

        public List<Models.Post> ReportedPosts { get; set; }
        public List<Models.PostThread> ReportedPostThreads { get; set; }
        public List<WomensTechForum2_0User> Users { get; set; }


        public ReportedPostModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager, ForumManager forumManager,AdminManager adminManager)
        {
            _context = context;
            _userManager = userManager;
            _forumManager = forumManager;
            _adminManager = adminManager;
        }

        public async Task<IActionResult> OnGetAsync(int changeId, int deleteId, int changePTId, int deletePTId)
        {
            ReportedPosts = _context.Post.Where(p => p.Offensive == true).ToList();
            ReportedPostThreads = _context.PostThread.Where(p => p.Offensive == true).ToList();
            Users = await _userManager.Users.ToListAsync();


            if (deleteId != 0)
            {

                string url = await _forumManager.DeleteObject<Post>(deleteId);

                return RedirectToPage("./ReportedPost");
            }

            if (changeId != 0)
            {
                await _adminManager.MarkAsNotOffensive<Post>(changeId);
                return RedirectToPage("./ReportedPost");
            }

            if (deletePTId != 0)
            {
                
                string url = await _forumManager.DeleteObject<PostThread>(deletePTId);

                return RedirectToPage("./ReportedPost");
            }

            if (changePTId != 0)
            {
                await _adminManager.MarkAsNotOffensive<PostThread>(changePTId);
                return RedirectToPage("./ReportedPost");
            }

            return Page();
        }
    }
}
