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
            var orders = JsonConvert.DeserializeObject<List<Order>>(jsonFilePath);
           
            return orders;
             
        }

        public static IList<Order> Order1s()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Orders.json");

            // deserialize JSON from a file      var message = JsonConvert 
            var orders = JsonConvert.DeserializeObject<List<Order>>(jsonFilePath);

            return orders; 

        } 
    }
}
