using System.ComponentModel.DataAnnotations;

namespace Course.Web.Models.Catalog
{
    public class CourseCreate
    {
        [Display(Name="Kurs ismi")]
        [Required]
        public string Name { get; set; }
        [Display(Name="Kurs açıklama")]
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        [Display(Name="Kurs fiyat")]
        [Required]
        public decimal Price { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs kategori")]
        [Required]
        public string CategoryId { get; set; }
    }
}
