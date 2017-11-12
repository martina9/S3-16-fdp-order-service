using FDP.OrderService.Data.Model;
using FDP.OrderService.MessageDirectory.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDP.OrderService.Test
{

    public static class JsonLoad
    {
        public static IList<Order> Orders()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSources", "Orders.json");

            // deserialize JSON from a file
            using (StreamReader sr = File.OpenText(jsonFilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                IList<Order> orders = serializer.Deserialize<IList<Order>>(reader);
                return orders;
            }
        }

        public static ConfirmOrder ConfirmOrder()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSources", "ConfirmOrder.json");

            // deserialize JSON from a file
            using (StreamReader sr = File.OpenText(jsonFilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                ConfirmOrder confirmOrder = serializer.Deserialize<ConfirmOrder>(reader);
                return confirmOrder;
            }
        }

        public static IList<User> User()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSources", "Users.json");

            // deserialize JSON from a file
            using (StreamReader sr = File.OpenText(jsonFilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer(); 
                IList<User> users = serializer.Deserialize<IList<User>>(reader);
                return users;
            }
        }

        public static IList<Restaurant> Restaurants()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSources", "Restaurants.json");

            // deserialize JSON from a file
            using (StreamReader sr = File.OpenText(jsonFilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                IList<Restaurant> restaurants = serializer.Deserialize<IList<Restaurant>>(reader);
                return restaurants;
            }
        }
    }
}
