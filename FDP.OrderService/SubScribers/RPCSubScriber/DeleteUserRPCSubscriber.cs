using FDP.OrderService.Data;
using RawRabbit;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using FDP.OrderService.MessageDirectory.Message;
using FDP.OrderService.MessageDirectory.Response;
using RawRabbit.Context;


namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class DeleteUserRPCSubscriber : FDP.MessageService.Interface.IResponder
    {
        protected readonly IBusClient Bus;

        public DeleteUserRPCSubscriber(IBusClient bus)
        {
            this.Bus = bus;
        }

        public async Task<DeleteOrder> Response(MessageDirectory.Request.DeleteOrder request, MessageContext context)
        {
            DeleteOrder response = new DeleteOrder();

            using (OrderDataContext dataContext = new OrderDataContext())
            {
                var order = await dataContext.Orders.SingleOrDefaultAsync(p => p.Id == request.OrderId);
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
               
                dataContext.Orders.Remove(order);
                dataContext.SaveChanges();

                await Bus.PublishAsync(orderDeleted);
            }

            return response;
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<MessageDirectory.Request.DeleteOrder, DeleteOrder>(this.Response);
        }
    }
}
