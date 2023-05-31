using System.ComponentModel.DataAnnotations;

namespace WomensTechForum2._0.Models
{
    public class PostThread
    {
        public int Id { get; set; }

        [Display(Name = "Inlägg")]
        [Required(ErrorMessage = "Du måste skriva en text")] 
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public bool Offensive { get; set; }
        public int NoOfReports { get; set; }

        [Display(Name = "Bild")]
        public string? ImageSrc { get; set; }
        public string UserId { get; set; }
        public virtual Post? Post { get; set; }
        public int PostId { get; set; }

        public int? PTAnswerId { get; set; }
    }
}
