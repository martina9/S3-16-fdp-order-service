using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.OrderInfo")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.OrderInfo", PrefetchCount = 1)]
    public class OrderInfo
    {
        [Required]
        public int Id { get; set; } 
    }
}