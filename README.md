# cs-designpatterns
A collection of Design Patterns implemented using C#

Here I share with you some examples on how to implement some Design Patterns using the .NET and C# programming language.

### Decorator Pattern (using .NET Dependency Injection)

This design pattern allows for changing (add or remove) an object's behavior.
In this example I demonstrate how to add a Cache behavior to an `IUserRepository` method implementation.

#### How the example works?

```C#
public interface IUserRepository
{
	void AddUser(User user);
	User? FindById(int id);
}

public class ConcreteUserRepository : IUserRepository
{
	public void AddUser(User user) { /* code to create a new User */ }

	public User? FindById(int id)
	{
		// Sleeps for 10 seconds to simulate a long-running query
		Thread.Sleep(10000);
		return new User { Id = id, Name = "John Doe" };
	}
}

public class CachedUserRepository : IUserRepository
{
	IUserRepository _userRepository;
	IMemoryCache _localCache { get; }

	public CachedUserRepository(IUserRepository userRepository, IMemoryCache localCache)
	{
		_userRepository = userRepository;
		_localCache = localCache;
	}

	public void AddUser(User user)
	{
		_userRepository.AddUser(user);
	}

	public User? FindById(int id)
	{
		// On real world implementations beware of the chosen Key to avoid collision and runtime errors
		var cachedItem = _localCache.GetOrCreate("IUserRepository-FindById-" + id,
			cacheEntry =>
			{
				cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1)); // Caches the result for a minute
				return _userRepository.FindById(id);
			});

		return cachedItem;
	}
```

Note that the Cached version receives an instance of `IUserRepository`. That will allow it to run methods on that object, thus the name Decorator. 

Now we set the `Services` part in the `Program.cs` file as shown below. Note that we instantiate the CachedUserRepository passing an object of ConcreteUserRepository. This makes the connection between the two objects.

```C#
builder.Services.AddScoped<ConcreteUserRepository>();
builder.Services.AddScoped<IUserRepository, CachedUserRepository>(provider =>
				new CachedUserRepository(
					provider.GetRequiredService<ConcreteUserRepository>(),
					provider.GetRequiredService<IMemoryCache>()));
```

Modified `HomeController.cs` code

```C#
public class HomeController : Controller
{
  private readonly IUserRepository _userRepository;
  public HomeController(IUserRepository userRepository)
  {
	  _userRepository = userRepository; // HomeController knows nothing about what implementation is being used
  }
  public IActionResult Index(int id = 1)
  {
    var user = _userRepository.FindById(id);// Gets the user by their Id.
    return View();
  }
}
```

### Benefits of this approach
* Allows you to change the Repository code without touching it's caching logic.
* Allows for changing the caching code without changing the Repository itself.
* The Controller (or any part of the application) doesn't need to know (or care) which implementation is used thanks to the Interface.

A further example could be to employ another Decorator class to log/audit when the system uses the FindById method.
