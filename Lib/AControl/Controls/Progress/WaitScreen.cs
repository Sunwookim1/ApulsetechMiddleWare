using AControl.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace AControl.Controls.Progress
{
    public static class WaitScreen
    {
        private static ProgressLayerControl ctrlProgress = null;
        private static Form frmParent = null;

        public static void Show(Form parent, string msg)
        {
            frmParent = parent;
            ctrlProgress = new ProgressLayerControl();
            ctrlProgress.Image = Resources.Progress;
            ctrlProgress.MaskColor = Color.FromArgb(100, 125, 125, 125);
            ctrlProgress.Bounds = frmParent.ClientRectangle;
            ctrlProgress.BringToFront();
            ctrlProgress.Visible = true;
            frmParent.Controls.Add(ctrlProgress);
            ctrlProgress.Show();
        }

        public static void Hide()
        {
            if (ctrlProgress == null)
                return;

            ctrlProgress.Visible = true;
            frmParent.Controls.Remove(ctrlProgress);
            ctrlProgress.Dispose();
            ctrlProgress = null;
            frmParent = null;
        }
    }
}
