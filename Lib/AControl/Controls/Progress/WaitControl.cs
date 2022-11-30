using System.Windows.Forms;

namespace AControl.Controls.Progress
{
    public static class WaitControl
    {
        private static Control m_Onwer;
        private static ProgressControl m_Control;

        public static void Show(Control owner)
        {
            if (m_Onwer != null && m_Control != null)
                return;

            m_Onwer = owner;
            m_Control = new ProgressControl();
            m_Control.InitControl();
            m_Control.Bounds = m_Onwer.ClientRectangle;
            m_Control.Dock = DockStyle.Fill;
            m_Onwer.Controls.Add(m_Control);
            m_Control.BringToFront();
            m_Control.Visible = true;
        }

        public static void Hide()
        {
            if (m_Onwer == null && m_Control == null)
                return;
            m_Control.Visible = false;
            m_Onwer.Controls.Remove(m_Control);
            m_Control.Dispose();
            m_Control = null;
            m_Onwer = null;
        }
    }
}
