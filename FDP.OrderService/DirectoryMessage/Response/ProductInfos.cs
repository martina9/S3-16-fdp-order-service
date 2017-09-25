using FDP.Infrastructure.Interface;
using FDP.OrderService.DirectoryMessage.Shared; 
using System.Collections.Generic; 

namespace FDP.OrderService.DirectoryMessage.Response
{
    [Message(DirectoryName = "FDP.OrderService.DirectoryMessage", TypeName = "Response.OrderConfiremd")]
    public class ProductInfos
    {
        public ProductInfos()
        {
            Products = new List<ProductInfo>(); 
        }
        public List<ProductInfo> Products { get; set; }
    }
}
