using CachingAPI.Implementations.Db;
using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementations.Providers
{
    public class AlbumsProvider
    {
        private readonly DbCacheProvider _cacheProvider;
        private readonly CachingApiDbContext _dbContext;

        public AlbumsProvider(DbCacheProvider cacheProvider, CachingApiDbContext dbContext)
        {
            _cacheProvider = cacheProvider;
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets all albums
        /// </summary>
        /// <returns>All albums</returns>
        public async Task<Album[]> GetAllAsync()
        {
            Album[] albums = await _dbContext.Albums.ToArrayAsync();

            return albums.Length > 0 ? albums : await _cacheProvider.CacheAllAlbumsAsync();
        }

        /// <summary>
        ///  Get a specific album by it's ID
        /// </summary>
        /// <param name="albumId">Alboms ID</param>
        /// <returns>Album or null if album wasn't found</returns>
        public async Task<Album?> GetByIdAsync(int albumId)
        {
            Album? album = await _dbContext.Albums.SingleOrDefaultAsync(album => album.Id == albumId);

            // Return if album was found
            // or if database is populated we assume it already has cached API values
            if (album is not null || await _dbContext.Albums.AnyAsync())
            {
                return album;
            }

            // Populate db with albums
            Album[] albums = await _cacheProvider.CacheAllAlbumsAsync();

            return albums.SingleOrDefault(album => album.Id == albumId);
        }

        /// <summary>
        ///  Gets all albums of a scpecific user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>All albums of a user with <paramref name="userId"/></returns>
        public async Task<Album[]> GetAllByUserId(int userId)
        {
            Album[] albums = await _dbContext.Albums.Where(album => album.UserId == userId).ToArrayAsync();

            // Return if albums were found
            // or if database is populated we assume it already has cached API values
            if (albums.Length > 0 || await _dbContext.Albums.AnyAsync())
            {
                return albums;
            }

            // Populate db with albums
            Album[] album = await _cacheProvider.CacheAllAlbumsAsync();

            return album.Where(album => album.UserId == userId).ToArray();
        }
    }
}
