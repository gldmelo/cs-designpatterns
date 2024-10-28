using DependencyInjection.Console.Entities;
using DependencyInjection.Console.Repository;
using DependencyInjection.Console.Services;
using Microsoft.Extensions.DependencyInjection;

// ---- Setup DI
var serviceCollection = new ServiceCollection()
	.AddScoped<IUserService, UserService>()
	.AddScoped<IUserRepository, UserRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// ---- Your app code
var userService = serviceProvider.GetRequiredService<IUserService>();

List<User> users = userService.GetUsers();
users.ForEach(user => Console.WriteLine($"Nome: { user.Name } - Age: { user.Age }"));
