using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FDP.OrderService.Data;
using FDP.MessageService.Interface;
using FDP.OrderService.MessageDirectory.Response;
using RawRabbit;
using RawRabbit.Context;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class OrderListRPCSubscriber  : IResponder
    {
        protected readonly IBusClient Bus;

        public OrderListRPCSubscriber(IBusClient bus)
        {
            this.Bus = bus;
        }

        public async Task<OrderList> Response(MessageDirectory.Request.OrderList request, MessageContext context)
        {
            OrderList response = new OrderList();

            using (OrderDataContext dataContext = new OrderDataContext())
            {
                var orders = await dataContext.Orders.Where(p => (request.UserId == null || p.User.Id == request.UserId) && (request.RestaurantId == null || p.Restaurant.Id == request.RestaurantId)).ToListAsync();

                response.Items = orders.Select(p => new Order()
                {
                    UserId = p.User.Id, Id = p.Id, PhoneNumber = p.PhoneNumber, Email = p.Email,Address = p.Address,
                    Amount = p.Amount,City = p.City,CreateDate = p.CreateDate,DeliveryType = (DeliveryType)p.DeliveryType,Products = p.Products.Select(o=> new Product()
                    {
                        ProductId = o.ProductId,
                        Quantity = o.Quantity
                    }).ToList()
                }).ToList();
            }
            return response;
        }

        public void Subscribe()
        {
            this.Bus.RespondAsync<MessageDirectory.Request.OrderList, OrderList>(this.Response);
        }
    }
}
