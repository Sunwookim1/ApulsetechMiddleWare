using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a211_AutoCabinet.Controls
{
    public partial class TagDataView : UserControl
    {
        public TagDataView()
        {
            InitializeComponent();
            InitializeBackColor();
        }

        private void InitializeBackColor()
        {
            labelAntNum.Parent = labelTagCount;
            labelAntNum.BackColor = Color.Transparent;
            labelStateIN.Parent = labelTagCount;
            labelStateIN.BackColor = Color.Transparent;
            labelStateOUT.Parent = labelTagCount;
            labelStateOUT.BackColor = Color.Transparent;
        }
    }
}
