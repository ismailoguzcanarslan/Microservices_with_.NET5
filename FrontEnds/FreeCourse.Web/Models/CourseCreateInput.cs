using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    [Display]
    public class CourseCreateInput
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Feature")]
        public FeatureViewModel Feature { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public IFormFile PhotoFormFile { get; set; }
        public string Picture { get; set; }
    }
}
