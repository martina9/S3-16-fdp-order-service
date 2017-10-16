using System.Collections.Generic;
using FDP.OrderService.MessageDirectory.Shared;

namespace FDP.OrderService.MessageDirectory.Response
{
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