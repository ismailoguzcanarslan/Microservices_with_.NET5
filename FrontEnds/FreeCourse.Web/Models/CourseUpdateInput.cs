using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
