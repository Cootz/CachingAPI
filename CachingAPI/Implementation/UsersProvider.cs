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
            if (await _dbContext.Users.AnyAsync())
            {
                return await _dbContext.Users
                    .Include(user => user.Address)
                    .Include(user => user.Address.Geo)
                    .Include(user => user.Company)
                    .ToArrayAsync();
            }
            else
            {
                User[] users = await _jsonPlaceholderClient.GetAllUsersAsync();

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
            User? user = await _dbContext.Users.FindAsync(userId);

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
