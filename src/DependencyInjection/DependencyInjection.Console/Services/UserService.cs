using DependencyInjection.Console.Entities;
using DependencyInjection.Console.Repository;

namespace DependencyInjection.Console.Services
{
	public class UserService(IUserRepository userRepository) : IUserService
	{
		public List<User> GetUsers()
		{
			return userRepository.GetUsers();
		}
	}
}
