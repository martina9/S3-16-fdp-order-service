namespace FDP.OrderService.Interface
{
    public interface IResponder<in TRequest, out TResponse>
       where TRequest : class
       where TResponse : class
    {
        void Subscribe();

        TResponse Response(TRequest request);
    }
}
