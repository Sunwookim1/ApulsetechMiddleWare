using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Class.Sound
{
    public class SoundUtil
    {
        //private static SoundPlayer mSuccessSoundPlayer;
        //private static SoundPlayer mFailSoundPlayer;
        private CachedSound mSucessSound;
        private CachedSound mFailSound;
        private CachedSound mBeepSound;
        private CachedSound mAlarmSound;
        private CachedSound mCompareSound;

        public SoundUtil()
        {
            //mSuccessSoundPlayer = new SoundPlayer();
            //mFailSoundPlayer = new SoundPlayer();
            //mSuccessSoundPlayer.Stream = Properties.Resources.success;
            //mFailSoundPlayer.Stream = Properties.Resources.fail;
            mSucessSound = new CachedSound("success.wav");
            mFailSound = new CachedSound("fail.wav");
            mBeepSound = new CachedSound("beep.wav");
            mAlarmSound = new CachedSound("Alarm07.wav");
            mCompareSound = new CachedSound("compare.wav");
        }

        public void Dispose()
        {
            AudioPlaybackEngine.Instance.Dispose();
        }

        public void PlaySuccessSound()
        {
            //mSuccessSoundPlayer.Play();
            AudioPlaybackEngine.Instance.PlaySound(mSucessSound);
        }

        public void PlayFailSound()
        {
            //mFailSoundPlayer.Play();
            AudioPlaybackEngine.Instance.PlaySound(mFailSound);
        }

        public void PlayBeepSound()
        {
            AudioPlaybackEngine.Instance.PlaySound(mBeepSound);
        }

        public void PlayAlarmSound()
        {
            AudioPlaybackEngine.Instance.PlaySound(mAlarmSound);
        }

        public void PlayCompareSound()
        {
            AudioPlaybackEngine.Instance.PlaySound(mCompareSound);
        }
    }
}
