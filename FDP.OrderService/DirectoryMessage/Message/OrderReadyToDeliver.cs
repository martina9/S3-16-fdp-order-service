using System;
using System.Collections.Generic;
using FDP.Infrastructure.Interface;
using FDP.OrderService.DirectoryMessage.Shared;
using FDP.OrderService.DirectoryMessage.Shared.Enum;

namespace FDP.OrderService.DirectoryMessage.Message
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Message.OrderReadyToDeliver")]
    public class OrderReadyToDeliver
    { 
        public OrderReadyToDeliver()
        {
            ProductsToPrepare = new List<ProductToPrepare>();
        }
        public decimal PayedAmount { get; set;}

        public DateTime CreateDate { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public decimal Amount { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual List<ProductToPrepare> ProductsToPrepare { get; set; }
    }
}
