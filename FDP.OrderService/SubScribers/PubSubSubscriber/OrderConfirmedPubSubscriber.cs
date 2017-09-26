using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Message;
using FDP.OrderService.DirectoryMessage.Request;
using FDP.OrderService.DirectoryMessage.Shared;

namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class OrderConfirmedPubSubscriber : IConsume<OrderConfirmed>
    {
        protected readonly IBus Bus;
        protected OrderDataContext context;

        public OrderConfirmedPubSubscriber(IBus bus)
        { 
            this.Bus = bus;
            this.context = new OrderDataContext();
        }

        public OrderConfirmedPubSubscriber(IBus bus, OrderDataContext context)
        {
            this.Bus = bus;
            this.context = context;
        }

        /// <summary>
        /// Message to recive a Confirmed Order , Calculate the price and let 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [AutoSubscriberConsumer(SubscriptionId = "Id")]
        public void Consume(OrderConfirmed message)
        {
            using (context)
            {
                Order order = context.Orders.SingleOrDefault(p => p.Id == message.Id);
                if (order == null)
                {
                    Exception ex = new Exception("Order not found");
                    ex.Data.Add("OrderId", message.Id);
                    throw ex;
                }

               
                var calcualtePrice = new CalculatePrice()
                {
                    Products = order.Products.Select(p => new FDP.OrderService.DirectoryMessage.Shared.Product()
                    {
                        ProductId = p.Id,
                        Quantity = p.Quantity
                    }).ToList()
                };

                var calculatedPrice = Bus.Request<CalculatePrice, FDP.OrderService.DirectoryMessage.Response.CalculatePrice>(calcualtePrice);

                order.Amount = calculatedPrice.TotalAmount;

                context.SaveChanges();


                var products = order.Products.Select(o => new FDP.OrderService.DirectoryMessage.Shared.Product() { ProductId = o.ProductId, Quantity = o.Quantity }).ToList();

                var reqProductionInfos = new ProductInfos()
                {
                    Products = products
                };

                var resultInfo = Bus.Request<ProductInfos,FDP.OrderService.DirectoryMessage.Response.ProductInfos>(reqProductionInfos);

                OrderReadyToDeliver orderReady = new OrderReadyToDeliver()
                {
                    Address = order.Address,
                    Amount = order.Amount,
                    City = order.City,
                    CreateDate = order.ConfirmationDate,
                    DeliveryType = (FDP.OrderService.DirectoryMessage.Shared.Enum.DeliveryType)order.DeliveryType,
                    Email = order.Email,
                    PayedAmount = order.Amount,
                    PhoneNumber = order.PhoneNumber,
                    ProductsToPrepare = resultInfo.Products.Select(p => new ProductToPrepare()
                    {
                        ProductName = p.ProductName,
                        Quantity = p.Quantity
                    }).ToList()

                };

                Bus.Publish(orderReady);
            }
        }
    }
}
