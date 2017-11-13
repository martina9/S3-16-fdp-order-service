using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.DeleteOrder")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.DeleteOrder", PrefetchCount = 1)]
    public class DeleteOrder
    { 
        [Required]
        public int OrderId { get; set; }
    }
}