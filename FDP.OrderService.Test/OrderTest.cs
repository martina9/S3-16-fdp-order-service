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
    public class OrderTest
    {
        public IBusClient bus;
        public IBusClient fakeBus;
        private OrderDataContext dataContext;
        private IList<Order> orders;
        private IList<User> users;
        private IList<Restaurant> restaurants;
        private ConfirmOrder confirmOrder;
        private Order order;

        [TestInitialize]
        public void Init()
        {
            this.fakeBus = A.Fake<IBusClient>();
            var path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(path1, "App_Data"));
            // create test data

            this.orders = JsonLoad.Orders();
            this.restaurants = JsonLoad.Restaurants();
            this.users = JsonLoad.User();
            order = orders.FirstOrDefault();
            this.confirmOrder = JsonLoad.ConfirmOrder();

            // setup DbSet
            var set = A.Fake<DbSet<OrderService.Data.Model.Order>>(o => o.Implements(typeof(IQueryable<Order>))
                .Implements(typeof(IDbAsyncEnumerable<Order>))).SetupData(this.orders);
            var setUser = A.Fake<DbSet<OrderService.Data.Model.User>>(o => o.Implements(typeof(IQueryable<User>))
           .Implements(typeof(IDbAsyncEnumerable<User>))).SetupData(this.users);
            var setRestaurant = A.Fake<DbSet<OrderService.Data.Model.Restaurant>>(o => o.Implements(typeof(IQueryable<Restaurant>))
           .Implements(typeof(IDbAsyncEnumerable<Restaurant>))).SetupData(this.restaurants);

            // arrange
            this.dataContext = A.Fake<Data.OrderDataContext>(); 
            A.CallTo(() => dataContext.Orders.Add(A<Order>.Ignored)).Returns(order);
            A.CallTo(() => dataContext.Orders).Returns(set);
            A.CallTo(() => dataContext.Users).Returns(setUser);
            A.CallTo(() => dataContext.Restaurants).Returns(setRestaurant);

        }
         
        [TestMethod]
        public async Task get_order_list()
        {  
            //Act
            var list = await dataContext.Orders.ToListAsync();
            var expectedList = this.orders;

            //Assert
            Assert.AreEqual(2, list.Count(), "Should have 2"); 
            CollectionAssert.AreEquivalent(list, expectedList.ToList());
        }

        [TestMethod]
        public async Task Existing_Order()
        {
            //Arrange
            int orderId = 1;
            //Act
            var order = await dataContext.Orders.SingleOrDefaultAsync(p=>p.Id == orderId);

            //Assert 
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public async Task Not_Existing_Order()
        {
            //Arrrange
            int orderId = 5;
            //Act
            var order = await dataContext.Orders.SingleOrDefaultAsync(p => p.Id == orderId); 
            //Assert 
            Assert.IsNull(order);
        }

        [TestMethod]
        public void OrderListMessage()
        { 
            OrderList orderList = new OrderList();
            orderList.RestaurantId = 1;
            var sub = new OrderListRPCSubscriber(fakeBus, dataContext);
            var result = sub.Response(orderList, new MessageContext() { GlobalRequestId = Guid.NewGuid() }).Result; 
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result.Items);
        }

        [TestMethod]
        public void OrderInfoFakeMessage()
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Id = 1;
            var sub = new OrderInfoRPCSubscriber(fakeBus, dataContext);
            var result = sub.Response(orderInfo, new MessageContext() { GlobalRequestId = Guid.NewGuid() }).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Order Object not found")]
        public void OrderInfoMessageNotExist()
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Id = 121212;
            var sub = new OrderInfoRPCSubscriber(fakeBus);
            var result = sub.Response(orderInfo, new MessageContext() { GlobalRequestId = Guid.NewGuid() }).Result;
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void CreateOrder()
        {
            ConfirmOrder confirmOrder = this.confirmOrder;
            this.bus = BusBuilder.CreateMessageBus();
            var sub = new ConfirmOrderRPCSubscriber(bus,dataContext);
            var result = sub.Response(confirmOrder, new MessageContext() { GlobalRequestId = Guid.NewGuid() }).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod] 
        [ExpectedException(typeof(Exception), "User Doesnt not exist")]
        public async Task OrderConfirNotExistingUser()
        {
            ConfirmOrder confirmOrder = this.confirmOrder;
            var sub = new ConfirmOrderRPCSubscriber(fakeBus);
            
            var result = await sub.Response(confirmOrder, new MessageContext() { GlobalRequestId = Guid.NewGuid() });
            Assert.IsNotNull(result); 
        }
    }
}
