using FDP.OrderService.MessageDirectory.Shared.Enum;
using System;
using System.Collections.Generic; 

namespace FDP.OrderService.MessageDirectory.Shared
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public decimal Amount { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
