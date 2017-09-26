using FDP.Infrastructure.Interface;
using System.Collections.Generic;

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.OrderList")]
    public class OrderList
    { 
        public OrderList()
        {
            Items = new List<OrderInfo>();
        } 

        public List<OrderInfo> Items { get; set; }
    }
}