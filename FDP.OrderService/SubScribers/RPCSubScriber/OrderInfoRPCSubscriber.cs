using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FDP.OrderService.Data; 
using FDP.OrderService.MessageDirectory.Response;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;
using RawRabbit;
using RawRabbit.Context;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class OrderInfoRPCSubscriber  :  FDP.MessageService.Interface.IResponder
    {
        protected readonly IBusClient Bus;

        public OrderInfoRPCSubscriber(IBusClient bus)
        {
            this.Bus = bus;
        }

        public async Task<OrderInfo> Response(MessageDirectory.Request.OrderInfo request, MessageContext context)
        {
            

            using (OrderDataContext dataContext = new OrderDataContext())
            {
                var order = await dataContext.Orders.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (order == null)
                {
                    Exception ex = new Exception("Order Object not found");
                    ex.Data.Add("Id", request.Id); 
                    throw ex;
                }
                OrderInfo response = new OrderInfo
                {
                    Id = order.Id,
                    Amount = order.Amount,
                    CreateDate = order.CreateDate,
                    UserId = order.User.Id,
                    RestaurantId = order.Restaurant.Id,
                    PhoneNumber = order.PhoneNumber,
                    Email = order.Email,
                    Address = order.Address,
                    DeliveryType = (DeliveryType) order.DeliveryType,
                    City = order.City,
                    Products = order.Products.Select(p=>new Product()
                    {
                        Quantity = p.Quantity,
                        ProductId = p.ProductId
                    }).ToList()
                };

                return response;
            }

            
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<MessageDirectory.Request.OrderInfo, OrderInfo>(this.Response);
        }
    }
}
