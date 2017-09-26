using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EasyNetQ;
using FDP.Infrastructure.Responders;
using FDP.OrderService.Data;
using FDP.OrderService.DirectoryMessage.Response;
using System.Linq;
using System.Collections.Generic;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Shared.Enum;

namespace FDP.OrderService.SubScribers.RPCSubScriber
{
    public class OrderListRPCSubscriber : IResponder
    {
        protected readonly IBus Bus;

        public OrderListRPCSubscriber(IBus bus)
        {
            this.Bus = bus;
        }

        public OrderList Response(DirectoryMessage.Request.OrderList request)
        {
            OrderList response = new OrderList();

            using (OrderDataContext context = new OrderDataContext())
            { 
                var orders =context.Orders.Where(p => (request.UserId == null || p.UserId == request.UserId) && (request.RestaurantId == null || p.RestaurantId == request.RestaurantId)).ToList();

                response.Items = orders.Select(o => new OrderInfo() {
                UserId = o.UserId,RestaurantId = o.RestaurantId,Address =o.Address,
                Amount = o.Amount, City = o.City, ConfirmationDate = o.ConfirmationDate,
                DeliveryType =(DeliveryType) o.DeliveryType, Email = o.Email, Id = o.Id,  PhoneNumber = o.PhoneNumber ,
                Products = o.Products.Select(p=>new FDP.OrderService.DirectoryMessage.Shared.Product() {
                ProductId = p.ProductId , Quantity = p.Quantity}).ToList()
                }).ToList(); 
            }

            return response;
        }

        public void Subscribe()
        {
            this.Bus.Respond<DirectoryMessage.Request.OrderList, OrderList>(this.Response);
        }
    }
}
