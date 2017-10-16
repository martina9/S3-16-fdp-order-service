using System.Collections.Generic;
using FDP.OrderService.MessageDirectory.Shared;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Request
{  
    [Exchange(Type = ExchangeType.Direct, Name = "rpc.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Request.ProductInfo")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Request.ProductInfo", PrefetchCount = 1)]
    public class ProductInfo
    {
        public ProductInfo()
        {
            Products = new List<Product>(); 
        }
        public List<Product> Products { get; set; }
    }
}
