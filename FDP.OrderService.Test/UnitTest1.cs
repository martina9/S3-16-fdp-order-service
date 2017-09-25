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
        private IList<Order> _orders;
        private IList<User> _users;

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
            this._orders = JsonLoad.Orders();
            this._users = JsonLoad.Users();
            // setup DbSet
            var orders = A.Fake<DbSet<Order>>(o => o.Implements(typeof(IQueryable<Order>)).Implements(typeof(IDbAsyncEnumerable<Order>))).SetupData(this._orders); 
            var users = A.Fake<DbSet<User>>(o => o.Implements(typeof(IQueryable<User>)).Implements(typeof(IDbAsyncEnumerable<User>))).SetupData(this._users);
            // arrange
            this.dataContext = A.Fake<OrderDataContext>();
            A.CallTo(() => dataContext.Orders).Returns(orders);
            A.CallTo(() => dataContext.Users).Returns(users);
        }

        [TestMethod]
        public void UserCreatedFound()
        {
            var message = new UserCreated
            {
                Email = "test@email.com",
                Username = "test1"
            };
            var user = new UserCreatedPubSubSubScriber(bus,dataContext);
            user.Consume(message);  
            Assert.Fail();
        } 
    }
}
