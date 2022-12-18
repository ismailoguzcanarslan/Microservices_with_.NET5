﻿using System;

namespace FreeCourse.Web.Models
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description; }
        public CategoryViewModel Category { get; set; }
    }
}
