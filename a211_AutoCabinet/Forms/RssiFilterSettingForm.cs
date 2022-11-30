using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace a211_AutoCabinet.Forms
{
    public partial class RssiFilterSettingForm : Form
    {
        // Rssi 필터 사용 여부 반환 이벤트
        public delegate void RssiFilterCheck(bool CheckState);
        public event RssiFilterCheck RssiFilterCheckEvent;

        // Rssi Max Value 반환 이벤트
        public delegate void RssiFilterValue(int MaxValue);
        public event RssiFilterValue RssiFilterValueEvent;

        private string ConfigLoad = string.Empty;
        private bool RssiCheck = false;
        private int RssiMaxValue = 0;
        private bool CheckState = false;

        public RssiFilterSettingForm()
        {
            InitializeComponent();
            InitializeLoadConfig();
            InitializeSetting();
            InitialItem();
            ItemEnable(cbRssiFilter.Checked);
        }

        private void InitialItem()
        {
            cbRssiFilter.Checked = RssiCheck;
            txbFilterValue.Text = Convert.ToString(RssiMaxValue);
        }

        private void cbRssiFilter_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnFilterValue_Click(object sender, EventArgs e)
        {
            RssiFilterCheckEvent(CheckState);
            if (txbFilterValue.Text != "")
            {
                int FilterValue = Convert.ToInt32(txbFilterValue.Text);
                RssiFilterValueEvent(FilterValue);
                RssiMaxValue = FilterValue;
            }
            SaveSettingValue();
            MessageBox.Show(Properties.Resources.StringDeviceSuccessSetting);
            Close();
        }

        private void ItemEnable(bool Enable)
        {
            txbFilterValue.Enabled = Enable;
        }

        // exe 파일이 있는 폴더에 Config 파일 위치 설정
        public void InitializeLoadConfig()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad =  config_path;
        }

        public void InitializeSetting()
        {
            XmlDocument ConfigSetting = new XmlDocument();
            ConfigSetting.Load(ConfigLoad);

            RssiCheck = Convert.ToBoolean(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiCheckInfo").InnerText);
            RssiMaxValue = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiValue").InnerText);
        }

        public void SaveSettingValue()
        {
            XmlDocument ConfigSetting = new XmlDocument();
            ConfigSetting.Load(ConfigLoad);

            XmlNode RssiCheckInfo = ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiCheckInfo");
            XmlNode RssiValue = ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiValue");

            RssiCheckInfo.InnerText = Convert.ToString(CheckState);
            RssiValue.InnerText = Convert.ToString(RssiMaxValue);

            ConfigSetting.Save(ConfigLoad);
        }

        private void cbRssiFilter_CheckStateChanged(object sender, EventArgs e)
        {
            CheckState = cbRssiFilter.Checked;

            ItemEnable(CheckState);
        }
    }
}
