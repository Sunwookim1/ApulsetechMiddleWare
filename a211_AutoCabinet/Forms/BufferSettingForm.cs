using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace a211_AutoCabinet.Forms
{
    public partial class BufferSettingForm : Form
    {

        private string ConfigLoad;
        private string SelectPortString;
        private string SettingsBufferString;
        // private string BulkChangeString;

        private int[] BufferLength = new int[128];

        // 각 포트에 대한 버퍼 길이를 받을 델리게이트 선언
        public delegate void TagBufferLengthArray(int[] BufferLength);
        public static event TagBufferLengthArray BufferSettingEvent;

        // 리스트 뷰에 사용 중인 안테나만 활성화하기 위한 배열
        private bool[] UsablePort = new bool[128];

        public BufferSettingForm()
        {
            InitializeComponent();
            InitializeLoadConfig();
            UsedPort();
        }

        private void InitializeLoadConfig() //Asyen : exe 파일이 있는 폴더에 Config 파일 위치 설정
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;

            for (int i = 0; i < 128; i++)
            {
                string[] items = new string[13];
                items[0] = (i + 1).ToString();
                ListViewItem item = new ListViewItem(items);
                LVBufferSettings.Items.Add(item);
                BufferLength[i] = 4;
            }
        }

        // 현재 사용가능한 포트 확인 
        private void UsedPort()
        {
            XmlDocument ConfigSetting = new XmlDocument();
            ConfigSetting.Load(ConfigLoad);

            int AntCount = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/AntCount").InnerText);

            // 사용 가능한 포트 확인
            for (int i = 0; i < 128; i++)
            {
                if (i < AntCount)
                    UsablePort[i] = true;
                else
                    UsablePort[i] = false;
            }

            // 사용 가능한 포트만 리스트 활성화
            for (int i = 0; i < 128; i++)
            {
                if (i < AntCount)
                    LVBufferSettings.Items[i].BackColor = Color.AliceBlue;
            }
        }

        // 설정할 포트와 버퍼 길이를 입력
        // 입력 시 입력한 포트에 대한 버퍼 길이를 업데이트
        private void SelectPort()
        {
            SelectPortString = txbSelectport.Text;  // 입력한 포트 스트링
            SettingsBufferString = txbBufferSettings.Text; // 설정한 버퍼 스트링
            //BulkChangeString = txbBulkChange.Text; 

            if (SelectPortString == null || SettingsBufferString == null)
                return;

            int PortNum = Convert.ToInt32(SelectPortString) - 1;
            int BufferNum = Convert.ToInt32(SettingsBufferString);

            if (BufferNum > 11)
                BufferNum = 11;  
            if (PortNum > 128)
                PortNum = 128;

            BufferLength[PortNum] = BufferNum;

            for (int i = 0; i < BufferNum; i++)
            {
                LVBufferSettings.Items[PortNum].UseItemStyleForSubItems = false;
                LVBufferSettings.Items[PortNum].SubItems[i + 1].BackColor = Color.BlueViolet;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SelectPort();
            if (BufferSettingEvent != null)
                BufferSettingEvent(BufferLength);
        }
    }


}
