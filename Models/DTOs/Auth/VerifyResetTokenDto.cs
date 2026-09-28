using System.ComponentModel.DataAnnotations;

namespace olhuz_desktop_completo.Models.DTOs.Auth
{
    public class VerifyResetTokenDto
    {
        [Required(ErrorMessage = "O token é obrigatório.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "O token deve conter 6 dígitos.")]
        public string Token { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado é inválido.")]
        public string Email { get; set; } = string.Empty;
    }
}
