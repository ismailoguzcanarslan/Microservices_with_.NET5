﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeCourse.Web.Models
{
    public class ServiceAppSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi Photo { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Discount { get; set; }
        public ServiceApi Payment { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
