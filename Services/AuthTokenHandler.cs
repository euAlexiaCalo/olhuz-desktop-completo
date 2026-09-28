using System.Net.Http.Headers;

namespace olhuz_desktop_completo.Services
{
    // Intercepta todas as requisições HTTP antes de elas serem enviadas para a API
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly AuthSessionService _authSessionService;

        // Recebe o serviço responsável pela sessão
        public AuthTokenHandler(AuthSessionService authSessionService)
        {
            _authSessionService = authSessionService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Recupera o JWT que foi salvo após o login.
            var token = await _authSessionService.GetTokenAsync();

            // Verifica se existe um token salvo.
            if (!string.IsNullOrWhiteSpace(token))
            {
                // Adiciona o JWT no cabeçalho Authorization
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // Continua a requisição normalmente para a API.
            return await base.SendAsync(request, cancellationToken);
        }
    }
}