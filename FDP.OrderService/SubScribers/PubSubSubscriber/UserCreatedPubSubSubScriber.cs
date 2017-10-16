using System.Linq;
using System.Threading.Tasks;
using FDP.MessageService.Interface;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using RawRabbit;
using FDP.MessageService.Responders;
using FDP.OrderService.MessageDirectory.Message;
using RawRabbit.Context;

namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class UserCreatedPubSubSubScriber : IResponder
    {
        protected readonly IBusClient Bus;

        public UserCreatedPubSubSubScriber(IBusClient bus)
        { 
            this.Bus = bus;
        }

        public async Task Consume(UserCreated message, MessageContext context)
        {
            using (OrderDataContext dataContext = new OrderDataContext())
            {
                User user = dataContext.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user != null) return;

                user = new User
                {
                    Id = message.Id,
                    Email = message.Email,
                    Username = message.Username
                };

                dataContext.Users.Add(user);

                await dataContext.SaveChangesAsync();

            }
        }

        public void Subscribe()
        {
            Bus.SubscribeAsync<UserCreated>(Consume);
        }
    }
}
