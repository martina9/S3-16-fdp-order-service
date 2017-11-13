using System.ComponentModel.DataAnnotations;

namespace FDP.OrderService.MessageDirectory.Response
{
     public class DeleteOrder
    {
        [Required]
        public int Id { get; set; }  
    }
}
