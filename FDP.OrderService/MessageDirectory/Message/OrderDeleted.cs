using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.OrderDeleted")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.OrderDeleted", PrefetchCount = 50)]
    public class OrderDeleted
    {
        [Required]
        public int Id { get; set; }
    }
}
