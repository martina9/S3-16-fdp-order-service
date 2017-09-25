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
    public class UserDeletedPubSubscriber : IConsumeAsync<UserDeleted>
    {
        protected readonly IBus Bus;

        public UserDeletedPubSubscriber(IBus bus)
        {
            this.Bus = bus;
        }

        [AutoSubscriberConsumer(SubscriptionId = "Id")]
        public async Task Consume(UserDeleted message)
        {
            using (OrderDataContext context = new OrderDataContext())
            {
                User user = context.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user == null)
                    throw new Exception($"User Created : Image not found by Path {message.Email}");
                 
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
