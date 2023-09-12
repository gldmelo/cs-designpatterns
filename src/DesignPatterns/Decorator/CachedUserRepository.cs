using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Decorator
{
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

		/// <summary>
		/// The Cached version uses the base UserRepository when a local copy is not found in the cache
		/// </summary>
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
	}
}
