
namespace DependencyInjection.Console.Entities
{
	public class User(string name, int age)
	{
		public string Name { get; set; } = name;
		public int Age { get; set; } = age;
	}
}
