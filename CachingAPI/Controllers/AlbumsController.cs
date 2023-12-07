using CachingAPI.Implementations.Providers;
using CachingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CachingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumsProvider _albumsProvider;

        public AlbumsController(AlbumsProvider albumsProvider) => _albumsProvider = albumsProvider;

        [HttpGet]
        public async Task<ActionResult<Album[]>> GetAllAsync() => Ok(await _albumsProvider.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Album?>> GetByIdAsync(int id) => Ok(await _albumsProvider.GetByIdAsync(id));

        [HttpGet("by_user_id/{id}")]
        public async Task<ActionResult<Album[]>> GetAllByUserId(int id) => Ok(await _albumsProvider.GetAllByUserId(id));
    }
}
