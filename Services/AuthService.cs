using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using olhuz_desktop_completo.Models.DTOs.Auth;
using olhuz_desktop_completo.Models.DTOs.User;
using olhuz_desktop_completo.Models.Responses;
using olhuz_desktop_completo.Models.Responses.Auth;

namespace olhuz_desktop_completo.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            // Inicializa o HttpClient com instância única
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<UserResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<UserResponseDto>>();
        }

        public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", dto);
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<LoginResponse>>();

            return result;
        }

        public async Task<BaseResponse<object>> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/forgot-password", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<object>>();
        }

        public async Task<BaseResponse<object>> VerifyResetTokenAsync(VerifyResetTokenDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/verify-reset-token", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<object>>();
        }

        public async Task<BaseResponse<object>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/reset-password", dto);
            return await response.Content.ReadFromJsonAsync<BaseResponse<object>>();
        }
    }
}