using Suppliers.Application.Interfaces;
using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using System.Text;
using System.Text.Json;

namespace Suppliers.WebApi.Services
{
    public class UsersHttpClient : IUsersHttpClient
    {
        private readonly HttpClient apiClient;
        private readonly string? apiUrl;

        public UsersHttpClient(HttpClient httpClient, IConfiguration configuration) 
        {
            apiClient = httpClient;
            apiUrl = configuration.GetValue<string>("IdentityUrl");
        }

        public async Task<List<AppUserDto>> GetAllUsersAsync()
        {
            var response = await apiClient.GetAsync(apiUrl + "/localApi/users");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to load data");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<AppUserDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result ?? new List<AppUserDto>();
            }
        }

        public async Task<AppUserDto?> GetUserByIdAsync(Guid id)
        {
            var users = await GetAllUsersAsync();

            return users?.FirstOrDefault(user => user.Id == id.ToString());
        }

        public async Task SaveUserLicenseAsync(LicenseDto content)
        {
            var response = await apiClient.PostAsJsonAsync(apiUrl + "/localApi/users/save-license", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to save user license");
            }
        }

        public async Task<string?> LoadUserLicenseAsync(Guid id)
        {
            var response = await apiClient.GetAsync(apiUrl + $"/localApi/users/{id}/load-license");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to save user license");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }

        public async Task ConfirmUserLicense(Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl + $"/localApi/users/{id}/confirm-license"),
                Content = new StringContent(id.ToString())
            };

            var response = await apiClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to confirm user license");
            }
        }
    }
}
