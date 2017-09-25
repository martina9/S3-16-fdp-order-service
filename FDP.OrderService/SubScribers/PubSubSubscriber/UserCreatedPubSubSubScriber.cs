using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.OrderService.DirectoryMessage.Message;

namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class UserCreatedPubSubSubScriber : IConsumeAsync<UserCreated>
    {
        protected readonly IBus Bus;

        public UserCreatedPubSubSubScriber(IBus bus)
        { 
            this.Bus = bus;
        }

        [AutoSubscriberConsumer(SubscriptionId = "Id")]
        public async Task Consume(UserCreated message)
        {
            using (OrderDataContext context = new OrderDataContext())
            {
                User user = context.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user != null) return;

                user = new User
                {
                    Id = message.Id,
                    Email = message.Email,
                    Username = message.Username
                };

                context.Users.Add(user);

                await context.SaveChangesAsync();

            }
        }
    }
}
