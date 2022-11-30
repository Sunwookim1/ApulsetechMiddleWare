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
    public partial class TagDataListView : UserControl
    {
        public TagDataListView()
        {
            InitializeComponent();
            asd();
        }
        private void asd()
        {
            ListView lv = new ListView();
            lv.Columns.Add("Header", 100);
            lv.Columns.Add("Details", 100);
            lv.Dock = DockStyle.Fill;
            lv.Items.Add(new ListViewItem(new string[] { "Sachin", "Some details" }));
            lv.Items.Add(new ListViewItem(new string[] { "Stats", "More details" }));
            lv.View = View.Details;
            Controls.Add(lv);
        }
    }
}
