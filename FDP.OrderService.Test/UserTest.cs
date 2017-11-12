using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawRabbit;
using FDP.MessageService.Installers;
using FDP.OrderService.MessageDirectory.Request;
using FDP.OrderService.SubScribers.RPCSubScriber;
using RawRabbit.Context;
using System.Data.Entity;
using FakeItEasy;
using System.Linq;
using System.Data.Entity.Infrastructure;
using FDP.OrderService.Data.Model;
using System.Collections.Generic;
using FDP.OrderService.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FDP.OrderService.MessageDirectory.Message;
using FDP.OrderService.SubScribers.PubSubSubscriber;

namespace FDP.OrderService.Test
{
    [TestClass]
    public class UserTest
    {
        public IBusClient bus;
        public IBusClient busFake;
        private OrderDataContext dataContext; 
        private IList<User> user;

        [TestInitialize]
        public void Init()
        {
            this.busFake = A.Fake<IBusClient>();
            var path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(path1, "App_Data"));
            // create test data
            this.user = JsonLoad.User();
            var set = A.Fake<DbSet<User>>(o => o.Implements(typeof(IQueryable<User>)).Implements(typeof(IDbAsyncEnumerable<User>))).SetupData(user);
   
            // setup DbSet 
            // arrange
            this.dataContext = A.Fake<Data.OrderDataContext>();
            A.CallTo(() => dataContext.Users).Returns(set); 
        }

        [TestMethod]
        public async Task UserCreatedConsumeMessage()
        {
            UserCreated userCreated = new UserCreated()
            {
                Email = "testEmail@test.it",
                Username = "test",
                Id = 1
            };

            var sub = new UserCreatedPubSubSubScriber(bus);
            await sub.Consume(userCreated, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        public async Task UserDeletedConsumeMessage()
        {
            UserDeleted userDeleted = new UserDeleted()
            {
                Email = "testEmail@test.it",
                Username = "test",
                Id = 1
            };

            var sub = new UserDeletedPubSubscriber(bus);
            await sub.Consume(userDeleted, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        public async Task UserCreatedConsumeLocalMessage()
        {
            UserCreated userCreated = new UserCreated()
            {
                Email = "testEmail@test.it",
                Username = "test",
                Id = 1
            };

            var sub = new UserCreatedPubSubSubScriber(bus, dataContext);
            await sub.Consume(userCreated, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "User Created: not found by Email")]
        public async Task UserDeletedConsumeLocalMessage()
        {
            UserDeleted userDeleted = new UserDeleted()
            {
                Email = "notExistingEmail@not.com",
                Username = "test",
                Id = 1
            };

            var sub = new UserDeletedPubSubscriber(bus,dataContext);
            await sub.Consume(userDeleted, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }
    }
}
