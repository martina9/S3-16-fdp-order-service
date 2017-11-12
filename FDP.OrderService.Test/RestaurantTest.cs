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
    public class UnitTest1
    {
        public IBusClient bus;
        private OrderDataContext dataContext;
        private IList<Restaurant> restaurants;

        [TestInitialize]
        public void Init()
        {
            this.bus = A.Fake<IBusClient>();
            var path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(path1, "App_Data"));
            // create test data
            this.restaurants = JsonLoad.Restaurants();
            var set = A.Fake<DbSet<Restaurant>>(o => o.Implements(typeof(IQueryable<Restaurant>)).Implements(typeof(IDbAsyncEnumerable<Restaurant>))).SetupData(restaurants);

            // setup DbSet 
            // arrange
            this.dataContext = A.Fake<Data.OrderDataContext>();
            A.CallTo(() => dataContext.Restaurants).Returns(set);
        }

        [TestMethod]
        public async Task RestaurantCreatedConsumeMessage()
        {
            RestaurantCreated restaurantCreated = new RestaurantCreated()
            {
                Code = "BOSCHESE",
                Id = 1
            };

            var sub = new RestaurantCreatedPubSubSubScriber(bus);
            await sub.Consume(restaurantCreated, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        public async Task RestaurantDeletedConsumeMessage()
        {
            RestaurantDeleted restaurantDeleted = new RestaurantDeleted()
            {
                Code = "BOSCHESE",
                Id = 1
            };

            var sub = new RestaurantDeletedPubSubscriber(bus);
            await sub.Consume(restaurantDeleted, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        public async Task RestaurantCreatedConsumeFakeMessage()
        {
            RestaurantCreated restaurantCreated = new RestaurantCreated()
            {
                Code = "BOSCHESE",
                Id = 1
            };

            var sub = new RestaurantCreatedPubSubSubScriber(bus, dataContext);
            await sub.Consume(restaurantCreated, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }

        [TestMethod]
        public async Task RestaurantDeletedConsumeFakeMessage()
        {
            RestaurantDeleted restaurantDeleted = new RestaurantDeleted()
            {
                Code = "BOSCHESE",
                Id = 1
            };

            var sub = new RestaurantDeletedPubSubscriber(bus, dataContext);
            await sub.Consume(restaurantDeleted, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
        }
    }
    }
