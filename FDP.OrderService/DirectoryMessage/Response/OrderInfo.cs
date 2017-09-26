using System; 
using FDP.Infrastructure.Interface;
using System.Collections.Generic;
using FDP.OrderService.DirectoryMessage.Shared;
using FDP.OrderService.DirectoryMessage.Shared.Enum;

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Response.OrderInfo")]
    public class OrderInfo
    {
        public OrderInfo()
        {
            Products = new List<Product>();
        }
         
        public int Id { get; set; }

        public int UserId { get; set; }
         
        public int RestaurantId { get; set; }

        public DateTime ConfirmationDate { get; set; }
         
        public DeliveryType DeliveryType { get; set; }

        public decimal Amount { get; set; } 

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}