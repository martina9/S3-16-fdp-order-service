using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FDP.OrderService.Data.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Index("UQ_Rule_Product_Order", IsUnique = true, Order = 2)]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
          
        [Column("Order_Id")]
        [Index("UQ_Rule_Product_Order", IsUnique = true, Order = 1)]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
