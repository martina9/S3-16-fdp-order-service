using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EasyNetQ;
using FDP.Infrastructure.Responders;
using FDP.OrderService.Data;
using FDP.OrderService.DirectoryMessage.Message;
using FDP.OrderService.DirectoryMessage.Response;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class DeleteUserRPCSubscriber : IResponder
    {
        protected readonly IBus Bus;

        public DeleteUserRPCSubscriber(IBus bus)
        {
            this.Bus = bus;
        }

        public async Task<DeleteOrder> Response(DirectoryMessage.Request.DeleteOrder request)
        {
            DeleteOrder response = new DeleteOrder();

            using (OrderDataContext context = new OrderDataContext())
            {
                var order = await context.Orders.SingleOrDefaultAsync(p => p.Id == request.OrderId);
                if (order == null)
                {
                    Exception ex = new Exception("Delete Order : Does not exist");
                    ex.Data.Add("OrderId", request.OrderId); 
                    throw ex;
                }

                OrderDeleted orderDeleted = new OrderDeleted()
                {
                    Id = order.Id 
                }; 

                response.Id = order.Id; 
               
                context.Orders.Remove(order);
                context.SaveChanges();

                await Bus.PublishAsync(orderDeleted);
            }

            return response;
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<DirectoryMessage.Request.DeleteOrder, DeleteOrder>(this.Response);
        }
    }
}
