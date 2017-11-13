using System.ComponentModel.DataAnnotations;

namespace FDP.OrderService.MessageDirectory.Response
{
    public class ConfirmOrder
    {
        [Required]
        public int? id { get; set; } 
    }
}
