using DesignPatterns.Decorator;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region == DI Configuration for our Decorator pattern example ==
// Normal usage for a simple DI for the IUserRepository. Uncomment the line below to use the default implementation (without caching)
// builder.Services.AddScoped<IUserRepository, ConcreteUserRepository>();

// Commment the following lines (and uncomment the previous one) to disable the Decorator class and use the "default"
builder.Services.AddScoped<ConcreteUserRepository>();
builder.Services.AddScoped<IUserRepository, CachedUserRepository>(provider =>
				new CachedUserRepository(
					provider.GetRequiredService<ConcreteUserRepository>(),
					provider.GetRequiredService<IMemoryCache>()));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
