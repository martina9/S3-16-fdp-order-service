using System.Collections.Generic;
using FDP.OrderService.MessageDirectory.Shared;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{ 
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.ProductService.MessageDirectory:Request.CalculatePrice")]
    [Routing(RoutingKey = "FDP.ProductService.MessageDirectory:Request.CalculatePrice", PrefetchCount = 1)]
    public class CalculatePrice
    {
        public CalculatePrice()
        {
            Products = new List<Product>();
        } 

        public List<Product> Products { get; set; } 

    }
}