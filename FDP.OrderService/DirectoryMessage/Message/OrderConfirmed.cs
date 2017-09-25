using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Message
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Message.OrderConfiremd")]
    public class OrderConfirmed
    {
        [Required]
        public int Id { get; set; }
    }
}
