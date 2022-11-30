using AControl.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AControl.Controls.Progress
{
    public partial class ProgressControl : Panel
    {
        private static readonly string TAG = typeof(ProgressControl).Name;

        private Bitmap m_bmpProgress;

        public ProgressControl()
        {
            InitializeComponent();

            m_bmpProgress = null;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        public void InitControl() { InitControl(Resources.Progress); }
        public void InitControl(Image image)
        {
            SetStyle(ControlStyles.Opaque |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);
            TabStop = false;
            BackColor = Color.Transparent;
            m_bmpProgress = (Bitmap)image;
            ImageAnimator.Animate(m_bmpProgress, new EventHandler(OnFrameChanged));
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();

            Graphics g = this.CreateGraphics();

            if (m_bmpProgress != null)
            {
                int x = (Size.Width - m_bmpProgress.Width) / 2;
                int y = (Size.Height - m_bmpProgress.Height) / 2;
                g.DrawImage(m_bmpProgress, new Point(x, y));
            }
            base.OnPaint(e);
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
