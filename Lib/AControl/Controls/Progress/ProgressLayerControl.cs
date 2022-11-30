using AControl.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AControl.Controls.Progress
{
    public partial class ProgressLayerControl : Control
    {
        private Image m_Image;
        private Color m_MaskColor;

        public ProgressLayerControl()
        {
            InitializeComponent();
            m_Image = Resources.Progress;
        }

        [Description("Progress image"), Category("Data")]
        public Image Image
        {
            get { return m_Image; }
            set
            {
                m_Image = value;
                if (m_Image != null)
                    ImageAnimator.Animate(m_Image, new EventHandler(this.OnFrameChanged));
            }
        }
        public Color MaskColor
        {
            get { return m_MaskColor; }
            set { m_MaskColor = value; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_Image != null)
            {
                ImageAnimator.UpdateFrames();

                Bitmap bmp = new Bitmap(m_Image.Width, m_Image.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(this.m_MaskColor);
                g.DrawImage(m_Image, new Point(0, 0));
                ImageAttributes attr = new ImageAttributes();
                int x = (ClientSize.Width / 2) - (m_Image.Width / 2);
                int y = (ClientSize.Height / 2) - (m_Image.Height / 2);
                Rectangle rect = new Rectangle(x, y, m_Image.Width, m_Image.Height);
                attr.SetColorKey(this.m_MaskColor, this.m_MaskColor);
                e.Graphics.DrawImage(bmp, rect, 0, 0, m_Image.Width, m_Image.Height, GraphicsUnit.Pixel, attr);
            }
            else
            {
                Brush br = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(br, ClientRectangle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (Parent == null)
                return;
            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
