
using a211_AutoCabinet.Datas;
using Apulsetech.Rfid.Type;
using System.Windows.Forms;

namespace a211_AutoCabinet.Forms
{
    public partial class ATMW : Form
    {
        private async void InventoryToggle()
        {
            await SharedValues.Reader.SetToggleAsync(RFID.OFF).ConfigureAwait(true);
        }

        private async void InventoryOperationSettingsRemoteFilter()
        {
            await SharedValues.Reader.SetInventoryEpcFilterStateAsync(RFID.ON).ConfigureAwait(true);
        }

    }
}
