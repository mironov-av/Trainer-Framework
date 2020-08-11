using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainerFramework
{
    class PlaySound
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        string playerIdentifier;
        public PlaySound(string Identifier)
        {
            playerIdentifier = Identifier;
        }

        void open(string SoundFile)
        {
            string command = string.Format("open \"{0}\" type MPEGVideo alias {1}", SoundFile, playerIdentifier);
            mciSendString(command,null,0,IntPtr.Zero);
        }

        void play()
        {
            string command = string.Format("play {0}", playerIdentifier);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        void stop()
        {
            string command = string.Format("stop {0}", playerIdentifier);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        void close()
        {
            string command = string.Format("close {0}", playerIdentifier);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Player(string SoundFilePatch)
        {
            stop();
            close();
            open(SoundFilePatch);
            play();
        }
    }
}
