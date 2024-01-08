using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.Core;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    public static class Sounds
    {

        public static readonly MusicPlayer BackgroundMusic = LoadSound("field_theme_1.mp3");
        public static readonly MusicPlayer GameOverSound = LoadSound("Game_Over_1.mp3");


        private static MusicPlayer LoadSound(string filename)
        {
            return new MusicPlayer(new Uri($"Assets/Sounds/{filename}",UriKind.Relative));
        }

        public static readonly List<MusicPlayer> AllSounds = new List<MusicPlayer>
        {
            GameOverSound
        };
    }
}
