using System.ComponentModel.DataAnnotations;
using FDP.MessageService.Startables;

namespace FDP.OrderService.MessageDirectory.Response
{
    [Message(DirectoryName = "FDP.OrderService.MessageDirectory", TypeName = "Response.UpdateOrder")] 
    public class UpdateOrder
    {
        [Required]
        public int Id { get; set; } 
    }
}
