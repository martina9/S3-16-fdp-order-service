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
    public class RestaurantCreatedPubSubSubScriber : IResponder
    {
        protected readonly IBusClient Bus;
        protected OrderDataContext dataContext;
        public RestaurantCreatedPubSubSubScriber(IBusClient bus)
        { 
            this.Bus = bus; 
        }

        public RestaurantCreatedPubSubSubScriber(IBusClient bus, OrderDataContext dataContext)
        {
            this.Bus = bus;
            this.dataContext = dataContext;
        }

        public async Task Consume(RestaurantCreated message, MessageContext context)
        {
            this.dataContext = DataUtility.GetDataContext(dataContext); ;
            using (dataContext)
            {
                Restaurant restaurant = dataContext.Restaurants.SingleOrDefault(p => p.Id == message.Id);
                if (restaurant != null) return;

                restaurant = new Restaurant
                {
                    Id = message.Id, 
                    Code = message.Code
                };

                dataContext.Restaurants.Add(restaurant);

                await dataContext.SaveChangesAsync();

            }
        }

        public void Subscribe()
        {
            Bus.SubscribeAsync<RestaurantCreated>(Consume);
        }
    }
}
