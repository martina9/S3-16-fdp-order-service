using System;
using System.Configuration;
using System.IO;
using EasyNetQ;
using FDP.OrderService.DirectoryMessage.Message;
using FDP.OrderService.SubScribers.PubSubSubscriber;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using FDP.OrderService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using FDP.OrderService.Data.Model;
using FDP.OrderService.Test.JsonData;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FDP.OrderService.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IBus bus;
        private OrderService.Data.OrderDataContext dataContext;
        private IList<Order> Orders;

        [TestInitialize]
        public void Init()
        {
            var path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(path1, "App_Data"));
            if (bus == null)
            {
                var connectionString = ConfigurationManager.AppSettings["rabbitMQ"];
                if (connectionString == null)
                {
                    throw new Exception("easynetq connection string is missing or empty");
                }

                bus = RabbitHutch.CreateBus(connectionString); 
            }

            // create test data
            this.Orders = JsonLoad.Orders(); 

            // setup DbSet
            var orders = A.Fake<DbSet<Order>>(o => o.Implements(typeof(IQueryable<Order>)).Implements(typeof(IDbAsyncEnumerable<Order>))).SetupData(this.Orders);
           
            // arrange
            this.dataContext = A.Fake<OrderDataContext>();
            A.CallTo(() => dataContext.Orders).Returns(orders);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var message = new  UserCreated();
            message.Email = "test.test@test.com";
            message.Username = "test.test";
            var user = new UserCreatedPubSubSubScriber(bus);
            var result = user.Consume(message);
            result.Wait();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var message = new UserCreated();
            message.Email = "test.test@test.com";
            message.Username = "test.test";
            bus.Publish(message); 
        }
         
        [TestMethod]
        public async Task<dynamic> Get_Price_From_ProductElment(dynamic obj)
        { 
            // act
            var subscriber = new UserCreatedPubSubSubScriber(bus, this.dataContext);
            var priceManager = new PriceManager(this.dataContext);

            var price = await priceManager.GetTotalPrice(obj.Quantity, obj.DistributorCode, obj.SoftwareCode,
                obj.ProductElementCode, obj.DateTime);

            // assert
            return new
            {
                EUR = (double)price.Value.EUR,
                GBP = (double)price.Value.GBP,
                USD = (double)price.Value.USD
            };
        }
    }
}
