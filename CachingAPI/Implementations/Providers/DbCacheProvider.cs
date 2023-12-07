using CachingAPI.Implementation.Clients;
using CachingAPI.Implementations.Db;
using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementations.Providers
{
    public class DbCacheProvider
    {
        private CachingApiDbContext _dbContext;
        private JsonPlaceholderClient _placeholderClient;

        public DbCacheProvider(CachingApiDbContext dbContext, JsonPlaceholderClient jsonPlaceholderClient)
        {
            _dbContext = dbContext;
            _placeholderClient = jsonPlaceholderClient;
        }

        /// <summary>
        ///  Caches <paramref name="users"/> into database
        /// </summary>
        /// <param name="users">Users to cache</param>
        /// <returns>Cached users</returns>
        public async Task<User[]> CacheAllUsersAsync()
        {
            User[] users = await _placeholderClient.GetAllUsersAsync();

            await _dbContext.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();

            return users;
        }

        /// <summary>
        ///  Caches <paramref name="albums"/> into database
        /// </summary>
        /// <param name="albums">Alboms to cache</param>
        /// <returns>Cached alboms</returns>
        public async Task<Album[]> CacheAllAlbumsAsync()
        {
            Task<Album[]> getAllAlbumsAsync = _placeholderClient.GetAllAlmubsAsync();

            // Albums require users to persist in database. Add users if they're not cached yet
            if (!await _dbContext.Users.AnyAsync())
            {
                User[] users = await _placeholderClient.GetAllUsersAsync();
                await _dbContext.AddRangeAsync(users);
            }

            Album[] albums = await getAllAlbumsAsync;

            await _dbContext.AddRangeAsync(albums);
            await _dbContext.SaveChangesAsync();

            return albums;
        }
    }
}
