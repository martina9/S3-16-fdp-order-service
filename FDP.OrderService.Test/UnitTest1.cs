using System;
using System.Configuration;
using System.IO; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawRabbit;
using FDP.MessageService.Installers;
using FDP.OrderService.MessageDirectory.Request;
using FDP.OrderService.SubScribers.RPCSubScriber;
using RawRabbit.Context;

namespace FDP.OrderService.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IBusClient bus;

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

                bus = BusBuilder.CreateMessageBus(); 
            }
        }
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var message = new  UserCreated();
        //    message.Email = "test.test@test.com";
        //    message.Username = "test.test";
        //    var user = new UserCreatedPubSubSubScriber(bus);
        //    var result = user.Consume(message);
        //    result.Wait();
        //}

        [TestMethod]
        public void OrderListMessageTest()
        {
           OrderList orderList = new OrderList();
            orderList.RestaurantId = 1;

            var result = bus.RequestAsync<OrderList, FDP.OrderService.MessageDirectory.Response.OrderList>(orderList).Result;
        }

        [TestMethod]
        public void OrderListConsumeTest()
        {
            OrderList orderList = new OrderList();
            orderList.RestaurantId = 1;
            var sub = new OrderListRPCSubscriber(bus);
            var result = sub.Response(orderList, new MessageContext(){GlobalRequestId = Guid.NewGuid()}).Result;
        }
    }
}
