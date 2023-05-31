using System.ComponentModel.DataAnnotations;

namespace WomensTechForum2._0.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subkategori")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string? Description { get; set; }
        public virtual MainCategory? MainCategory { get; set; }
        public int MainCategoryId { get; set; }
    }
}
