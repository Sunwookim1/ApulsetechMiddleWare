using a211_AutoCabinet.Class;
using a211_AutoCabinet.Datas;
using Apulsetech.Rfid.Type;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace a211_AutoCabinet.Forms
{
    public partial class FormSessionSetting : Form
    {
        private string ConfigLoad = string.Empty;
        private string CultureString = string.Empty;

        public FormSessionSetting(string CultureString)
        {
            InitializeComponent();
            InitialContorls(true);
            UpdateControlState();
            InitializeLoadConfig();
            this.CultureString = CultureString;
            Properties.Resources.Culture = new CultureInfo(CultureString);
        }

        // xml에 세팅한 Session 값 저장
        // 프로그램이 실행될때 이전에 저장된 세션값으로 초기화하기 위해서
        private void InitializeLoadConfig() //Asyen : exe 파일이 있는 폴더에 Config 파일 위치 설정
        {
            var outPutDiretory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDiretory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;

        }

        private void InitialContorls(bool enabled)
        {
            cbSession.Enabled = enabled;
            cbTarget.Enabled = enabled;
            cbxToggleMode.Enabled = enabled;
        }   

        private async void UpdateControlState()
        {
            if (SharedValues.Reader == null)
            {
                return;
            }

            cbSession.Items.AddRange(
                SharedValues.RfidInventorySessionArray);
            int session = await SharedValues.Reader.GetSessionAsync().ConfigureAwait(true);
            if ((session >= RFID.Session.SESSION_S0) &&
                (session <= RFID.Session.SESSION_S3))
            {
                cbSession.SelectedIndex = session;
            }
            else
            {
                cbSession.SelectedIndex =
                    RFID.Session.SESSION_S0;
            }

            cbTarget.Items.AddRange(
               SharedValues.RfidInventoryTargetsArray);
            int target = await SharedValues.Reader.GetInventorySessionTargetAsync().ConfigureAwait(true);
            if ((target >= RFID.InvSessionTarget.TARGET_A) &&
                (target <= RFID.InvSessionTarget.TARGET_B))
            {
                cbTarget.SelectedIndex = target;
            }
            else
            {
                cbTarget.SelectedIndex =
                    RFID.InvSessionTarget.TARGET_A;
            }

            int Toggle = await SharedValues.Reader.GetToggleAsync().ConfigureAwait(true);
            if (Toggle == 1)
            {
                cbxToggleMode.Checked = true;
            }
            else if (Toggle == 0)
            {
                cbxToggleMode.Checked = false;
            }
         
        }

        private void SaveInventorySetting()
        {
            int result = SharedValues.Reader.SetSession(
                cbSession.SelectedIndex);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(
                    Properties.Resources.StringSelectionFailedToSetInventorySession);
            }
            else
                UpdateConfig();

            result = SharedValues.Reader.SetInventorySessionTarget(
                cbTarget.SelectedIndex);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(
                    Properties.Resources.StringSelectionFailedToSetInventoryTarget);
            }

        }

        private bool UpdateConfig()
        {
            try
            {
                XmlDocument XmlSetting = new XmlDocument();
                XmlSetting.Load(ConfigLoad);

                XmlNode ReSession = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Session");
                XmlNode ReTarget = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Target");
                XmlNode ReToggle = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Toggle");
                ReSession.InnerText = Convert.ToString(cbSession.Text);
                ReTarget.InnerText = Convert.ToString(cbTarget.Text);
                ReToggle.InnerText = Convert.ToString(cbxToggleMode.Checked ? true : false);
                XmlSetting.Save(ConfigLoad);
            }
            catch
            {
                return false;
            }
            return true;
        }


        private void FormSelectMaskSetting_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveInventorySetting();

            MessageBox.Show(Properties.Resources.StringSelectionSuccessSetting);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void cbxToggleMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxToggleMode.Checked)
            {
                await SharedValues.Reader.SetToggleAsync(1).ConfigureAwait(true);
            }
            else
                await SharedValues.Reader.SetToggleAsync(0).ConfigureAwait(true);
        }

        private void gbInventorySettings_Enter(object sender, EventArgs e)
        {

        }
    }
}
