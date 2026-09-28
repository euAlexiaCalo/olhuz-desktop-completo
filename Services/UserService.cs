using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using olhuz_desktop_completo.Models.DTOs.User;
using olhuz_desktop_completo.Models.DTOs.Preferences;
using olhuz_desktop_completo.Models.Responses;
using olhuz_desktop_completo.Models.Responses.Preferences;

namespace olhuz_desktop_completo.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<UserResponseDto>> GetProfileAsync()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<UserResponseDto>>("api/user/profile");
        }

        public async Task<BaseResponse<UserResponseDto>> UpdateProfileAsync(UpdateUserProfileDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/user/profile", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<UserResponseDto>>();
        }

        public async Task<BaseResponse<object>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/profile/change-password", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<object>>();
        }

        public async Task<BaseResponse<object>> DeactivateAccountAsync()
        {
            var response = await _httpClient.PatchAsync("api/user/profile", null);

            return await response.Content.ReadFromJsonAsync<BaseResponse<object>>();
        }
    }
}
