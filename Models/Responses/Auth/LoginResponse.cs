using olhuz_desktop_completo.Models.DTOs.User;

namespace olhuz_desktop_completo.Models.Responses.Auth
{
    // Retorno da autenticação do usuário.
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UserResponseDto? User { get; set; }
    }
}
