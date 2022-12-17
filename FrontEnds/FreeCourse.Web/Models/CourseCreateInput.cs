using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class CourseCreateInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public FeatureViewModel Feature { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
