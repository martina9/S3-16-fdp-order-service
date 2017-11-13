using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.MessageDirectory.Message;
using FDP.OrderService.MessageDirectory.Response;
using RawRabbit;
using RawRabbit.Context;
using RawRabbit.Extensions.Client;
using RawRabbit.Extensions.TopologyUpdater;
using FDP.MessageService.Interface;
using RawRabbit.Configuration.Exchange; 

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    /// <summary>
    /// Subscriber receive a message ConfirmOrder where check user and Restaurant add Order 
    /// after that publish a message OrderConfirmed 
    /// </summary>
    public class ConfirmOrderRPCSubscriber : FDP.MessageService.Interface.IResponder
    {
        protected readonly RawRabbit.IBusClient Bus;
        protected OrderDataContext dataContext;

        public ConfirmOrderRPCSubscriber(RawRabbit.IBusClient bus) 
        {
            this.Bus = bus; 
        }

        public ConfirmOrderRPCSubscriber(RawRabbit.IBusClient bus, OrderDataContext dataContext)
        {
            this.Bus = bus;
            this.dataContext = dataContext;
        }

        public async Task<ConfirmOrder> Response(MessageDirectory.Request.ConfirmOrder request, MessageContext context)
        {
            ConfirmOrder response = new ConfirmOrder();
            this.dataContext = DataUtility.GetDataContext(dataContext);
            using (dataContext)
            { 
                var user = await dataContext.Users.SingleOrDefaultAsync(p => p.Id == request.UserId);

                if (user == null)
                {
                    Exception ex = new Exception("User Doesnt not exist");
                    ex.Data.Add("Email",request.UserId); 
                    throw ex;
                }

                var restaurant = await dataContext.Restaurants.SingleOrDefaultAsync(p => p.Id == request.RestaurantId);

                if (restaurant == null)
                {
                    Exception ex = new Exception("Restaurant Doesnt not exist");
                    ex.Data.Add("Id", request.RestaurantId);
                    throw ex;
                }

                var order = new Order()
                {
                    Amount = request.Amount.Value,
                    Address = request.Address,
                    User = user,
                    Restaurant = restaurant,
                    CreateDate = DateTime.Now,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    City = request.City,
                    DeliveryType = (Data.Enum.DeliveryType)request.DeliveryType,
                    Products = request.Products.Select(p => new Product()
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    }).ToList(),     
                };

                dataContext.Orders.Add(order);

                await dataContext.SaveChangesAsync();

                OrderConfirmed orderConfiremd = new OrderConfirmed()
                {
                    Id = order.Id
                };

                response.id = order.Id;

                await Bus.PublishAsync(orderConfiremd);
            }

            return response;
        }

        public void Subscribe()
        { 
            this.Bus.RespondAsync<MessageDirectory.Request.ConfirmOrder, ConfirmOrder>(Response);
        }
    }
     
}
