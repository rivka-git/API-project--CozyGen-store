using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly myDBContext _dbContext;

        public OrderRepository(myDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersUser(int id)
        {
            var orders = await _dbContext.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Where(o => o.UserId == id)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
            return orders;


        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _dbContext.Orders
                                   .Include(o => o.OrderItems)
                                   .ThenInclude(oi => oi.Product)
                                   .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> AddNewOrder(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _dbContext.Products.FindAsync(orderItem.ProductId);

                if (product != null)
                {
                    product.Stock -= orderItem.Quantity;
                }
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }

}
