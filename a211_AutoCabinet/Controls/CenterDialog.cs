using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a211_AutoCabinet.Controls
{
    public class CenterDialog : IDisposable
    {

        private int m_nTries = 0;
        private Control m_frmOwner;
        private MethodInvoker m_fnFindDialog;
        private EnumThreadWndProc m_fnCallback;

        public CenterDialog(Control owner)
        {
            m_fnFindDialog = new MethodInvoker(FindDialog);
            m_fnCallback = new EnumThreadWndProc(CheckWindow);

            m_frmOwner = owner;
            owner.BeginInvoke(m_fnFindDialog);
        }

        private void FindDialog()
        {
            // Enumerate windows to find the message box
            if (m_nTries < 0) return;

            if (EnumThreadWindows(GetCurrentThreadId(), m_fnCallback, IntPtr.Zero))
            {
                if (++m_nTries < 10)
                    m_frmOwner.BeginInvoke(m_fnFindDialog);
            }
        }

        private bool CheckWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a dialog
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);

            if (sb.ToString() != "#32770")
                return true;

            // Got it
            Rectangle frmRect = new Rectangle(m_frmOwner.Location, m_frmOwner.Size);
            RECT dlgRect;

            GetWindowRect(hWnd, out dlgRect);
            MoveWindow(hWnd,
                frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                dlgRect.Right - dlgRect.Left,
                dlgRect.Bottom - dlgRect.Top, true);
            return false;
        }

        public void Dispose()
        {
            m_nTries = -1;
        }

        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

        // 스레드에 속한 윈도우 목록을 조사하는 함수
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

        // 현재 스레드의 ID를 얻는 함수
        [DllImport("Kernel32.dll")]
        private static extern int GetCurrentThreadId();

        // 지정된 Window의 Class이름을 가져오는 함수
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

        // Window의 위치및 크기 반환하는 함수
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);

        // 지정한 Window의 위치및 크기 변환하는 함수
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }    
    }
}
