using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Message;

namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class UserCreatedPubSubSubScriber : IConsume<UserCreated>
    {
        protected readonly IBus Bus;

        protected OrderDataContext context;

        public UserCreatedPubSubSubScriber(IBus bus)
        {
            this.Bus = bus;
            this.context = new OrderDataContext();
        }

        public UserCreatedPubSubSubScriber(IBus bus, OrderDataContext context)
        {
            this.Bus = bus;
            this.context = context;
        }

        [AutoSubscriberConsumer(SubscriptionId = "Id")]
        public void Consume(UserCreated message)
        {
            using (context)
            {
                User user = context.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user != null)
                {
                    Exception ex = new Exception("Object Already Found");
                    ex.Data.Add("Email",message.Email);
                    ex.Data.Add("Username", message.Username);
                    throw ex;
                }

                user = new User
                {
                    Id = message.Id,
                    Email = message.Email,
                    Username = message.Username
                };

                context.Users.Add(user);

                context.SaveChanges(); 
            }
        }
    }
}
