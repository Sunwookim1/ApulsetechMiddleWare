using System;
using System.Windows.Forms;

namespace AControl.Dialogs
{
    public static class MsgBox
    {
        public static DialogResult Show(Control owner, string msg)
        { return Show(owner, msg, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None); }
        public static DialogResult Show(Control owner, string msg, string title)
        { return Show(owner, msg, title, MessageBoxButtons.OK, MessageBoxIcon.None); }
        public static DialogResult Show(Control owner, string msg, MessageBoxIcon icon)
        { return Show(owner, msg, String.Empty, MessageBoxButtons.OK, icon); }
        public static DialogResult Show(Control owner, string msg, string title, MessageBoxIcon icon)
        { return Show(owner, msg, title, MessageBoxButtons.OK, icon); }
        public static DialogResult Show(Control owner, string msg, string title,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (new CenterDialog(owner))
            {
                return MessageBox.Show(owner, msg, title, buttons, icon);
            }
        }

        public static DialogResult ShowQuestion(Control owner, string msg)
        { return Show(owner, msg, owner.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public static DialogResult ShowQuestion(Control owner, string msg, string title)
        { return Show(owner, msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }

        public static DialogResult ShowError(Control owner, string msg)
        { return Show(owner, msg, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static DialogResult ShowError(Control owner, string msg, string title)
        { return Show(owner, msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error); }

        public static DialogResult Show(string msg)
        { return Show(msg, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None); }
        public static DialogResult Show(string msg, string title)
        { return Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.None); }
        public static DialogResult Show(string msg, MessageBoxIcon icon)
        { return Show(msg, String.Empty, MessageBoxButtons.OK, icon); }
        public static DialogResult Show(string msg, string title, MessageBoxIcon icon)
        { return Show(msg, title, MessageBoxButtons.OK, icon); }
        public static DialogResult Show(string msg, string title,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (Application.OpenForms.Count > 0)
            {
                Form form = Application.OpenForms[Application.OpenForms.Count - 1];
                using (new CenterDialog(form))
                {
                    return MessageBox.Show(form, msg, title, buttons, icon);
                }
            }
            else
            {
                return MessageBox.Show(msg, title, buttons, icon);
            }
        }

        public static DialogResult ShowQuestion(string msg)
        { return Show(msg, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public static DialogResult ShowQuestion(string msg, string title)
        { return Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }

        public static DialogResult ShowError(string msg)
        { return Show(msg, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static DialogResult ShowError(string msg, string title)
        { return Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
