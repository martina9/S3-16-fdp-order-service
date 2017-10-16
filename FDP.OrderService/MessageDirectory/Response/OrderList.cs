using System.Collections.Generic; 
using FDP.OrderService.MessageDirectory.Shared;

namespace FDP.OrderService.MessageDirectory.Response
{ 
    public class OrderList
    {
        public OrderList()
        {
            Items = new List<Order>();
        }
         
        public List<Order> Items { get; set; }
    }
}
