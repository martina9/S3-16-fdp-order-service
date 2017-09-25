using System;
using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;
using FDP.OrderService.DirectoryMessage.Shared;
using System.Collections.Generic;
using FDP.OrderService.DirectoryMessage.Shared.Enum;

namespace FDP.OrderService.DirectoryMessage.Request
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.UpdateOrder")]
    public class UpdateOrder
    {
        public UpdateOrder()
        {
            Products = new List<Product>();
        } 

        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        public DateTime? ConfirmationDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DeliveryType DeliveryType { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Product> Products { get; set; } 

    }
}