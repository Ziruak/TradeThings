using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFViewSwitchNavigationDependencyInjection.Core
{
    

    public class MusicPlayer
    {
        private MediaPlayer m_mediaPlayer;


        public MusicPlayer(Uri uri)
        {
            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(uri);
        }

        public void Play()
        {
            m_mediaPlayer.MediaEnded += m_MediaEndedNoLoop;
            m_mediaPlayer.Play();
        }

        public void PlayInLoop()
        {
            m_mediaPlayer.MediaEnded += m_MediaEndedLoop;
            m_mediaPlayer.Play();
        }

        private void m_MediaEndedLoop(object? sender, EventArgs e)
        {
            m_mediaPlayer.Position = TimeSpan.Zero;
            m_mediaPlayer.Play();
        }

        private void m_MediaEndedNoLoop(object? sender, EventArgs e)
        {
            m_mediaPlayer.Stop();
            m_mediaPlayer.Position = TimeSpan.Zero;
        }

        public void Stop()
        {
            m_mediaPlayer.Stop();
        }

        public void Pause()
        {
            m_mediaPlayer.Pause();
        }


        public void SetVolume(int volume)
        {
            // MediaPlayer volume is a float value between 0 and 1.
            m_mediaPlayer.Volume = volume / 100.0f;
        }
    }
}
