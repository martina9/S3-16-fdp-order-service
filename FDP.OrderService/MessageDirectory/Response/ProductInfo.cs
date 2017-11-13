using System.Collections.Generic;

namespace FDP.OrderService.MessageDirectory.Response
{
    public class ProductInfo
    {
        public ProductInfo()
        {
            Products = new List<Shared.ProductInfo>(); 
        }
        public List<Shared.ProductInfo> Products { get; set; }
    }
}
