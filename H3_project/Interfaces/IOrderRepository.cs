namespace H3_project.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrder();
        public Task<Order> GetOrderById(int orderId);
        public Task<Order> CreateOrder(Order order);
        public Task<Order> UpdateOrder(int orderId, Order order);
        public Task<Order> DeleteOrder(int orderId);
    }
}
