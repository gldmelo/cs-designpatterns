using System.Diagnostics;
using DesignPatterns.Decorator;
using DesignPatterns.Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns.Website.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserRepository _userRepository;

		public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
		{
			_logger = logger;
			
			// HomeController knows nothing about what implementation is being used
			_userRepository = userRepository;
		}

		public IActionResult Index(int id = 1)
		{
			// Gets the user by their Id.
			var user = _userRepository.FindById(id);

			// 2 Scenarios here:
			// a) When the ConcreteUserRepository is used, every access will take 10 seconds to execute (See the ConcreteUserRepository).
			// b) When the CachedUserRepository is used the Repository will check if it has a copy of the User in memory before
			// going to the ConcreteUserRepository to fetch user data.
			
			// Benefits of this approach:
			// - Allows you to change the Repository code without touching it's caching logic.
			// - Allows for changing the caching code without changing the Repository itself.
			// - The Controller (or any part of the application) doesn't need (or care) which implementation is used thanks to the Interface
			
			// A further example could be using another Decorator class to log/audit when the system uses the FindById call.

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}