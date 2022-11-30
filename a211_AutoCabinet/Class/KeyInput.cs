using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace a211_AutoCabinet.Class
{
    public sealed class KeyInput
    {
        private const int KEYBD_EVENT_KEYUP = 0x02;
        private const int KEYBD_EVENT_KEYDOWN = 0x00;
        private const byte VK_V = 0x56;
        private const byte VK_CONTROL = 0x11;
        private static uint info = 0;

        [DllImport("User32.dll")]
        public static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        public static void SendKeyByClipboard(string text)
        {
            Clipboard.Clear();
            Clipboard.SetText(text);

            keybd_event(VK_CONTROL, 0, KEYBD_EVENT_KEYDOWN, info);
            keybd_event(VK_V, 0, KEYBD_EVENT_KEYDOWN, info);
            keybd_event(VK_V, 0, KEYBD_EVENT_KEYUP, info);
            keybd_event(VK_CONTROL, 0, KEYBD_EVENT_KEYUP, info);
        }
    }
}
