
namespace DesignPatterns.Decorator
{
	/// <summary>
	/// The usual User Repository implementation
	/// </summary>
	public class ConcreteUserRepository : IUserRepository
	{
		public void AddUser(User user)
		{
			// code to create a new User
		}

		/// <summary>
		/// Standard implementation by fetching data using a database
		/// </summary>
		public User? FindById(int id)
		{
			// Sleeps for 10 seconds to simulate a long-running query
			Thread.Sleep(10000);

			return new User { Id = id, Name = "John Doe" };
		}

	}
}
