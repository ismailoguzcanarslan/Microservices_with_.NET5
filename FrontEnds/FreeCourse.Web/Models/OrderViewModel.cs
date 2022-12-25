using System.Collections.Generic;
using System;

namespace FreeCourse.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        //public Address Address { get; set; } ödeme geçmişinde adres alanına ihtiyaç olmadığı için kapatıldı.
        public string BuyerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
