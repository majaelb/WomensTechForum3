using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
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
                Post post = await _context.Post.FindAsync(deleteid);

                if (post != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + post.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + post.ImageSrc); //Ta bort bilden
                    }
                    string url = "./Forum?chosenSubId=" + post.SubCategoryId.ToString();

                    _context.Post.Remove(post); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return Redirect(url);
                }
            }
            if (deletePTid != 0)
            {
                Models.PostThread postThread = await _context.PostThread.FindAsync(deletePTid);

                if (postThread != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + postThread.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + postThread.ImageSrc); //Ta bort bilden
                    }
                    string url = "./Forum?chosenPostId=" + postThread.PostId.ToString();

                    _context.PostThread.RemoveRange(postThread); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return Redirect(url);
                }
            }
            if (changeId != 0)
            {
                Post offensivePost = await _context.Post.FindAsync(changeId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = true;
                    offensivePost.NoOfReports += 1;
                    await _context.SaveChangesAsync();
                    string url = "./Forum?chosenPostId=" + offensivePost.Id.ToString();
                    return Redirect(url);

                }
            }
            if (changePTId != 0)
            {
                PostThread offensivePost = await _context.PostThread.FindAsync(changePTId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = true;
                    offensivePost.NoOfReports += 1;
                    await _context.SaveChangesAsync();
                    string url = "./Forum?chosenPostId=" + offensivePost.PostId.ToString();
                    return Redirect(url);
                }
            }
            if (unlikepostid != 0)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                LikePost likePost = await _context.LikePost.FirstOrDefaultAsync(p => p.PostId == unlikepostid && p.UserId == userId);

                if (likePost != null)
                {
                    _context.LikePost.Remove(likePost); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    string url = "./Forum?chosenPostId=" + likePost.PostId.ToString();
                    return Redirect(url);
                }
            }
            if (likepostid != 0)
            {
                var likePost = new LikePost()
                {
                    PostId = likepostid,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.LikePost.Add(likePost); //lägg till i listan av likeposts
                await _context.SaveChangesAsync(); //Spara

                string url = "./Forum?chosenPostId=" + likePost.PostId.ToString();
                return Redirect(url);

            }
            if (unlikePTid != 0)
            {
                //Kolla att Usern som är inloggad är samma som usern i likepostthread!
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                LikePostThread likePostThread = await _context.LikePostThread.FirstOrDefaultAsync(p => p.PostThreadId == unlikePTid && p.UserId == userId);

                if (likePostThread != null)
                {
                    _context.LikePostThread.Remove(likePostThread); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    int id = _context.PostThread.FirstOrDefault(p => p.Id == likePostThread.PostThreadId).PostId;
                    string url = "./Forum?chosenPostId=" + id.ToString() + "#" + unlikePTid.ToString();
                    return Redirect(url);
                }
            }
            if (likePTid != 0)
            {
                var likePostThread = new LikePostThread()
                {
                    PostThreadId = likePTid,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.LikePostThread.Add(likePostThread); //lägg till i listan av likeposts
                await _context.SaveChangesAsync(); //Spara

                int id = _context.PostThread.FirstOrDefault(p => p.Id == likePostThread.PostThreadId).PostId;
                string url = "./Forum?chosenPostId=" + id.ToString() + "#"+ likePTid.ToString();
                return Redirect(url);

            }

            //if (likePTid != 0)
            //{
            //    var likePostThread = new LikePostThread()
            //    {
            //        PostThreadId = likePTid,
            //        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            //    };

            //    _context.LikePostThread.Add(likePostThread);
            //    await _context.SaveChangesAsync();

            //    return new JsonResult(new { success = true, message = "Inlägget gillades." });
            //}

            //if (unlikePTid != 0)
            //{
            //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    LikePostThread likePostThread = await _context.LikePostThread.FirstOrDefaultAsync(p => p.PostThreadId == unlikePTid && p.UserId == userId);

            //    if (likePostThread != null)
            //    {
            //        _context.LikePostThread.Remove(likePostThread);
            //        await _context.SaveChangesAsync();

            //        return new JsonResult(new { success = true, message = "Inlägget ogillades." });
            //    }
            //}

            // Om ingen åtgärd utfördes eller om det uppstod ett fel
            //return new JsonResult(new { success = false, message = "Ett fel uppstod." });




            return Page();
        }

        public async Task<IActionResult> OnPostNewPostAsync()
        {
            string fileName = string.Empty;

            if (string.IsNullOrEmpty(NewPost.Header))
            {
                ModelState.AddModelError("NewPost.Header", "Du måste ange en rubrik");
            }
            if (string.IsNullOrEmpty(NewPost.Text))
            {
                ModelState.AddModelError("NewPost.Text", "Du måste skriva en text");
            }

            if (UploadedImage != null)
            {
                fileName = _forumManager.SetFileName(fileName, UploadedImage);
                var file = _forumManager.CreateFile(fileName);
                await _forumManager.SaveFileAsync(file, UploadedImage);
            }
            if (NewPost.Header != null && NewPost.Text != null)
            {
                NewPost.Date = DateTime.Now;
                NewPost.ImageSrc = fileName;
                NewPost.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(NewPost);
                await _context.SaveChangesAsync();
            }

            string url = "./Forum?chosenPostId=" + NewPost.Id.ToString();
            return Redirect(url);

        }

        public async Task<IActionResult> OnPostNewPostThreadAsync()
        {
            string fileName = string.Empty;

            if (string.IsNullOrEmpty(NewPostThread.Text))
            {
                ModelState.AddModelError("NewPostThread.Text", "Du måste skriva en text");
            }
            if (UploadedImage != null)
            {
                fileName = _forumManager.SetFileName(fileName, UploadedImage);
                var file = _forumManager.CreateFile(fileName);
                await _forumManager.SaveFileAsync(file, UploadedImage);
            }

            if (NewPostThread.Text != null)
            {

                NewPostThread.Date = DateTime.Now;
                NewPostThread.ImageSrc = fileName;
                NewPostThread.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(NewPostThread);
                await _context.SaveChangesAsync();
            }

            string url = "./Forum?chosenPostId=" + NewPostThread.PostId.ToString();
            return Redirect(url);
        }
    }
}
