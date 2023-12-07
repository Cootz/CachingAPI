using CachingAPI.Implementations.Db;
using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementations.Providers
{
    /// <summary>
    ///  Users data provider
    /// </summary>
    public class UsersProvider
    {
        private readonly DbCacheProvider _cacheProvider;
        private readonly CachingApiDbContext _dbContext;

        /// <summary>
        ///  Represent users table with eager loading of all related data
        /// </summary>
        private IQueryable<User> EagerUsers
            => _dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Address.Geo)
                .Include(user => user.Company);

        public UsersProvider(DbCacheProvider cacheProvider, CachingApiDbContext dbContext)
        {
            _cacheProvider = cacheProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets all users in cache/web API
        /// </summary>
        /// <returns>All users</returns>
        public async Task<User[]> GetAllAsync()
        {
            User[] users = await EagerUsers.ToArrayAsync();

            return users.Length > 0 ? users : await _cacheProvider.CacheAllUsersAsync();
        }

        /// <summary>
        /// Gets a single user from cache/web API
        /// </summary>
        /// <param name="userId">Target user's ID</param>
        /// <returns>Found user</returns>
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            User? user = await EagerUsers.SingleOrDefaultAsync(user => user.Id == userId);

            // Return if user was found
            // or if database is populated we assume it already has cached API values
            if (user is not null || await _dbContext.Users.AnyAsync())
            {
                return user;
            }

            // Populate db with users
            User[] users = await _cacheProvider.CacheAllUsersAsync();

            return users.SingleOrDefault(user => user.Id == userId);
        }
    }
}
