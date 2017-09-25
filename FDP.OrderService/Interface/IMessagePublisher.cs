using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;

namespace FDP.OrderService.Interface
{
    public interface IMessagePublisher
    {
        void Publish(dynamic message);

        Task<TResponse> Request<TRequest, TResponse>(TRequest request, Action<TResponse> onResponse)
            where TRequest : class
            where TResponse : class;
    }
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;

        public MessagePublisher(IBus bus, ILogger logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async void Publish(dynamic message)
        {
            try
            {
                 
                    _logger.InfoFormat("Publishing Message: {0}", message);
                   await _bus.Publish(message);
                 
            }
            catch (EasyNetQException ex)
            {
                _logger.Error("Publish Message Failed: ", ex);
            }
        }


        public async Task<TResponse> Request<TRequest, TResponse>(TRequest request, Action<TResponse> onResponse)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                 
                    _logger.InfoFormat("Publishing Request: {0}", request);
              return await _bus.RequestAsync<TRequest,TResponse>(request); 

            }
            catch (EasyNetQException ex)
            {
                _logger.Error("Publish Request Failed: ", ex);
            }
            return null;
        }

        //public void Response<TRequest, TResponse>(Func<TRequest, TResponse> onResponse)
        //    where TRequest : class
        //    where TResponse : class
        //{
        //    try
        //    {
        //        _logger.InfoFormat("Publishing Response: {0}", onResponse);
        //        _bus.Respond(onResponse);
        //    }
        //    catch (EasyNetQException ex)
        //    {
        //        _logger.Error("Respond Failed: ", ex);
        //    }
        //}
    }
}
