using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.Services
{
	public interface IUserService
	{
		List<User> GetUsers();
	}
}
