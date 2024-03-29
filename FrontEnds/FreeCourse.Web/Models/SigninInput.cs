﻿using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    [Display]
    public class SigninInput
    {
        [Required]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsReMember { get; set; }
    }
}
