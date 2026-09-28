namespace olhuz_desktop_completo.Models.DTOs.Auth
{
    public class TokenResultDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
