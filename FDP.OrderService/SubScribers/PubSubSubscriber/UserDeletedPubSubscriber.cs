using System;
using System.Linq;
using System.Threading.Tasks;
using FDP.MessageService.Interface;
using FDP.OrderService.Data;
using FDP.OrderService.Data.Model;
using FDP.MessageService.Responders;
using FDP.OrderService.MessageDirectory.Message;
using RawRabbit;
using RawRabbit.Context;

namespace FDP.OrderService.SubScribers.PubSubSubscriber
{
    public class UserDeletedPubSubscriber : IResponder
    {
        protected readonly IBusClient Bus;

        public UserDeletedPubSubscriber(IBusClient bus)
        {
            this.Bus = bus;
        }
         
        public async Task Consume(UserDeleted message, MessageContext context)
        {
            using (OrderDataContext dataContext = new OrderDataContext())
            {
                User user = dataContext.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user == null)
                    throw new Exception($"User Created : Image not found by Path {message.Email}");
                 
                dataContext.Users.Remove(user);
                await dataContext.SaveChangesAsync();
            }
        }

        public void Subscribe()
        {
            Bus.SubscribeAsync<UserDeleted>(Consume);
        }
    }
}
