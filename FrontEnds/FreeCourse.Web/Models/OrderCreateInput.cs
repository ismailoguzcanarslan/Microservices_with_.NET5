﻿using System.Collections.Generic;

namespace FreeCourse.Web.Models
{
    public class OrderCreateInput
    {
        public OrderCreateInput()
        {
            OrderItems = new List<OrderItemCreateInput>();
        }

        public string BuyerId { get; set; }
        public List<OrderItemCreateInput> OrderItems { get; set; }
        public AddressCreateInput Address { get; set; }
    }
}
