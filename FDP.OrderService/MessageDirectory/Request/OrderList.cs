using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;
using System;
namespace FDP.OrderService.MessageDirectory.Request
{
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.OrderList")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.OrderList", PrefetchCount = 1)]
    public class OrderList
    { 
        public int? UserId { get; set; }
         
        public int? RestaurantId { get; set; }
    }
}
