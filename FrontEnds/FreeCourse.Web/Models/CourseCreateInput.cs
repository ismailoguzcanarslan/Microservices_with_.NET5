using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class CourseCreateInput
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Feature")]
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Photo")]
        public IFormFile PhotoFormFile { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}
