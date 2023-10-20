using H3_project.Models;

namespace H3_project.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private DatabaseContext Context { get; set; }
        public OrderRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            if (newOrder.UserId.HasValue)
            {
                newOrder.user = await Context.User.FirstOrDefaultAsync(e => e.UserID == newOrder.UserId);
            }

            Context.Order.Add(newOrder);
            await Context.SaveChangesAsync();

            return newOrder;
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            Order order = await GetOrderById(orderId);
            if (order != null)
            {
                Context.Order.Remove(order);
                await Context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<List<Order>> GetOrder()
        {
            return await Context.Order.Include(e => e.user).ThenInclude(e => e.login).ThenInclude(e => e.userType).ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await Context.Order.Include(e => e.user).ThenInclude(e => e.login).ThenInclude(e => e.userType).FirstOrDefaultAsync(e => e.OrderID == orderId);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Order> UpdateOrder(int orderId, Order updateorder)
        {
            Order order = await GetOrderById(orderId);

            if (order != null)
            {
                order.OrderID = updateorder.OrderID;
                order.OrderDate = updateorder.OrderDate;

                if (updateorder != null)
                {
                    order.user = await Context.User.FirstOrDefaultAsync(e => e.UserID == updateorder.user.UserID);
                }
                else
                {
                    order.user = null; // Clear the User if null is provided
                }

                Context.Entry(order).State = EntityState.Modified;


                await Context.SaveChangesAsync();
                return await GetOrderById(orderId);
            }
            return null;
        }
    }
}
