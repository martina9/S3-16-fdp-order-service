using System;
using System.Collections.Generic;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.OrderReadyToDeliver")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.OrderReadyToDeliver", PrefetchCount = 50)]
    public class OrderReadyToDeliver
    { 
        public OrderReadyToDeliver()
        {
            ProductsToPrepare = new List<ProductToPrepare>();
        }
        public decimal PayedAmount { get; set;}

        public DateTime CreateDate { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public decimal Amount { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual List<ProductToPrepare> ProductsToPrepare { get; set; }
    }
}
