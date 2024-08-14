﻿using System.Collections.Generic;
using System.Linq;

namespace Services.Basket.API.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto>  BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }
    }
}
