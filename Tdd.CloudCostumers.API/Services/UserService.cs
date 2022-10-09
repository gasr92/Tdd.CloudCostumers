using Microsoft.Extensions.Options;
using System.Net;
using Tdd.CloudCostumers.API.Config;
using Tdd.CloudCostumers.API.Models;

namespace Tdd.CloudCostumers.API.Services
{
    public interface IUserService 
    {
        Task<IList<User>> GetAllUsers();
    }



    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UserApiOptions> _apiConfig;

        public UserService(HttpClient httpClient, IOptions<UserApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var res = await _httpClient.GetAsync(_apiConfig.Value.Endpoint);

            if(res.StatusCode == HttpStatusCode.NotFound)
                return new List<User>();

            var response = res.Content;
            var allUsers = await response.ReadFromJsonAsync<List<User>>();
            return allUsers?.ToList();
        }
    }
}
