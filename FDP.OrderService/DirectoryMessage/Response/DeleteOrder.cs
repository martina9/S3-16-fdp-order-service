using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Response.DeleteUser")] 
    public class DeleteOrder
    {
        [Required]
        public int Id { get; set; }  
    }
}
