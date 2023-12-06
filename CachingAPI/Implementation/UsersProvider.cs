using CachingAPI.Implementation.Clients;
using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementation
{
    /// <summary>
    ///  Users data provider
    /// </summary>
    public class UsersProvider
    {
        private readonly JsonPlaceholderClient _jsonPlaceholderClient;
        private readonly CachingApiDbContext _dbContext;

        /// <summary>
        ///  Represent users table with eager loading of all related data
        /// </summary>
        private IQueryable<User> EagerUsers 
            => _dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Address.Geo)
                .Include(user => user.Company);

        public UsersProvider(JsonPlaceholderClient jsonPlaceholderClient, CachingApiDbContext dbContext)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets all users in cache/web API
        /// </summary>
        /// <returns>All users</returns>
        public async Task<User[]> GetAllAsync()
        {
            User[] users = await EagerUsers.ToArrayAsync();

            if (users.Length != 0)
            {
                return users;
            }
            else
            {
                users = await _jsonPlaceholderClient.GetAllUsersAsync();

                await cacheUsersAsync(users);
                return users;
            }
        }

        /// <summary>
        /// Gets a single user from cache/web API
        /// </summary>
        /// <param name="userId">Target user's ID</param>
        /// <returns>Found user</returns>
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            User? user = await EagerUsers.SingleAsync(user => user.Id == userId);

            if (user is not null)
            {
                return user;
            }

            user = await _jsonPlaceholderClient.GetUserByIdAsync(userId);

            if (user is not null)
            {
                await cacheUsersAsync(user);
            }
            
            return user;
        }

        /// <summary>
        ///  Caches <paramref name="users"/> into database
        /// </summary>
        /// <param name="users">Users to cache</param>
        private async Task cacheUsersAsync(params User[] users)
        {
            await _dbContext.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}
