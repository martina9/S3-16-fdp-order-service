using System; 
using FDP.Infrastructure.Interface;
using FDP.OrderService.DirectoryMessage.Shared;
using System.Collections.Generic; 

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Response.CalculatePrice")]
    public class CalculatePrice
    {
        public CalculatePrice()
        {
            Products = new List<Product>();
        } 

        public decimal TotalAmount { get; set; }

        public List<Product> Products { get; set; } 

    }
}