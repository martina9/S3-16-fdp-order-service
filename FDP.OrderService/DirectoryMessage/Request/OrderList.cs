using FDP.Infrastructure.Interface;

namespace FDP.OrderService.DirectoryMessage.Request
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Request.OrderList")]
    public class OrderList
    { 
        public int? UserId { get; set; }
        public int? RestaurantId { get; set; }
    }
}