using System;
using System.Linq;
using FDP.OrderService.Data;
using System.Data.Entity;
using RawRabbit;
using System.Threading.Tasks;
using FDP.MessageService.Interface;
using FDP.OrderService.MessageDirectory.Message;
using FDP.OrderService.MessageDirectory.Request;
using FDP.OrderService.MessageDirectory.Shared;
using FDP.OrderService.MessageDirectory.Shared.Enum;
using RawRabbit.Context;
using Product = FDP.OrderService.MessageDirectory.Shared.Product;
using ProductInfo = FDP.OrderService.MessageDirectory.Request.ProductInfo;
namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class OrderConfirmedPubSubscriber :  IResponder
    {
        protected readonly IBusClient Bus;

        public OrderConfirmedPubSubscriber(IBusClient bus)
        { 
            this.Bus = bus;
        }

        /// <summary>
        /// Message to recive a Confirmed Order , Calculate the price and let 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns> 
        public async Task Consume(OrderConfirmed message,MessageContext context)
        { 
            using (OrderDataContext dataContext = new OrderDataContext())
            {
                Data.Model.Order order = dataContext.Orders.Include(p=>p.Products).SingleOrDefault(p => p.Id == message.Id);
                if (order == null)
                {
                    Exception ex = new Exception("Order not found");
                    ex.Data.Add("OrderId", message.Id);
                    throw ex;
                }
                               
                var calcualtePrice = new CalculatePrice()
                {
                    Products = order.Products.Select(p => new Product()
                    {
                        ProductId = p.Id,
                        Quantity = p.Quantity
                    }).ToList()
                };

                //Wait for resolve Notified bug in library ticket id #56487 for adding GUID in exchange
                //var calculatedPrice = await Bus.RequestAsync<CalculatePrice, MessageDirectory.Response.CalculatePrice>(calcualtePrice);
                //order.Amount = calculatedPrice.TotalAmount;
                //dataContext.SaveChanges();

                var products = order.Products.Select(o => new Product() { ProductId = o.ProductId, Quantity = o.Quantity }).ToList();

                var reqProductionInfos = new ProductInfo()
                {
                    Products = products
                };

               //var resultInfo = await Bus.RequestAsync<ProductInfo, MessageDirectory.Response.ProductInfo>(reqProductionInfos);
                 OrderReadyToDeliver orderReady = new OrderReadyToDeliver()
                {
                    Address = order.Address,
                    Amount = order.Amount,
                    City = order.City,
                    CreateDate = order.CreateDate,
                    DeliveryType = (DeliveryType)order.DeliveryType,
                    Email = order.Email,
                    PayedAmount = order.Amount,
                    PhoneNumber = order.PhoneNumber,
                    //ProductsToPrepare = resultInfo.Products.Select(p => new ProductToPrepare()
                    //{
                    //    ProductName = p.ProductName,
                    //    Quantity = p.Quantity
                    //}).ToList()

                };

                await Bus.PublishAsync(orderReady);
            }
        }

        public void Subscribe()
        { 
            Bus.SubscribeAsync<OrderConfirmed>(Consume);
        }
    }
}
