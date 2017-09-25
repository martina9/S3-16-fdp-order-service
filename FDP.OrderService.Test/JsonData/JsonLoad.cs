using FDP.OrderService.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO; 

namespace FDP.OrderService.Test.JsonData
{
    public static class JsonLoad
    {
        public static IList<Order> Orders()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData","Orders.json");

            // deserialize JSON from a file      var message = JsonConvert 
            var orders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(jsonFilePath));
           
            return orders;
             
        }

        public static IList<User> Users()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Users.json");

            // deserialize JSON from a file      var message = JsonConvert 
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(jsonFilePath));

            return users; 

        } 
    }
}
