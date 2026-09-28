using System;
using olhuz_desktop_completo.Models.Enums;

namespace olhuz_desktop_completo.Models
{
    public class UserPreferences
    {
        public Guid Id { get; set; }

        public bool ScreenReader { get; set; }

        public decimal SpeechRate { get; set; }

        public VoiceType VoiceType { get; set; }

        public int VolumeLevel { get; set; }

        public ThemeType Theme { get; set; }

        public bool VibrationEnabled { get; set; }

        public bool AlertSoundEnabled { get; set; }

        public Guid UserId { get; set; }

        public UserPreferences()
        {
            ScreenReader = false;
            SpeechRate = 1.0m;
            VoiceType = VoiceType.Feminina;
            VolumeLevel = 50;
            Theme = ThemeType.Light;
            VibrationEnabled = true;
            AlertSoundEnabled = true;
        }
    }
}