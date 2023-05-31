using Microsoft.AspNetCore.Identity;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.DAL;

namespace WomensTechForum2._0.Helpers
{
    public class ForumManager
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;
       
        public ForumManager(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        public string SetFileName(string fileName, IFormFile UploadedImage)
        {
            
            Random rnd = new();
            fileName = rnd.Next(100000).ToString() + UploadedImage.FileName;
            return fileName;
        }
        public string CreateFile(string fileName)
        {
            var file = "./wwwroot/img/" + fileName;

            return file;

        }
        public async Task SaveFileAsync(string filePath, IFormFile file)
        {
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        public bool CheckIfLiked(int postId, string userId)
        {
            var like = _context.LikePost.FirstOrDefault(p => p.PostId == postId && p.UserId == userId);

            return like != null;
        }

        public bool CheckIfPTLiked(int postId, string userId)
        {
            var like = _context.LikePostThread.FirstOrDefault(p => p.PostThreadId == postId && p.UserId == userId);

            return like != null;
        }
    }
}
