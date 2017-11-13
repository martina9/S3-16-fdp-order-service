using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{  
    [Exchange(Type = ExchangeType.Direct,  Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.ConfirmOrder",AutoDelete = false, Durable = true)]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.ConfirmOrder")]
    public class ConfirmOrder
    {
        public ConfirmOrder()
        {
            Products = new List<Product>();
        }
         
        [Required]
        public  int UserId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public decimal? Amount { get; set; }
         

        [Required]
        public DeliveryType DeliveryType { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public List<Product> Products { get; set; } 

    }
}