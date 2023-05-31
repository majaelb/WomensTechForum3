using System.ComponentModel.DataAnnotations;


namespace WomensTechForum2._0.Models
{
    public class MainCategory
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Huvudkategori")]
        public string Name { get; set; }
    }
}
