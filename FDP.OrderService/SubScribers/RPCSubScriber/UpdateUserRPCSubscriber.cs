using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using FDP.Infrastructure.Responders;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Response;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class UpdateUserRPCSubscriber : IResponder
    {
        protected readonly IBus Bus;

        public UpdateUserRPCSubscriber(IBus bus) 
        {
            this.Bus = bus;
        }

        public async Task<UpdateOrder> Response(DirectoryMessage.Request.UpdateOrder request)
        {
            UpdateOrder response = new UpdateOrder();

            using (OrderDataContext context = new OrderDataContext())
            {
                var order = await context.Orders.SingleOrDefaultAsync(p => p.Id == request.Id);
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

                context.Products.RemoveRange(order.Products); 
                order.Products.Clear();

                order.Products = request.Products.Select(p => new Product()
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList();

               await context.SaveChangesAsync();

            response.Id = order.Id; 
            return response;
            }  
        }
        public void Subscribe()
        {
            this.Bus.RespondAsync<DirectoryMessage.Request.UpdateOrder, UpdateOrder>(this.Response);
        }
    }
}
