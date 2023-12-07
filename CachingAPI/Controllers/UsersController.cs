using CachingAPI.Implementations.Providers;
using CachingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CachingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersProvider _usersProvider;

        public UsersController(UsersProvider usersProvider) => _usersProvider = usersProvider;

        [HttpGet]
        public async Task<ActionResult<User[]>> GetAllUsersAsync() => Ok(await _usersProvider.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id) => Ok(await _usersProvider.GetUserByIdAsync(id));
    }
}
