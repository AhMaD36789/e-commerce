using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class OrderService : IOrder
    {
        private readonly StoreDbContext _context;
        private readonly IEmail _email;

        public OrderService(StoreDbContext context, IEmail email)
        {
            _context = context;
            _email = email;

        }


        public async Task<Order> Create(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            order.ID = order.ID;
            return order;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
