using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.Repository
{
	/// <summary>
	/// Simulates database call to Get users
	/// </summary>
	public class UserRepository : IUserRepository
	{
		public List<User> GetUsers()
		{
			return
			[
				new("Fulano", 30),
				new("Beltrano", 25)
			];
		}
	}
}
