using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.ViewModel
{
    internal class SettingsViewModel : Core.ViewModel
    {
        private int _volumeMusicSetting;

        private int _volumeSoundSetting;

        public string VolumeMusicSettingText
        {
            get
            {
                return $"Music volume: {_volumeMusicSetting}";
            }
        }

        public string VolumeSoundSettingText
        {
            get
            {
                return $"Sound volume: {_volumeSoundSetting}";
            }
        }

        public double VolumeMusicSetting
        {
            get => _volumeMusicSetting;
            set
            {
                _volumeMusicSetting = (int)value;
                Sounds.BackgroundMusic.SetVolume(_volumeMusicSetting);
                OnPropertyChanged();
                OnPropertyChanged(nameof(VolumeMusicSettingText));
            }
        }

        public double VolumeSoundSetting
        {
            get => _volumeSoundSetting;
            set
            {
                _volumeSoundSetting = (int)value;
                foreach (var sound in Sounds.AllSounds) sound.SetVolume(_volumeSoundSetting);
                OnPropertyChanged();
                OnPropertyChanged(nameof(VolumeSoundSettingText));
            }
        }

        public SettingsViewModel() 
        {
            VolumeMusicSetting = 50;
            VolumeSoundSetting = 50;
        }
    }
}
