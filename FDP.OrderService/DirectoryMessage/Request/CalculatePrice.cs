using System; 
using FDP.Infrastructure.Interface;
using FDP.OrderService.DirectoryMessage.Shared;
using System.Collections.Generic; 

namespace FDP.OrderService.DirectoryMessage.Request
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.CalculatePrice")]
    public class CalculatePrice
    {
        public CalculatePrice()
        {
            Products = new List<Product>();
        } 

        public List<Product> Products { get; set; } 

    }
}