using System;
using System.Collections.Generic;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;

namespace FDP.OrderService.MessageDirectory.Response
{
        public class OrderInfo
        {
            public OrderInfo()
            {
                Products = new List<Product>();
            }
         
        public int Id { get; set; }

        public int UserId { get; set; }
        
        public int RestaurantId { get; set; }

        public DateTime CreateDate { get; set; }
         
        public DeliveryType DeliveryType { get; set; }

        public decimal Amount { get; set; } 

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}