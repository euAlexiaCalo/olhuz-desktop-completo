using System;
using System.Text.Json.Serialization;

namespace olhuz_desktop_completo.Models.Responses.Readings
{
    public class ReadingHistoryResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

        [JsonPropertyName("fileSize")]
        public string? FileSize { get; set; }

        [JsonPropertyName("fileUri")]
        public string? FileUri { get; set; }

        [JsonPropertyName("uploadDate")]
        public string UploadDate { get; set; } = string.Empty;

        [JsonPropertyName("descriptionText")]
        public string DescriptionText { get; set; } = string.Empty;

        // URL completa da imagem
        public string ImageUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FileUri))
                    return string.Empty;

                return $"https://localhost:7250{FileUri}";
            }
        }
    }
}