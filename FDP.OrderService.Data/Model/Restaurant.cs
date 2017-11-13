
using System.ComponentModel.DataAnnotations.Schema; 

namespace FDP.OrderService.Data.Model
{
    public class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Code { get; set; }
    }
}
