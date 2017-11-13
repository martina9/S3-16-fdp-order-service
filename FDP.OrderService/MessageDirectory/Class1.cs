using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory
{
    [Exchange(Type = ExchangeType.Direct, Name = "easy_net_q_rpc1")]
    [Queue(Name = "test2", Durable = true, Exclusive = false, AutoDelete = false)]
    [Routing(RoutingKey = "test3", PrefetchCount = 1)]
    public class ValueRequest
    {
        public int id { get; set; }
    }

    public class ValueResponse
    {
        public string Value { get; set; }
    }
}
