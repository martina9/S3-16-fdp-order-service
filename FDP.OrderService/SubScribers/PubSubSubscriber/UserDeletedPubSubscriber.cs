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
        protected OrderDataContext dataContext;
        public UserDeletedPubSubscriber(IBusClient bus)
        {
            this.Bus = bus;
        }

        public UserDeletedPubSubscriber(IBusClient bus, OrderDataContext dataContext)
        {
            this.Bus = bus;
            this.dataContext = dataContext;
        }

        public async Task Consume(UserDeleted message, MessageContext context)
        {
            this.dataContext = DataUtility.GetDataContext(dataContext);
            using (dataContext)
            {
                User user = dataContext.Users.SingleOrDefault(p => p.Email == message.Email);
                if (user == null)
                {
                    Exception ex = new Exception("User Created : not found by Email");
                    ex.Data.Add("Email", message.Email);
                    throw ex;
                }

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
