using System.ComponentModel.DataAnnotations;

namespace olhuz_desktop_completo.Models.DTOs.Readings
{
    public class CreateReadingDto
    {
        [Required(ErrorMessage = "O tipo do arquivo é obrigatório.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; } = string.Empty;

        public string? FilePath { get; set; } // Arquivo enviado da câmera ou galeria

        public string DescriptionText { get; set; } = string.Empty;
    }
}