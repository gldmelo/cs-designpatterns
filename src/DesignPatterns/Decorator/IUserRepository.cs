
namespace DesignPatterns.Decorator
{
	// Just a common User Repository
	public interface IUserRepository
	{
		void AddUser(User user);

		User? FindById(int id);

	}
}
