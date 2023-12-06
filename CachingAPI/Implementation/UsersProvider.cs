using CachingAPI.Implementation.Clients;
using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementation
{
    public class UsersProvider
    {
        private readonly JsonPlaceholderClient _jsonPlaceholderClient;
        private readonly CachingApiDbContext _dbContext;

        public UsersProvider(JsonPlaceholderClient jsonPlaceholderClient, CachingApiDbContext dbContext)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
            _dbContext = dbContext;
        }

        public async Task<User[]> GetAllAsync() 
            => await _dbContext.Users.AnyAsync() ? await _dbContext.Users.ToArrayAsync() : await _jsonPlaceholderClient.GetAllUsersAsync();

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            User? user = await _dbContext.Users.FindAsync(userId);

            return user is not null ? user : await _jsonPlaceholderClient.GetUserByIdAsync(userId);
        }
    }
}
