using System.Net.Http.Json;
using olhuz_desktop_completo.Models.DTOs.Preferences;
using olhuz_desktop_completo.Models.Responses;
using olhuz_desktop_completo.Models.Responses.Preferences;

namespace olhuz_desktop_completo.Services
{
    public class UserPreferencesService
    {
        private readonly HttpClient _httpClient;

        public UserPreferencesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ================================================
        // OBTER PREFERÊNCIAS
        // ================================================

        public async Task<BaseResponse<UserPreferencesResponse>> GetPreferencesAsync()
        {
            var response = await _httpClient.GetAsync(
                "api/user/preferences");

            var result =
                await response.Content.ReadFromJsonAsync<
                    BaseResponse<UserPreferencesResponse>>();

            return result
                ?? new BaseResponse<UserPreferencesResponse>
                {
                    Error = true,
                    Message = "Não foi possível obter as preferências.",
                    StatusCode = (int)response.StatusCode
                };
        }


        // ================================================
        // ATUALIZAR PREFERÊNCIAS
        // ================================================

        public async Task<BaseResponse<UserPreferencesResponse>> UpdatePreferencesAsync(
            UpdateUserPreferencesDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync(
                "api/user/preferences",
                dto);

            var result =
                await response.Content.ReadFromJsonAsync<
                    BaseResponse<UserPreferencesResponse>>();

            return result
                ?? new BaseResponse<UserPreferencesResponse>
                {
                    Error = true,
                    Message = "Não foi possível atualizar as preferências.",
                    StatusCode = (int)response.StatusCode
                };
        }


        // ================================================
        // RESETAR PREFERÊNCIAS
        // ================================================

        public async Task<BaseResponse<UserPreferencesResponse>> ResetPreferencesAsync()
        {
            var response = await _httpClient.PostAsync(
                "api/user/preferences/reset",
                null);

            var result =
                await response.Content.ReadFromJsonAsync<
                    BaseResponse<UserPreferencesResponse>>();

            return result
                ?? new BaseResponse<UserPreferencesResponse>
                {
                    Error = true,
                    Message = "Não foi possível restaurar as preferências.",
                    StatusCode = (int)response.StatusCode
                };
        }
    }
}