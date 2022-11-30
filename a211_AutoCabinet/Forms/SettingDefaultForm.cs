using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using a211_AutoCabinet.Datas;
using a211_AutoCabinet.Class;
using a211_AutoCabinet.Forms;

using System.Globalization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace a211_AutoCabinet.Forms
{
    public partial class SettingDefaultForm : Form
    {
        public string ConfigLoad;

        private int MinColumn;
        private int MinRow;
        private int MaxColumn;
        private int MaxRow;
        private int PanelColumn;
        private int PanelRow;
        private int Baudrate;
        private int AntCount;
        private string ComPort;
        private int AccessTimeout;
        private int AccessRetryInterval;
        private int RfTxOnTime;
        private int RfTxOffTime;

        private bool[] AntEnable;
        private int[] AntPower;
        private string[] AntCableInfo;
        private string[] AntAntExtInfo;
        private string[] AntUserPosInfo;
        private string[] AntRemarksInfo;

        public delegate void EventDeviceSettings(bool[] states, int[] gains, int[] dwells, int[] counts, string PanelRow, string PanelColumn, string DeviceName);
        public event EventDeviceSettings EventDeviceInfo;

        public delegate void EventRssiSettings(bool RssiCheck, int RssiValue);
        public event EventRssiSettings EventRssi;

        private string CultureString = string.Empty;

        // 선택된 디바이스 아이디
        private string DeviceId = string.Empty;

        // 네트워크 모드인지 확인
        private bool NetWorkMode = false;

        // 부모 폼
        ATMW mainform;

        public SettingDefaultForm(ATMW main ,string CultureString, string DeviceId, bool NetWorkMode)
        {
            InitializeComponent();
            InitializeLoadConfig();
            InitializeSettingConfig();
            this.CultureString = CultureString;
            Properties.Resources.Culture = new CultureInfo(CultureString);
            mainform = new ATMW();
            mainform = main;
            LoadDeviceInfo(DeviceId, NetWorkMode);
        }

        private void LoadDeviceInfo(string DeviceId, bool NetWorkMode)
        {
            if (DeviceId != null)
            {
                this.DeviceId = DeviceId;
                txbDeivceId.Text = this.DeviceId;
            }
            this.NetWorkMode = NetWorkMode;

        }

        public void InitializeLoadConfig() //Asyen : exe 파일이 있는 폴더에 Config 파일 위치 설정
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;
           
        }

        private void InitializeSettingConfig() //Asyen : Config 값을 가져온다
        {
            try
            {
                XmlDocument ConfigSetting = new XmlDocument();
                ConfigSetting.Load(ConfigLoad);

                MinColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinColumn").InnerText);
                MinRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinRow").InnerText);
                MaxColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxColumn").InnerText);
                MaxRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxRow").InnerText);
                PanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelColumn").InnerText);
                PanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelRow").InnerText);

                ComPort = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/ComPort").InnerText);
                Baudrate = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Baudrate").InnerText);
                AntCount = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/AntCount").InnerText);

                AccessTimeout = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Timeout").InnerText);
                AccessRetryInterval = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Interval").InnerText);
                RfTxOnTime = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOnTime").InnerText);
                RfTxOffTime = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOffTime").InnerText);

                AntPower = new int[128];
                AntEnable = new bool[128];
                AntCableInfo = new string[128];
                AntAntExtInfo = new string[128];
                AntUserPosInfo = new string[128];
                AntRemarksInfo = new string[128];
                
                for (int i = 0; i < 128; i++)
                {
                    AntEnable[i] = Convert.ToBoolean(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntEnable/Ant" + (i + 1).ToString() + "Enable").InnerText);
                    AntPower[i] = Convert.ToInt32(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntPower/Ant" + (i + 1).ToString() + "Power").InnerText);

                    AntCableInfo[i] = ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntCableInfo/Ant" + (i + 1).ToString() + "CableInfo").InnerText;
                    AntAntExtInfo[i] = ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntAntExtInfo/Ant" + (i + 1).ToString() + "AntExtInfo").InnerText;
                    AntUserPosInfo[i] = ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntUserPosInfo/Ant" + (i + 1).ToString() + "UserPosInfo").InnerText;
                    AntRemarksInfo[i] = ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntRemarksInfo/Ant" + (i + 1).ToString() + "RemarksInfo").InnerText;
                }
            }
            catch (Exception)
            {
                MinColumn = 166;
                MinRow = 146;
                MaxColumn = 498;
                MaxRow = 438;
                PanelColumn = 4;
                PanelRow = 2;

                ComPort = "";
                Baudrate = 115200;
                AntCount = 8;

                for (int i = 0; i < 128; i++)
                {
                    AntEnable[i] = true;
                    AntPower[i] = 15;
                    AntCableInfo[i] = "";
                    AntAntExtInfo[i] = "";
                    AntUserPosInfo[i] = "";
                    AntRemarksInfo[i] = "";
                }

                MessageBox.Show("Failed to load setting information! \nApply Default Values.");
            }
            
        }

        private void DefaultSettingForm_Load(object sender, EventArgs e)
        {

            ConfigSetting();

            if (SharedValues.Reader != null)
            {
                textBoxAntCount.Enabled = false;
            }
        }

        private void ConfigSetting()
        {
            textBoxPanelColumn.Text = PanelColumn.ToString();
            textBoxPanelRow.Text = PanelRow.ToString();
            textBoxAntCount.Text = AntCount.ToString();
            txbTxOnTime.Text = RfTxOnTime.ToString();
            txbTxOffTime.Text = RfTxOffTime.ToString();

            for (int i = 0; i < 128; i++)
            {
                ListViewItem lvt = new ListViewItem();
                lvt.SubItems.Add((i + 1).ToString());//Ant Number
                lvt.SubItems.Add(AntPower[i].ToString());//Ant Power
                lvt.SubItems.Add(AntCableInfo[i].ToString());//Ant LengthInfo
                lvt.SubItems.Add(AntAntExtInfo[i].ToString());//Ant CountInfo
                lvt.SubItems.Add(AntUserPosInfo[i].ToString());//Ant UserPosInfo
                lvt.SubItems.Add(AntRemarksInfo[i].ToString());//Ant RemarksInfo
            }

            // 설정 포트 개수
            SharedValues.NumberOfAntennaPorts =
                    Convert.ToInt32(textBoxAntCount.Text, CultureInfo.CurrentCulture);

        }


        private bool UpdateConfig()
        {
            try
            {
                XmlDocument XmlSetting = new XmlDocument();
                XmlSetting.Load(ConfigLoad);

                XmlNode RePanelColumn = XmlSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelColumn");
                XmlNode RePanelRow = XmlSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelRow");
                XmlNode ReBaudrate = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Baudrate");
                XmlNode ReAntCount = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/AntCount");

                XmlNode ReAccessTimeout = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Timeout");
                XmlNode ReAccessRetryInterval = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/Interval");
                XmlNode ReRfTxOnTime = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOnTime");
                XmlNode ReRfTxOffTime = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOffTime");

                XmlNode[] ReAntEnable = new XmlNode[128];
                XmlNode[] ReAntPower = new XmlNode[128];

                XmlNode[] ReAntCableInfo = new XmlNode[128];
                XmlNode[] ReAntAntExtInfo = new XmlNode[128];
                XmlNode[] ReAntUserPosInfo = new XmlNode[128];
                XmlNode[] ReAntRemarksInfo = new XmlNode[128];

                for (int i = 0; i < 128; i++)
                {
                    ReAntEnable[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntEnable/Ant" + (i + 1).ToString() + "Enable");
                    ReAntPower[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntPower/Ant" + (i + 1).ToString() + "Power");
                    
                    ReAntCableInfo[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntCableInfo/Ant" + (i + 1).ToString() + "CableInfo");
                    ReAntAntExtInfo[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntAntExtInfo/Ant" + (i + 1).ToString() + "AntExtInfo");
                    ReAntUserPosInfo[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntUserPosInfo/Ant" + (i + 1).ToString() + "UserPosInfo");
                    ReAntRemarksInfo[i] = XmlSetting.SelectSingleNode("Configuration/Setting/Function/AntRemarksInfo/Ant" + (i + 1).ToString() + "RemarksInfo");
                }

                RePanelColumn.InnerText = Convert.ToString(textBoxPanelColumn.Text);
                RePanelRow.InnerText = Convert.ToString(textBoxPanelRow.Text);
                ReAntCount.InnerText = Convert.ToString(textBoxAntCount.Text);
                ReRfTxOnTime.InnerText = Convert.ToString(txbTxOnTime.Text);
                ReRfTxOffTime.InnerText = Convert.ToString(txbTxOffTime.Text);

                XmlSetting.Save(ConfigLoad);

                if (NetWorkMode)
                {
                    if (DeviceId != null || RePanelColumn.InnerText != null || RePanelRow.InnerText != null)
                    {
                        bool result = mainform.RequestUpdateColRowNum(DeviceId, RePanelRow.InnerText, RePanelColumn.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        // 장비이름 등록 테스트 변수
        private string DeviceName = string.Empty;
        private void buttonOk_Click(object sender, EventArgs e)
        {
            
            if (UpdateConfig())
            {
                EventDeviceInfo(states, gains, dwells, counts, textBoxPanelRow.Text, textBoxPanelColumn.Text, DeviceName);
                EventRssi(RssiCheck, RssiValue);
                DialogResult = DialogResult.OK;
                MessageBox.Show(Properties.Resources.StringAntSave);
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show(Properties.Resources.StringAntFailedSave);
            }

            Close();
        }

    

        private void SettingDefaultForm_Load(object sender, EventArgs e)
        {

            ConfigSetting();

            if (SharedValues.Reader != null)
            {
                textBoxAntCount.Enabled = false;
            }
        }

        private void btnDwellTime_Click(object sender, EventArgs e)
        {
            int AntCount = Convert.ToInt32(textBoxAntCount.Text);
            using (FormDeviceSetting form = new FormDeviceSetting(AntCount, CultureString))
            {
                form.ResultEvent += new FormDeviceSetting.RfidAntennaResultHandler(GetDeviceSettingInfo);
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please connect the device first");
                }
            }
        }

        private void btnSelectMask_Click(object sender, EventArgs e)
        {
            using (FormSessionSetting form = new FormSessionSetting(CultureString))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }


        private bool[] states= null;
        private int[] gains = null;
        private int[] dwells = null;
        private int[] counts = null;

        private void GetDeviceSettingInfo(bool[] states, int[] gains, int[] dwells, int[] counts)
        {
            this.states = states;
            this.gains = gains;
            this.dwells = dwells;
            this.counts = counts;
        }

        private void btnRssiFilterSetting_Click(object sender, EventArgs e)
        {
            using (RssiFilterSettingForm form = new RssiFilterSettingForm())
            {
                form.RssiFilterCheckEvent += new RssiFilterSettingForm.RssiFilterCheck(RssiFilterCheck);
                form.RssiFilterValueEvent += new RssiFilterSettingForm.RssiFilterValue(RssiFilterValue);
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private bool RssiCheck = false;
        private void RssiFilterCheck(bool CheckState)
        {
            RssiCheck = CheckState;
        }

        private int RssiValue = 0;
        private void RssiFilterValue(int MaxValue)
        {
            RssiValue = MaxValue;
        }

        private void txbDeivceId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}