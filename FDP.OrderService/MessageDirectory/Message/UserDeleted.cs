using System;
using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.UserDeleted")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.UserDeleted", PrefetchCount = 50)]
    public class UserDeleted
    {
        [Required]
        public int Id { get; set; } 

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }
    }
}
