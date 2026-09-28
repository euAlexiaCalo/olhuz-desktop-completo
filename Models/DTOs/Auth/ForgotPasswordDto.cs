using System.ComponentModel.DataAnnotations;

namespace olhuz_desktop_completo.Models.DTOs.Auth
{
    // DTO utilizado para solicitar recuperação de senha.
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado é inválido.")]
        public string Email { get; set; } = string.Empty;
    }
}
