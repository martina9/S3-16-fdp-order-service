using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EasyNetQ;
using FDP.Infrastructure.Responders;
using FDP.OrderService.Data;
using FDP.OrderService.DirectoryMessage.Response;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class OrderInfoRPCSubscriber  : IResponder
    {
        protected readonly IBus Bus;

        public OrderInfoRPCSubscriber(IBus bus)
        {
            this.Bus = bus;
        }

        public async Task<OrderInfo> Response(DirectoryMessage.Request.OrderInfo request)
        {
            OrderInfo response = new OrderInfo();

            using (OrderDataContext context = new OrderDataContext())
            {
                var order = await context.Orders.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (order == null)
                {
                    Exception ex = new Exception("Order Object not found");
                    ex.Data.Add("Id", request.Id); 
                    throw ex;
                }

                response.Id = order.Id;
                response.Amount = order.Amount;
                response.ConfirmationDate = order.ConfirmationDate;
                response.UserId = order.UserId;  
            }

            return response;
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<DirectoryMessage.Request.OrderInfo, OrderInfo>(this.Response);
        }
    }
}
