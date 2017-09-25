using System;
using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Request
{ 
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.DeleteOrder")]
    public class DeleteOrder
    { 
        [Required]
        public int OrderId { get; set; }
    }
}