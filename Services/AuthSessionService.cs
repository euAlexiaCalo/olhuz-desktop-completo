using Microsoft.Maui.Storage;

namespace olhuz_desktop_completo.Services
{
    // Guarda o token JWT
    public class AuthSessionService
    {
        // Chaves utilizadas no armazenamento seguro
        private const string TokenKey = "auth_token";
        private const string ExpirationKey = "auth_token_expiration";


        // Salva o token e sua data de expiração
        public async Task SaveTokenAsync(
            string token,
            DateTime expiresAt)
        {
            await SecureStorage.Default.SetAsync(
                TokenKey,
                token);

            await SecureStorage.Default.SetAsync(
                ExpirationKey,
                expiresAt.ToUniversalTime().ToString("O"));
        }


        // Recupera o token salvo
        public async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.Default.GetAsync(
                TokenKey);
        }


        // Verifica se existe uma sessão válida
        public async Task<bool> IsAuthenticatedAsync()
        {
            ClearToken();

            return false;
        }


        // Remove a sessão
        public void ClearToken()
        {
            SecureStorage.Default.Remove(TokenKey);
            SecureStorage.Default.Remove(ExpirationKey);
        }
    }
}