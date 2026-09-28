using System;

namespace olhuz_desktop_completo.Models
{
    public class ReadingHistory
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? FileName { get; set; }

        public string? FileSize { get; set; }

        public string? FilePath { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public string DescriptionText { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}