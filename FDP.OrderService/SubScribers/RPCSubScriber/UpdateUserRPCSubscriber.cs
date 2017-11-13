using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.MessageService.Interface;
using FDP.OrderService.MessageDirectory.Response;
using RawRabbit.Context;
using RawRabbit;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class UpdateUserRPCSubscriber : IResponder
    {
        protected readonly IBusClient Bus;

        public UpdateUserRPCSubscriber(IBusClient bus) 
        {
            this.Bus = bus;
        }

        public async Task<UpdateOrder> Response(MessageDirectory.Request.UpdateOrder request, MessageContext context)
        {
            UpdateOrder response = new UpdateOrder();

            using (OrderDataContext dataContext = new OrderDataContext())
            {
                var order = await dataContext.Orders.SingleOrDefaultAsync(p => p.Id == request.Id);
                if (order == null)
                {
                    Exception ex = new Exception("Update Order : Does not exist");
                    ex.Data.Add("Id", request.Id);
                    throw ex;
                }

                order.Email = request.Email;
                order.City = request.City;
                order.DeliveryType = (Data.Enum.DeliveryType)request.DeliveryType;
                order.PhoneNumber = request.PhoneNumber;
                order.Address = request.Address;
                order.City = request.City;

                dataContext.Products.RemoveRange(order.Products); 
                order.Products.Clear();

                order.Products = request.Products.Select(p => new Product()
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList();

               await dataContext.SaveChangesAsync();

            response.Id = order.Id; 
            return response;
            }  
        }
        public void Subscribe()
        {
            this.Bus.RespondAsync<MessageDirectory.Request.UpdateOrder, UpdateOrder>(this.Response);
        }
    }
}
