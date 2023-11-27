using Microsoft.Extensions.Configuration;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using Suppliers.Application.Users.Commands.ChangePassword;
using Suppliers.Application.Users.Commands.UpdateUser;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Suppliers.Application.Common.Services
{
    public class UsersHttpClient : IUsersHttpClient
    {
        private readonly HttpClient apiClient;
        private readonly string? apiUrl;

        public UsersHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            apiClient = httpClient;
            apiUrl = configuration.GetSection("IdentityUrl").Value;
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

        public async Task UpdateUser(EditUserDto dto)
        {
            var response = await apiClient.PutAsJsonAsync(apiUrl + $"/localApi/users", dto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update user info");
            }
        }

        public async Task ChangePassword(ChangePasswordDto dto)
        {
            var response = await apiClient.PutAsJsonAsync(apiUrl + $"/localApi/users/password", dto);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new BadRequestException(await response.Content.ReadAsStringAsync());
                throw new Exception("Failed to change user password");
            }
        }

        public async Task ConfirmRegister(Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(apiUrl + $"/localApi/users/{id}/register/confirm"),
                Content = new StringContent(id.ToString())
            };

            var response = await apiClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to confirm user registration");
            }
        }

        public async Task BlockUser(Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(apiUrl + $"/localApi/users/{id}/block"),
                Content = new StringContent(id.ToString())
            };

            var response = await apiClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to block user");
            }
        }

        public async Task UnlockUser(Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(apiUrl + $"/localApi/users/{id}/unlock"),
                Content = new StringContent(id.ToString())
            };

            var response = await apiClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to block user");
            }
        }
    }
}
