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
    public class RestaurantDeletedPubSubscriber : IResponder
    {
        protected readonly IBusClient Bus;
        protected OrderDataContext dataContext;
        public RestaurantDeletedPubSubscriber(IBusClient bus)
        {
            this.Bus = bus; 
        }

        public RestaurantDeletedPubSubscriber(IBusClient bus, OrderDataContext dataContext)
        {
            this.Bus = bus;
            this.dataContext = dataContext;
        }
        public async Task Consume(RestaurantDeleted message, MessageContext context)
        { 
            this.dataContext = DataUtility.GetDataContext(dataContext);
            using (dataContext)
            {
                Restaurant restaurant = dataContext.Restaurants.SingleOrDefault(p => p.Id == message.Id);
                if (restaurant == null)
                {
                    throw new Exception($"restaurant Delete : not found by Code {message.Code} by Id {message.Id}");
                }
                dataContext.Restaurants.Remove(restaurant);
                await dataContext.SaveChangesAsync();
            }
        }

        public void Subscribe()
        {
            Bus.SubscribeAsync<RestaurantDeleted>(Consume);
        }
    }
}
