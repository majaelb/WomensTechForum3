using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.DAL;
using WomensTechForum2._0.Models;

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

        public async Task<T> MarkAsOffensive<T>(int changeId) where T : class
        {

            T offensiveObject = await _context.Set<T>().FindAsync(changeId);

            if (offensiveObject != null)
            {
                PropertyInfo offensive = typeof(T).GetProperty("Offensive");
                PropertyInfo noOfReportsProp = typeof(T).GetProperty("NoOfReports");

                if (offensive != null && noOfReportsProp != null)
                {
                    offensive.SetValue(offensiveObject, true);
                    int noOfReports = (int)noOfReportsProp.GetValue(offensiveObject);
                    noOfReportsProp.SetValue(offensiveObject, noOfReports + 1);
                    await _context.SaveChangesAsync();
                }
            }

            return offensiveObject;

        }

        public async Task<string> DeleteObject<T>(int objectId) where T : class
        {
            string url = "";

            T obj = await _context.Set<T>().FindAsync(objectId);

            if (obj != null)
            {
                PropertyInfo imageSrcProp = typeof(T).GetProperty("ImageSrc");
                PropertyInfo subCategoryIdProp = typeof(T).GetProperty("SubCategoryId");
                PropertyInfo postIdProp = typeof(T).GetProperty("PostId");

                if (imageSrcProp != null)
                {
                    string imageSrc = imageSrcProp.GetValue(obj).ToString();
                    string imagePath = "./wwwroot/img/" + imageSrc;

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                if (subCategoryIdProp != null)
                {
                    string subCategoryId = subCategoryIdProp.GetValue(obj).ToString();
                    url = "./Forum?chosenSubId=" + subCategoryId;
                }

                if (postIdProp != null)
                {
                    string postId = postIdProp.GetValue(obj).ToString();
                    url = "./Forum?chosenPostId=" + postId;
                }
                _context.Set<T>().Remove(obj);
                await _context.SaveChangesAsync();
            }
            return url;
        }

        public async Task<string> LikeObject<T>(int objectId, string userId) where T : class, new()
        {

            T likeObject = new T();
            PropertyInfo objectIdProperty = typeof(T).GetProperty("PostId") ?? typeof(T).GetProperty("PostThreadId");
            PropertyInfo userIdProperty = typeof(T).GetProperty("UserId");
            string url = string.Empty;
            int postId = 0;

            if (objectIdProperty != null && userIdProperty != null)
            {
                objectIdProperty.SetValue(likeObject, objectId);
                userIdProperty.SetValue(likeObject, userId);

                _context.Set<T>().Add(likeObject);
                await _context.SaveChangesAsync();


                if (typeof(T) == typeof(LikePost))
                {
                    postId = (int)objectIdProperty.GetValue(likeObject);
                    url = "./Forum?chosenPostId=" + postId.ToString();
                }
                else if (typeof(T) == typeof(LikePostThread))
                {
                    var postThread = _context.Set<PostThread>().FirstOrDefault(p => p.Id == objectId);
                    if (postThread != null)
                    {
                        postId = postThread.PostId;
                        url = "./Forum?chosenPostId=" + postId.ToString() + "#" + objectId.ToString();
                    }
                }
            }
            return url;
        }
        public async Task<string> UnlikeObject<T>(int objectId, string userId) where T : class
        {
            string url = string.Empty;
            int postId = 0;

            T likeObject = _context.Set<T>().AsEnumerable().FirstOrDefault(p => GetObjectId(p) == objectId && GetUserId(p) == userId);

            if (likeObject != null)
            {
                _context.Set<T>().Remove(likeObject);
                await _context.SaveChangesAsync();


                if (typeof(T) == typeof(LikePost))
                {
                    postId = GetObjectId(likeObject);
                    url = "./Forum?chosenPostId=" + postId.ToString();
                }
                else if (typeof(T) == typeof(LikePostThread))
                {
                    var postThread = _context.Set<PostThread>().FirstOrDefault(p => p.Id == objectId);
                    if (postThread != null)
                    {
                        postId = postThread.PostId;
                        url = "./Forum?chosenPostId=" + postId.ToString() + "#" + objectId.ToString();
                    }
                }
            }
            return url;
        }

        private int GetObjectId<T>(T obj)
        {
            PropertyInfo objectIdProperty = typeof(T).GetProperty("PostId") ?? typeof(T).GetProperty("PostThreadId");
            if (objectIdProperty != null)
            {
                return (int)objectIdProperty.GetValue(obj);
            }
            return 0;
        }

        private string GetUserId<T>(T obj)
        {
            PropertyInfo userIdProperty = typeof(T).GetProperty("UserId");
            if (userIdProperty != null)
            {
                return userIdProperty.GetValue(obj).ToString();
            }
            return null;
        }
    }
}
