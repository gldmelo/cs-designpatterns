using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.Repository
{
	public interface IUserRepository
	{
		List<User> GetUsers();
	}
}
