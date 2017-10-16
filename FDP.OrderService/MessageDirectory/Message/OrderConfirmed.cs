using System.ComponentModel.DataAnnotations;
using FDP.MessageService.Interface;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.OrderConfirmed")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.OrderConfirmed", PrefetchCount = 50)]
    public class OrderConfirmed
    {
        [Required]
        public int Id { get; set; }
    }
}
