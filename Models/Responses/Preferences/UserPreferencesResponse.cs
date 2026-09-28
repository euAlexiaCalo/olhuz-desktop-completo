using olhuz_desktop_completo.Models.Enums;

namespace olhuz_desktop_completo.Models.Responses.Preferences
{
    public class UserPreferencesResponse
    {
        public bool ScreenReader { get; set; }

        public decimal SpeechRate { get; set; }

        public VoiceType VoiceType { get; set; }

        public int VolumeLevel { get; set; }

        public ThemeType Theme { get; set; }

        public bool VibrationEnabled { get; set; }

        public bool AlertSoundEnabled { get; set; }
    }
}
