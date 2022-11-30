using a211_AutoCabinet.Class;
using a211_AutoCabinet.Datas;
using Apulsetech.Rfid.Type;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace a211_AutoCabinet.Forms
{
    public partial class FormDeviceSetting : Form
    {
        private const string TAG = "FormRfidAntennaSettings";
        private const bool D = false;

        private bool mAntennaAllStates;
        private int mAntennaAllPowerGains;
        private int mAntennaAllDwellTimes;
        
        private bool[] mAntennaStates;
        private int[] mAntennaPowerGains;
        private int[] mAntennaDwellTimes;
        private int[] mAntennaTxOnTime;

        private int[] mAntDwellTimeInfo;
        private int[] mAntPowerGainInfo;
        private int AntCount = 0;
        private string ConfigLoad = string.Empty;
        //private System.Runtime.CompilerServices.ConfiguredTaskAwaitable<int> DwellTimeSettingResult;

        private object mAntennaAllLabels = new object();
        private object mAntennaAllEnableCheckBox = new object();
        private object mAntennaAllPowerGainComboBox = new object();
        private object mAntennaAllDwellTimeTextBox = new object();

        private object[] mAntennaLabels = new object[16];
        private object[] mAntennaEnableCheckBoxs = new object[16];
        private object[] mAntennaPowerGainComboBoxes = new object[16];
        private object[] mAntennaDwellTimeTextBoxes = new object[16];
        private object[] mAntennaTxOnTimeTextBoxes = new object[16];


        public delegate void RfidAntennaResultHandler(bool[] states, int[] gains, int[] dwells, int[] counts);
        public event RfidAntennaResultHandler ResultEvent;
            
        private string CultureString = string.Empty;

        public FormDeviceSetting(int AntCount, string CultureString)
        {
            InitializeComponent();

            if (SharedValues.Reader == null)
            {
                Dispose();
                return;
            }
            Initialize(AntCount);

            this.CultureString = CultureString;
            Properties.Resources.Culture = new CultureInfo(this.CultureString);
        }

        private async void Initialize(int AntCount)
        {
            InitializeControls();
            LoadConfigFile(AntCount);
            EnableControlsByPortGroup(tabAntennaPorts.SelectedIndex, true);
            EnableControls(false);
            await LoadAntennaInformation().ConfigureAwait(true);
            UpdateControlStatesByPortGroup(tabAntennaPorts.SelectedIndex);
            EnableControls(true);
            cbxAllPowerGain.Enabled = false;
            txbAllDwellTime.Enabled = false;
            cbAllEnable.Enabled = false;
        }

        // Config file로부터 DwellTime 값 가져오기
        private void LoadConfigFile(int AntCount)
        {
            try
            {
                this.AntCount = AntCount;
                mAntDwellTimeInfo = new int[this.AntCount];

                InitializeLoadConfig();

                XmlDocument ConfigSetting = new XmlDocument();
                ConfigSetting.Load(ConfigLoad);

                for (int i = 0; i < this.AntCount; i++)
                {
                    mAntDwellTimeInfo[i] = Convert.ToInt32(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntDwellTimeInfo/Ant" + (i + 1).ToString() + "DwellTimeInfo").InnerText);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Fail Load XmlFile");
            }
        }


        XmlNode[] ReAntPower;
        XmlNode[] ReAntDwellTime;
        private void SaveDeviceSettingValue()
        {
            ReAntPower = new XmlNode[128];
            ReAntDwellTime = new XmlNode[128];
            XmlDocument ConfigSetting = new XmlDocument();
            ConfigSetting.Load(ConfigLoad);

            int AntCount = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/AntCount").InnerText);


            for (int i = 0; i < AntCount; i++)
            {
                ReAntPower[i] = ConfigSetting.SelectSingleNode("Configuration/Setting/Function/AntPower/Ant" + (i+1).ToString() +"Power");
                ReAntDwellTime[i] = ConfigSetting.SelectSingleNode("Configuration/Setting/Function/AntDwellTimeInfo/Ant" + (i + 1).ToString() + "DwellTimeInfo");
                ReAntPower[i].InnerText = Convert.ToString(PowerGainArr[i]);
                ReAntDwellTime[i].InnerText = Convert.ToString(mAntennaDwellTimes[i]);
            }

            ConfigSetting.Save(ConfigLoad);
            
        }

        private void InitializeLoadConfig()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;
        }

        // 컨트롤 초기화
        private void InitializeControls()
        {

            // 설정된 안테나 수에 따라 탭 개수 설정
            // 15를 16으로 나눠도 1이 안되기 때문에 15을 더해서 만약 연결 포트 개수가 1이면 
            // 1 / 16을 하면 탭카운트가 0이므로 1 + 15를 해서 탭카운트가 1이 되도록 만들 수 있다.
            int numberOfAntennaTabGroups = (SharedValues.NumberOfAntennaPorts + 15) / 16;
            for (int i = numberOfAntennaTabGroups; i < 8; i++)
            {
                tabAntennaPorts.TabPages.RemoveAt(tabAntennaPorts.TabPages.Count - 1);
            }

            mAntennaAllLabels = lbAll;
            mAntennaAllEnableCheckBox = cbAllEnable;
            mAntennaAllPowerGainComboBox = cbxAllPowerGain;
            mAntennaAllDwellTimeTextBox = txbAllDwellTime;

            mAntennaLabels[0] = labelAntenna0;
            mAntennaLabels[1] = labelAntenna1;
            mAntennaLabels[2] = labelAntenna2;
            mAntennaLabels[3] = labelAntenna3;
            mAntennaLabels[4] = labelAntenna4;
            mAntennaLabels[5] = labelAntenna5;
            mAntennaLabels[6] = labelAntenna6;
            mAntennaLabels[7] = labelAntenna7;
            mAntennaLabels[8] = labelAntenna8;
            mAntennaLabels[9] = labelAntenna9;
            mAntennaLabels[10] = labelAntenna10;
            mAntennaLabels[11] = labelAntenna11;
            mAntennaLabels[12] = labelAntenna12;
            mAntennaLabels[13] = labelAntenna13;
            mAntennaLabels[14] = labelAntenna14;
            mAntennaLabels[15] = labelAntenna15;

            mAntennaEnableCheckBoxs[0] = checkBoxAntenna0Enable;
            mAntennaEnableCheckBoxs[1] = checkBoxAntenna1Enable;
            mAntennaEnableCheckBoxs[2] = checkBoxAntenna2Enable;
            mAntennaEnableCheckBoxs[3] = checkBoxAntenna3Enable;
            mAntennaEnableCheckBoxs[4] = checkBoxAntenna4Enable;
            mAntennaEnableCheckBoxs[5] = checkBoxAntenna5Enable;
            mAntennaEnableCheckBoxs[6] = checkBoxAntenna6Enable;
            mAntennaEnableCheckBoxs[7] = checkBoxAntenna7Enable;
            mAntennaEnableCheckBoxs[8] = checkBoxAntenna8Enable;
            mAntennaEnableCheckBoxs[9] = checkBoxAntenna9Enable;
            mAntennaEnableCheckBoxs[10] = checkBoxAntenna10Enable;
            mAntennaEnableCheckBoxs[11] = checkBoxAntenna11Enable;
            mAntennaEnableCheckBoxs[12] = checkBoxAntenna12Enable;
            mAntennaEnableCheckBoxs[13] = checkBoxAntenna13Enable;
            mAntennaEnableCheckBoxs[14] = checkBoxAntenna14Enable;
            mAntennaEnableCheckBoxs[15] = checkBoxAntenna15Enable;

            mAntennaPowerGainComboBoxes[0] = comboBoxAntenna0PowerGain;
            mAntennaPowerGainComboBoxes[1] = comboBoxAntenna1PowerGain;
            mAntennaPowerGainComboBoxes[2] = comboBoxAntenna2PowerGain;
            mAntennaPowerGainComboBoxes[3] = comboBoxAntenna3PowerGain;
            mAntennaPowerGainComboBoxes[4] = comboBoxAntenna4PowerGain;
            mAntennaPowerGainComboBoxes[5] = comboBoxAntenna5PowerGain;
            mAntennaPowerGainComboBoxes[6] = comboBoxAntenna6PowerGain;
            mAntennaPowerGainComboBoxes[7] = comboBoxAntenna7PowerGain;
            mAntennaPowerGainComboBoxes[8] = comboBoxAntenna8PowerGain;
            mAntennaPowerGainComboBoxes[9] = comboBoxAntenna9PowerGain;
            mAntennaPowerGainComboBoxes[10] = comboBoxAntenna10PowerGain;
            mAntennaPowerGainComboBoxes[11] = comboBoxAntenna11PowerGain;
            mAntennaPowerGainComboBoxes[12] = comboBoxAntenna12PowerGain;
            mAntennaPowerGainComboBoxes[13] = comboBoxAntenna13PowerGain;
            mAntennaPowerGainComboBoxes[14] = comboBoxAntenna14PowerGain;
            mAntennaPowerGainComboBoxes[15] = comboBoxAntenna15PowerGain;

            mAntennaDwellTimeTextBoxes[0] = textBoxAntenna0DwellTime;
            mAntennaDwellTimeTextBoxes[1] = textBoxAntenna1DwellTime;
            mAntennaDwellTimeTextBoxes[2] = textBoxAntenna2DwellTime;
            mAntennaDwellTimeTextBoxes[3] = textBoxAntenna3DwellTime;
            mAntennaDwellTimeTextBoxes[4] = textBoxAntenna4DwellTime;
            mAntennaDwellTimeTextBoxes[5] = textBoxAntenna5DwellTime;
            mAntennaDwellTimeTextBoxes[6] = textBoxAntenna6DwellTime;
            mAntennaDwellTimeTextBoxes[7] = textBoxAntenna7DwellTime;
            mAntennaDwellTimeTextBoxes[8] = textBoxAntenna8DwellTime;
            mAntennaDwellTimeTextBoxes[9] = textBoxAntenna9DwellTime;
            mAntennaDwellTimeTextBoxes[10] = textBoxAntenna10DwellTime;
            mAntennaDwellTimeTextBoxes[11] = textBoxAntenna11DwellTime;
            mAntennaDwellTimeTextBoxes[12] = textBoxAntenna12DwellTime;
            mAntennaDwellTimeTextBoxes[13] = textBoxAntenna13DwellTime;
            mAntennaDwellTimeTextBoxes[14] = textBoxAntenna14DwellTime;
            mAntennaDwellTimeTextBoxes[15] = textBoxAntenna15DwellTime;

            // 콤보박스에 파워 목록 추가
            for (int i = 0; i < 16; i++)
            {
                for (int j = RFID.Power.MIN_POWER; j <= RFID.Power.MAX_POWER; j++)
                {
                    ((ComboBox)mAntennaPowerGainComboBoxes[i]).Items.Add(string.Format(CultureInfo.CurrentCulture, "{0:F1} dBm", j / 1.0));
                }
            }

            for (int j = RFID.Power.MIN_POWER; j <= RFID.Power.MAX_POWER; j++)
            {
                ((ComboBox)mAntennaAllPowerGainComboBox).Items.Add(string.Format(CultureInfo.CurrentCulture, "{0:F1} dBm", j / 1.0));
            }

            ShowControlsByPortGroup(tabAntennaPorts.SelectedIndex);
        }

        private void ShowControlsByPortGroup(int groupIndex)
        {
            // 선택한 탭 인덱스에 따른 안테나 포트 
            // 만약 선택된 탭 인덱스가 0이고 포트수가 16을 넘었을 경우 0번 탭에서는 0~15번 포트가 보여지겠지
            int baseNumberOfAntennaPorts = SharedValues.NumberOfAntennaPorts - 16 * groupIndex;
            int numberOfAntennaPorts = baseNumberOfAntennaPorts > 16 ? 16 : baseNumberOfAntennaPorts;

            for (int i = 0; i < numberOfAntennaPorts; i++)
            {
                ((Label)(mAntennaLabels[i])).Visible = true;
                ((CheckBox)(mAntennaEnableCheckBoxs[i])).Visible = true;
                ((ComboBox)(mAntennaPowerGainComboBoxes[i])).Visible = true;
                ((TextBox)(mAntennaDwellTimeTextBoxes[i])).Visible = true;
            }

            // 현재 보여지는(선택된 인덱스) 안테나까지 기능 활성화하고 나머지 비활성화
            for (int i = numberOfAntennaPorts; i < 16; i++)
            {
                ((Label)(mAntennaLabels[i])).Visible = false;
                ((CheckBox)(mAntennaEnableCheckBoxs[i])).Visible = false;
                ((ComboBox)(mAntennaPowerGainComboBoxes[i])).Visible = false;
                ((TextBox)(mAntennaDwellTimeTextBoxes[i])).Visible = false;
            }
        }

        private void EnableControlsByPortGroup(int groupIndex, bool enable)
        {
            int baseNumberOfAntennaPort = SharedValues.NumberOfAntennaPorts - 16 * groupIndex;  // 현재 선택한 인덱스에서 보여질 포트 수
            int numberOfAntennaPorts = baseNumberOfAntennaPort > 16 ? 16 : baseNumberOfAntennaPort;

            ((CheckBox)mAntennaAllEnableCheckBox).Enabled = enable;
            ((ComboBox)mAntennaAllPowerGainComboBox).Enabled = enable;
            ((TextBox)mAntennaAllDwellTimeTextBox).Enabled = enable;

            for (int i = 0; i < numberOfAntennaPorts; i++)
            {
                ((CheckBox)mAntennaEnableCheckBoxs[i]).Enabled = enable;
                ((ComboBox)mAntennaPowerGainComboBoxes[i]).Enabled = enable;
                ((TextBox)mAntennaDwellTimeTextBoxes[i]).Enabled = enable;
            }
        }

        private void EnableControls(bool enabled)
        {
            foreach (TabPage page in tabAntennaPorts.TabPages)
            {
                page.Enabled = enabled;
            }

            ((CheckBox)mAntennaAllEnableCheckBox).Enabled = enabled;
            ((ComboBox)mAntennaAllPowerGainComboBox).Enabled = enabled;
            ((TextBox)mAntennaAllDwellTimeTextBox).Enabled = enabled;

            for (int i = 0; i < 16; i++)
            {
                ((CheckBox)(mAntennaEnableCheckBoxs[i])).Enabled = enabled;
                ((ComboBox)mAntennaPowerGainComboBoxes[i]).Enabled = enabled;
                ((TextBox)mAntennaDwellTimeTextBoxes[i]).Enabled = enabled;
            }

            buttonSave.Enabled = enabled;
            buttonCancel.Enabled = enabled;
        }

        private async Task LoadAntennaInformation()
        {
            if (SharedValues.Reader != null)
            {

                mAntennaStates = new bool[SharedValues.NumberOfAntennaPorts];
                mAntennaPowerGains = new int[SharedValues.NumberOfAntennaPorts];
                mAntennaDwellTimes = new int[SharedValues.NumberOfAntennaPorts];

                for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
                {
                    // 안테나 포트의 사용 여부를 반환 
                    // 팩토리 클래스 알아보기
                    mAntennaStates[i] = await SharedValues.Reader.GetAntennaPortStateAsync(i).ConfigureAwait(true) == RFID.ON;
                    int powerGain = await SharedValues.Reader.GetRadioPowerAsync(i).ConfigureAwait(true);
                    mAntennaPowerGains[i] = (powerGain >= RFID.Power.MIN_POWER) && (powerGain <= RFID.Power.MAX_POWER) ?
                        powerGain : RFID.Power.MAX_POWER;
                    mAntennaDwellTimes[i] = await SharedValues.Reader.GetDwellTimeAsync(i).ConfigureAwait(true);//mAntDwellTimeInfo[i];//await SharedValues.Reader.GetDwellTimeAsync(i).ConfigureAwait(true);
                    //DwellTimeSettingResult = SharedValues.Reader.SetDwellTimeAsync(mAntDwellTimeInfo[i]).ConfigureAwait(true);
                }
            }
        }

        private void UpdateControlStatesByPortGroup(int groupIndex)
        {
            int baseNumberOfAntennaPorts = SharedValues.NumberOfAntennaPorts - 16 * groupIndex;
            int numberOfAntennaPorts = baseNumberOfAntennaPorts > 16 ? 16 : baseNumberOfAntennaPorts;

            for (int i = 0; i < numberOfAntennaPorts; i++)
            {
                // 안테나 사용여부 체크박스 컨트롤 사용상태 업데이트
                ((CheckBox)mAntennaEnableCheckBoxs[i]).Checked = mAntennaStates[groupIndex * 16 + i];

                // 안테나 파워 콤보박스 컨트롤 사용상태 업데이트
                ComboBox antennaPowerGainComboBox = (ComboBox)mAntennaPowerGainComboBoxes[i];
                antennaPowerGainComboBox.Enabled = mAntennaStates[groupIndex * 16 + i];

                // 안테나 드웰타임 텍스트 박스 컨트롤 사용상태 업데이트
                TextBox antennaDwellTimeTextBox = (TextBox)mAntennaDwellTimeTextBoxes[i];
                antennaDwellTimeTextBox.Enabled = mAntennaStates[groupIndex * 16 + i];

                string powerString = string.Format(CultureInfo.CurrentCulture,
                    "{0:F1} dBm", mAntennaPowerGains[groupIndex * 16 + i] / 1.0);

                // 콤보박스 아이템 개수만큼
                for (int j = 0; j < antennaPowerGainComboBox.Items.Count; j++)
                {
                    // 각 아이템에 대하여
                    string item = (string)antennaPowerGainComboBox.Items[j];
                    // 각 포트마다 설정된 파워값과 일치하는 아이템 인덱스를 찾아냄
                    if (item.Equals(powerString, StringComparison.CurrentCulture))
                    {
                        // 그 인덱스로 선택 설정
                        antennaPowerGainComboBox.SelectedIndex = j;
                    }
                }

                // 각 컨트롤에 데이터값 출력
                ((Label)mAntennaLabels[i]).Text = "#" + (groupIndex * 16 + i).ToString();
                antennaDwellTimeTextBox.Text = mAntennaDwellTimes[groupIndex * 16 + i] + Properties.Resources.StringUnitMs;

            }
        }

        private int[] PowerGainArr = new int[128];
        private void SaveAntennaSettings()
        {
            //PowerGainArr = new int[128]; 
            if (SharedValues.Reader != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < AntCount; i++)
                {
                    SharedValues.Reader.SetAntennaPortState(i, mAntennaStates[i] ? RFID.ON : RFID.OFF);
                    SharedValues.Reader.SetRadioPower(i, mAntennaPowerGains[i]);
                    int powerGain = SharedValues.Reader.GetRadioPower(i);
                    int antennaPowerGain = (powerGain >= RFID.Power.MIN_POWER) && (powerGain <= RFID.Power.MAX_POWER) ? powerGain : RFID.Power.MAX_POWER;
                    PowerGainArr[i] = antennaPowerGain;
                    if (i == SharedValues.NumberOfAntennaPorts - 1)
                    {
                        sb.Append(antennaPowerGain + Properties.Resources.StringUnitDbm);
                    }
                    else
                    {
                        sb.Append(antennaPowerGain + ", ");
                    }

                    //Asyen : Asyen 안테나 Enabled -> true: 
                    SharedValues.Reader.SetAntennaPortState(i, true ? RFID.ON : RFID.OFF);
                    SharedValues.Reader.SetDwellTime(i, mAntennaDwellTimes[i]);
                }
                MessageBox.Show(Properties.Resources.StringDeviceSuccessSetting);
            }
            else
            {
                MessageBox.Show("Reader DisConnect");
            }
        }

        // 모든 포트에 대한 일괄적으로 값 세팅하기
        private void BatchSettingforAllports()
        {
            if (cbxAll.Checked)
            {
                // Config 파일 경로
                InitializeLoadConfig();

                XmlDocument ConfigFile = new XmlDocument();
                ConfigFile.Load(ConfigLoad);

                XmlNode[] AntennaAllEnable = new XmlNode[SharedValues.NumberOfAntennaPorts];
                XmlNode[] AntennaAllPower = new XmlNode[SharedValues.NumberOfAntennaPorts];
                XmlNode[] AntennaAllDwellTime = new XmlNode[SharedValues.NumberOfAntennaPorts];

                for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
                {
                    AntennaAllEnable[i] = ConfigFile.SelectSingleNode("Configuration/Setting/Function/AntEnable/Ant" + (i + 1).ToString() + "Enable");
                    AntennaAllPower[i] = ConfigFile.SelectSingleNode("Configuration/Setting/Function/AntPower/Ant" + (i + 1).ToString() + "Power");
                    AntennaAllDwellTime[i] = ConfigFile.SelectSingleNode("Configuration/Setting/Function/AntDwellTimeInfo/Ant" + (i + 1).ToString() + "DwellTimeInfo");

                    AntennaAllEnable[i].InnerText = Convert.ToString(mAntennaAllStates);
                    AntennaAllPower[i].InnerText = Convert.ToString(mAntennaAllPowerGains);
                    AntennaAllDwellTime[i].InnerText = Convert.ToString(mAntennaAllDwellTimes);
                }

                ConfigFile.Save(ConfigLoad);
            }
        }

        private async void AntennaAllSetting()
        {
            try
            {
                for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
                {
                    SharedValues.Reader.SetAntennaPortState(i, mAntennaAllStates ? RFID.ON : RFID.OFF);
                    await SharedValues.Reader.SetRadioPowerAsync(i, mAntennaAllPowerGains).ConfigureAwait(true);
                    await SharedValues.Reader.SetDwellTimeAsync(i, mAntennaAllDwellTimes).ConfigureAwait(true);
                }
                
            }
            catch
            {
                
            }

        }


        private void tabAntennaPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowControlsByPortGroup(tabAntennaPorts.SelectedIndex);
            UpdateControlStatesByPortGroup(tabAntennaPorts.SelectedIndex);
        }

        private void SettingAllValue()
        {
            for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
            {
                mAntennaStates[i] = mAntennaAllStates;
                mAntennaPowerGains[i] = mAntennaAllPowerGains;
                mAntennaDwellTimes[i] = mAntennaAllDwellTimes;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (!cbxAll.Checked)
            {
                ResultEvent(mAntennaStates, mAntennaPowerGains, mAntennaDwellTimes, mAntennaTxOnTime);
                SaveAntennaSettings();
                SaveDeviceSettingValue();
            }
            else
            {
                SettingAllValue();
                ResultEvent(mAntennaStates, mAntennaPowerGains, mAntennaDwellTimes, mAntennaTxOnTime);
                AntennaAllSetting();
                BatchSettingforAllports();
            }
                
            Close();
        }

        private void checkBoxAntenna0Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna0Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna0Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16] = enabled;
            comboBoxAntenna0PowerGain.Enabled = enabled;
            textBoxAntenna0DwellTime.Enabled = enabled;

        }

        private void checkBoxAntenna1Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna1Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna1Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 1] = enabled;
            comboBoxAntenna1PowerGain.Enabled = enabled;
            textBoxAntenna1DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna2Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna2Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna2Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 2] = enabled;
            comboBoxAntenna2PowerGain.Enabled = enabled;
            textBoxAntenna2DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna3Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna3Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna3Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 3] = enabled;
            comboBoxAntenna3PowerGain.Enabled = enabled;
            textBoxAntenna3DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna4Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna4Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna4Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 4] = enabled;
            comboBoxAntenna4PowerGain.Enabled = enabled;
            textBoxAntenna4DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna5Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna5Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna5Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 5] = enabled;
            comboBoxAntenna5PowerGain.Enabled = enabled;
            textBoxAntenna5DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna6Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna6Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna6Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 6] = enabled;
            comboBoxAntenna6PowerGain.Enabled = enabled;
            textBoxAntenna6DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna7Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna7Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna7Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 7] = enabled;
            comboBoxAntenna7PowerGain.Enabled = enabled;
            textBoxAntenna7DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna8Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna8Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna8Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 8] = enabled;
            comboBoxAntenna8PowerGain.Enabled = enabled;
            textBoxAntenna8DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna9Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna9Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna9Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 9] = enabled;
            comboBoxAntenna9PowerGain.Enabled = enabled;
            textBoxAntenna9DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna10Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna10Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna10Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 10] = enabled;
            comboBoxAntenna10PowerGain.Enabled = enabled;
            textBoxAntenna10DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna11Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna11Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna11Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 11] = enabled;
            comboBoxAntenna11PowerGain.Enabled = enabled;
            textBoxAntenna11DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna12Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna12Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna12Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 12] = enabled;
            comboBoxAntenna12PowerGain.Enabled = enabled;
            textBoxAntenna12DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna13Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna13Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna13Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 13] = enabled;
            comboBoxAntenna13PowerGain.Enabled = enabled;
            textBoxAntenna13DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna14Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna14Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna14Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 14] = enabled;
            comboBoxAntenna14PowerGain.Enabled = enabled;
            textBoxAntenna14DwellTime.Enabled = enabled;
        }

        private void checkBoxAntenna15Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAntenna15Enable.Focused)
            {
                return;
            }

            bool enabled = checkBoxAntenna15Enable.Checked;
            mAntennaStates[tabAntennaPorts.SelectedIndex * 16 + 15] = enabled;
            comboBoxAntenna15PowerGain.Enabled = enabled;
            textBoxAntenna15DwellTime.Enabled = enabled;
        }

        private void comboBoxAntenna0PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna0PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16] = 
                comboBoxAntenna0PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna1PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna1PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 1] =
                comboBoxAntenna1PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna2PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna2PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 2] =
                comboBoxAntenna2PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna3PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna3PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 3] =
                comboBoxAntenna3PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna4PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna4PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 4] =
                comboBoxAntenna4PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna5PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna5PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 5] =
                comboBoxAntenna5PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna6PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna6PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 6] =
                comboBoxAntenna6PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna7PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna7PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 7] =
                comboBoxAntenna7PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna8PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna8PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 8] =
                comboBoxAntenna8PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna9PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna9PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 9] =
                comboBoxAntenna9PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna10PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna10PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 10] =
                comboBoxAntenna10PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna11PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna11PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 11] =
                comboBoxAntenna11PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna12PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna12PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 12] =
                comboBoxAntenna12PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna13PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna13PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 13] =
                comboBoxAntenna13PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna14PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna14PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 14] =
                comboBoxAntenna14PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void comboBoxAntenna15PowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxAntenna15PowerGain.Focused)
            {
                return;
            }

            mAntennaPowerGains[tabAntennaPorts.SelectedIndex * 16 + 15] =
                comboBoxAntenna15PowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void textBoxAntenna0DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna1DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna2DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna3DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna4DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna5DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna6DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna7DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna8DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna9DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna10DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna11DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna12DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna13DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna14DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna15DwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna0DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna1DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna2DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna3DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna4DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna5DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna6DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna7DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna8DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna9DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna10DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna11DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna12DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna13DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna14DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna15DwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBoxAntenna0DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna0DwellTime.Text = null;
        }

        private void textBoxAntenna1DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna1DwellTime.Text = null;
        }

        private void textBoxAntenna2DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna2DwellTime.Text = null;
        }

        private void textBoxAntenna3DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna3DwellTime.Text = null;
        }

        private void textBoxAntenna4DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna4DwellTime.Text = null;
        }

        private void textBoxAntenna5DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna5DwellTime.Text = null;
        }

        private void textBoxAntenna6DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna6DwellTime.Text = null;
        }

        private void textBoxAntenna7DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna7DwellTime.Text = null;
        }

        private void textBoxAntenna8DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna8DwellTime.Text = null;
        }

        private void textBoxAntenna9DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna9DwellTime.Text = null;
        }

        private void textBoxAntenna10DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna10DwellTime.Text = null;
        }

        private void textBoxAntenna11DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna11DwellTime.Text = null;
        }

        private void textBoxAntenna12DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna12DwellTime.Text = null;
        }

        private void textBoxAntenna13DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna13DwellTime.Text = null;
        }

        private void textBoxAntenna14DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna14DwellTime.Text = null;
        }

        private void textBoxAntenna15DwellTime_Enter(object sender, EventArgs e)
        {
            textBoxAntenna15DwellTime.Text = null;
        }

        private void textBoxAntenna0DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna0DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna0DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna0DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna1DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna1DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna1DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 1] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna1DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 1] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna2DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna2DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna2DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 2] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna2DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 2] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna3DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna3DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna3DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 3] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna3DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 3] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna4DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna4DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna4DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 4] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna4DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 4] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna5DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna5DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna5DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 5] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna5DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 5] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna6DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna6DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna6DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 6] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna6DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 6] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna7DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna7DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna7DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 7] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna7DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 7] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna8DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna8DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna8DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 8] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna8DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 8] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna9DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna9DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna9DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 9] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna9DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 9] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna10DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna10DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna10DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 10] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna10DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 10] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna11DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna11DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna11DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 11] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna11DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 11] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna12DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna12DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna12DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 12] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna12DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 12] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna13DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna13DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna13DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 13] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna13DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 13] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna14DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna14DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna14DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 14] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna14DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 14] + Properties.Resources.StringUnitMs;
        }

        private void textBoxAntenna15DwellTime_Leave(object sender, EventArgs e)
        {
            int length = textBoxAntenna15DwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(textBoxAntenna15DwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 15] = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            textBoxAntenna15DwellTime.Text =
                mAntennaDwellTimes[tabAntennaPorts.SelectedIndex * 16 + 15] + Properties.Resources.StringUnitMs;
        }

        private void FormDeviceSetting_Load(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna4InventoryCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void cbxAllPowerGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cbxAllPowerGain.Focused)
            {
                return;
            }

            mAntennaAllPowerGains = cbxAllPowerGain.SelectedIndex + RFID.Power.MIN_POWER;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            // 체크 상태이면 개별 포트에 대한 아이템들(선택한 탭 인덱스에 대한)과 Tab 비활성화
            if (cbxAll.Checked)
            {
                tabAntennaPorts.Enabled = false;
                for (int i = 0; i < 16; i++)
                {
                    ((CheckBox)mAntennaEnableCheckBoxs[i]).Enabled = false;
                    ((ComboBox)mAntennaPowerGainComboBoxes[i]).Enabled = false;
                    ((TextBox)mAntennaDwellTimeTextBoxes[i]).Enabled = false;
                }

                ((CheckBox)mAntennaAllEnableCheckBox).Enabled = true;
                ((ComboBox)mAntennaAllPowerGainComboBox).Enabled = true;
                ((TextBox)mAntennaAllDwellTimeTextBox).Enabled = true;
            }
            else
            {
                tabAntennaPorts.Enabled = true;
                for (int i = 0; i < 16; i++)
                {
                    ((CheckBox)mAntennaEnableCheckBoxs[i]).Enabled = true;
                    ((ComboBox)mAntennaPowerGainComboBoxes[i]).Enabled = true;
                    ((TextBox)mAntennaDwellTimeTextBoxes[i]).Enabled = true;
                }

                ((CheckBox)mAntennaAllEnableCheckBox).Enabled = false;
                ((ComboBox)mAntennaAllPowerGainComboBox).Enabled = false;
                ((TextBox)mAntennaAllDwellTimeTextBox).Enabled = false;
            }
        }

        private void cbAllEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllEnable.Checked)
            {
                for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
                {
                    mAntennaAllStates = true;
                }
            }
            else
            {
                for (int i = 0; i < SharedValues.NumberOfAntennaPorts; i++)
                {
                    mAntennaAllStates = false;
                }
            }
        }

        private void txbAllDwellTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAntenna0DwellTime_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txbAllDwellTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            // 0x7f delete , 0x08 back space
            if ((c >= '0') && (c <= '9') || (c == 0x7f) || (c == 0x08))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txbAllDwellTime_Enter(object sender, EventArgs e)
        {
            txbAllDwellTime.Text = null; 
        }

        private void txbAllDwellTime_Leave(object sender, EventArgs e)
        {
            int length = txbAllDwellTime.Text?.Length ?? 0;    // text가 null일 경우 length를 0으로 초기화

            if (length > 0)
            {
                int dwellTime = Convert.ToInt32(txbAllDwellTime.Text, CultureInfo.CurrentCulture);
                if (dwellTime >= RFID.Dwell.MIN_DWELL)
                {
                    mAntennaAllDwellTimes = dwellTime;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringInvalidParameterRange);
                }
            }

            txbAllDwellTime.Text =
                mAntennaAllDwellTimes + Properties.Resources.StringUnitMs;
        }
    }

}

