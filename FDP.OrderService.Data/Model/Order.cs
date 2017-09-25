using FDP.OrderService.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace FDP.OrderService.Data.Model
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>(); 
        }

        [Key]
        public int Id { get; set; }  
                 
        public int UserId { get; set; } 

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DeliveryType DeliveryType { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } 

        [Required]
        public decimal Amount { get; set; }
        
        public virtual List<Product> Products { get; set; }
    }
}
