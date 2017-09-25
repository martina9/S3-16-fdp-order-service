namespace FDP.OrderService.Data.Repositories
{
    public class OrderRepository
    {
        protected OrderDataContext context;

        public OrderRepository(OrderDataContext context)
        {
            this.context = context;
        }
    }
}