using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
	public class OrderService : IOrderService
	{
		ApplicationDbContext _context;

		public OrderService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Order> GetOrder()
		{
			var orderWithMaxPrice = await _context.Orders.
				OrderByDescending(u => u.Price).FirstOrDefaultAsync();

			return orderWithMaxPrice;
		}

		public async Task<List<Order>> GetOrders()
		{
			var ordersWithValue = await _context.Orders.
				Where(u => u.Quantity > 10).ToListAsync();

			return ordersWithValue;
		}
	}
}
