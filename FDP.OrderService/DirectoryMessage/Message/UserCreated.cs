using System;
using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Message
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Message.UserCreated")]
    public class UserCreated
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }
    }
}
