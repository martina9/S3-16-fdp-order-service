using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using FDP.Infrastructure.Responders;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Message;
using FDP.OrderService.DirectoryMessage.Response;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class ConfirmOrderRPCSubscriber :  IResponder
    {
        protected readonly IBus Bus;

        public ConfirmOrderRPCSubscriber(IBus bus) 
        {
            this.Bus = bus;
        }

        public async Task<ConfirmOrder> Response(DirectoryMessage.Request.ConfirmOrder request)
        {
            ConfirmOrder response = new ConfirmOrder();
             
            using (OrderDataContext context = new OrderDataContext())
            { 
                var user = await context.Users.SingleOrDefaultAsync(p => p.Id == request.UserId);

                if (user != null)
                {
                    Exception ex = new Exception("User Doesnt not exist");
                    ex.Data.Add("Email",request.UserId); 
                    throw ex;
                }

                var order = new Order()
                {
                    Amount = request.Amount.Value,
                    Address = request.Address,
                    UserId = request.UserId,
                    ConfirmationDate = DateTime.Now,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    City = request.City,
                    DeliveryType = (Data.Enum.DeliveryType)request.DeliveryType,
                    Products = request.Products.Select(p => new Product()
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    }).ToList()                    
                };

                context.Orders.Add(order);

                await context.SaveChangesAsync();

                OrderConfirmed orderConfiremd = new OrderConfirmed()
                {
                    Id = order.Id
                };

                await Bus.PublishAsync(orderConfiremd);
            }

            return response;
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<DirectoryMessage.Request.ConfirmOrder, ConfirmOrder>(this.Response);
        }
    }
}
