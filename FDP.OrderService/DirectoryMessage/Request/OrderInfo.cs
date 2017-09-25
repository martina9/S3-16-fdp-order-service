using System;
using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Request
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.OrderInfo")]
    public class OrderInfo
    {
        [Required]
        public int Id { get; set; } 
    }
}