using EasyNetQ;

namespace FDP.OrderService.Interface
{
    public abstract class BaseRPC<TRequest, TResponse> : IResponder<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        protected readonly IBus bus;

        protected BaseRPC(IBus bus)
        {
            this.bus = bus;
        }

        public void Subscribe()
        {
            this.bus.Respond<TRequest, TResponse>(this.Response);
        }

        public abstract TResponse Response(TRequest request);
    }
}
