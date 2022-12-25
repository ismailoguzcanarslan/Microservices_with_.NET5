using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class CheckoutInfoInput
    {
        [Display(Name = "City")]
        public string Province { get; set; }
        [Display(Name = "County")]
        public string District { get; set; }
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        [Display(Name = "Address Line")]
        public string Line { get; set; }
        [Display(Name = "Name")]
        public string CardName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNo { get; set; }
        [Display(Name = "ExpirationDate")]
        public string Expiration { get; set; }
        [Display(Name = "CCV/CCV2")]
        public string CCV { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }

    }
}
