using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Response.ConfirmOrder")]
    public class ConfirmOrder
    {
        [Required]
        public int? Id { get; set; } 
    }
}
