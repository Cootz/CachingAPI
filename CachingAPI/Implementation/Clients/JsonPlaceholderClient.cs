using CachingAPI.Models;

namespace CachingAPI.Implementation.Clients
{
    /// <summary>
    ///  Client for JSONPalceholder API
    /// </summary>
    public class JsonPlaceholderClient
    {
        private readonly HttpClient _httpClient;

        private const string Url = @"http://jsonplaceholder.typicode.com";

        public JsonPlaceholderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(Url);
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            using HttpResponseMessage httpResponce = await _httpClient.GetAsync("users");
            return (await httpResponce.Content.ReadFromJsonAsync<User[]>())!;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            using HttpResponseMessage httpResponce = await _httpClient.GetAsync($"users/{userId}");
            return await httpResponce.Content.ReadFromJsonAsync<User>();
        }
    }
}
