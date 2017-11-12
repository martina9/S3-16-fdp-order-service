using System;
using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.RestaurantDeleted")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.RestaurantDeleted", PrefetchCount = 50)]
    public class RestaurantDeleted
    {
        [Required]
        public int Id { get; set; } 

        [Required]
        public String Code { get; set; } 
    }
}
