using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Security.Claims;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Helpers;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages
{
    public class ForumModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;
        private readonly ForumManager _forumManager;

        public ForumModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager, ForumManager forumManager)
        {
            _context = context;
            _userManager = userManager;
            _forumManager = forumManager;
        }

        public List<WomensTechForum2_0User> Users { get; set; }
        public WomensTechForum2_0User CurrentUser { get; set; }
        public List<MainCategory>? MainCategories { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
        public MainCategory ChosenMainCategory { get; set; }
        public SubCategory ChosenSubCategory { get; set; }

        [BindProperty]
        public Post ChosenPost { get; set; }
        [BindProperty]
        public PostThread ChosenPostThread { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostThread> PostThreads { get; set; }
        public List<LikePost> LikedPosts { get; set; }
        public List<LikePostThread> LikedPostThreads { get; set; }


        [BindProperty]
        public Post NewPost { get; set; }

        [BindProperty]
        public PostThread NewPostThread { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; } //Läggs utanför databas-innehållet för att sparas som en sträng i db längre ner
        
        private readonly DateTimeOffset localTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
        private string url = "";

        public async Task<IActionResult> OnGetAsync(int chosenMainId, int chosenSubId, int chosenPostId, int deleteid, int deletePTid, int changeId, int changePTId, int unlikepostid, int likepostid, int unlikePTid, int likePTid)
        {
            Users = await _userManager.Users.ToListAsync();
            CurrentUser = await _userManager.GetUserAsync(User);
            MainCategories = await _context.MainCategory.ToListAsync();
            SubCategories = await _context.SubCategory.ToListAsync();
            Posts = await _context.Post.ToListAsync();
            PostThreads = await _context.PostThread.ToListAsync();
            LikedPosts = await _context.LikePost.ToListAsync();
            LikedPostThreads = await _context.LikePostThread.ToListAsync();


            if (chosenMainId != 0)
            {
                ChosenMainCategory = MainCategories.FirstOrDefault(c => c.Id == chosenMainId);
            }
            if (chosenSubId != 0)
            {
                ChosenSubCategory = SubCategories.FirstOrDefault(c => c.Id == chosenSubId);
            }
            if (chosenPostId != 0)
            {
                ChosenPost = Posts.FirstOrDefault(c => c.Id == chosenPostId);
            }
            if (deleteid != 0)
            {

                url = await _forumManager.DeleteObject<Post>(deleteid);

                return Redirect(url ?? "./Forum");
            }
            if (deletePTid != 0)
            {
                url = await _forumManager.DeleteObject<PostThread>(deletePTid);
                return Redirect(url ?? "./Forum");

            }
            if (changeId != 0)
            {
                var offensiveObject = await _forumManager.MarkAsOffensive<Post>(changeId);

                if (offensiveObject != null)
                {
                    url = "./Forum?chosenPostId=" + offensiveObject.Id.ToString();
                    return Redirect(url);
                }
            }

            if (changePTId != 0)
            {
                var offensiveObject = await _forumManager.MarkAsOffensive<PostThread>(changePTId);

                if (offensiveObject != null)
                {
                    url = "./Forum?chosenPostId=" + offensiveObject.PostId.ToString();
                    return Redirect(url);
                }
            }

            if (unlikepostid != 0)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    url = await _forumManager.UnlikeObject<LikePost>(unlikepostid, userId);
                }
                return Redirect(url ?? "./Forum");
            }
            if (likepostid != 0)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    url = await _forumManager.LikeObject<LikePost>(likepostid, userId);
                }
                return Redirect(url ?? "./Forum");

            }
            if (unlikePTid != 0)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    url = await _forumManager.UnlikeObject<LikePostThread>(unlikePTid, userId);
                }
                return Redirect(url ?? "./Forum");

            }
            if (likePTid != 0)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    url = await _forumManager.LikeObject<LikePostThread>(likePTid, userId);
                }
                return Redirect(url ?? "./Forum");

            }

            return Page();
        }
        //public async Task<IActionResult> OnGetSearch(string searchString)
        //{
        //    PostsSearch = await _context.Post.Where(p => p.Header.Contains(searchString) || p.Text.Contains(searchString)).ToListAsync();
        //    PostThreadsSearch = await _context.PostThread.Where(p => p.Text.Contains(searchString)).ToListAsync();
        //    SubCategoriesSearch = await _context.SubCategory.Where(p => p.Name.Contains(searchString)).ToListAsync();

        //    return Page();
        //}

        public async Task<IActionResult> OnPostNewPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (NewPost.Header != null && NewPost.Text != null)
            {
                url = await _forumManager.CreateNewPost(NewPost, UploadedImage, userId);
            }
            return Redirect(url);

        }

        public async Task<IActionResult> OnPostNewPostThreadAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (NewPostThread.Text != null)
            {
                url = await _forumManager.CreateNewPostThread(NewPostThread, UploadedImage, userId);
            }
            return Redirect(url);
        }
    }
}
