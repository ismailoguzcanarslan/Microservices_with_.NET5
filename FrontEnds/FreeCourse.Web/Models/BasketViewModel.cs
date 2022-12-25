using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Web.Models
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        private List<BasketItemViewModel> _basketitems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketitems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return _basketitems;
            }
            set
            {
                _basketitems = value;
            }
        }

        public decimal TotalPrice { get => _basketitems.Sum(a => a.GetCurrentPrice); }
        public int? DiscountRate { get; set; }

        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;
        }
    }
}
