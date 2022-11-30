using System;
using System.Windows.Forms;

namespace AControl.Controls.CheckBoxs
{
    public class ReadOnlyCheckBox : CheckBox
    {
        public bool ReadOnly { get; set; }

        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly) base.OnClick(e);
        }
    }
}
