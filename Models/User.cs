using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olhuz_desktop_completo.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11)]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
        // Token utilizado no processo de recuperação de senha
        public string? RecoveryToken { get; set; }
        // Data limite para utilização do token de recuperação
        public DateTime? TokenExpirationDate { get; set; }
        // Indica se o token já foi utilizado
        public bool TokenUsed { get; set; }

        // Relacionamento 1:1
        // Um usuário possui apenas um conjunto de preferências
        public UserPreferences? Preferences { get; set; }

        // Propriedade de navegação para o histórico de leituras
        public List<ReadingHistory> ReadingHistories { get; set; } = new();
    }
}
