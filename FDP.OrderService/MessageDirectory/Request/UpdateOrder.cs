using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.UpdateOrder")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.UpdateOrder", PrefetchCount = 1)]
    public class UpdateOrder
    {
        public UpdateOrder()
        {
            Products = new List<Product>();
        } 

        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        public DateTime? ConfirmationDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DeliveryType DeliveryType { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Product> Products { get; set; } 

    }
}