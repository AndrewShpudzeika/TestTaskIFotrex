using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;

namespace TestTask.Services.Implementations
{
	public class UserService : IUserService
	{

		private readonly ApplicationDbContext _context;

		public UserService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<User> GetUser()
		{
			var userWithMostOrders = await _context.Users
				.Where(u => u.Status == UserStatus.Active)
				.OrderByDescending(u => u.Orders.Sum(o => o.Quantity))
				.Include(u => u.Orders)
				.FirstOrDefaultAsync();

			return userWithMostOrders;
		}

		public async Task<List<User>> GetUsers()
		{
			var inactiveUsers = await _context.Users
				.Include(u => u.Orders)
				.Where(u => u.Status == UserStatus.Inactive)
				.ToListAsync();

			return inactiveUsers;
		}
	}
}
