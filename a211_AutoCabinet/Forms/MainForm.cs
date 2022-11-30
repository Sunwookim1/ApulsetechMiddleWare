using a211_AutoCabinet.Class;
using a211_AutoCabinet.Class.Sound;
using a211_AutoCabinet.Controls;
using a211_AutoCabinet.Datas;
using a211_AutoCabinet.Properties;
using Apulsetech.Event;
using Apulsetech.Rfid;
using Apulsetech.Rfid.Type;
using Apulsetech.Type;
using Apulsetech.Util;
using Apulsetech.Util.Diagnostics;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace a211_AutoCabinet.Forms
{
    public partial class ATMW : Form, ReaderEventListener
    {

        private static readonly string TAG = typeof(ATMW).Name;
        private const bool I = true;
        private const bool E = true;    // exception

        public delegate void DataPushEventHandler(bool ClearListCheck, int PortNum, string[] value);
        public DataPushEventHandler DataSendEvent;

        //Asyen_22-08-24 : 변수 (Xml에서 값 가져오기)
        private int DbPort;             //Asyen_22-08-25 : 데이터베이스 - 포트
        private string DbAddress;       //Asyen_22-08-25 : 데이터베이스 - 주소
        private string DbDatabase;      //Asyen_22-08-25 : 데이터베이스 - DB명
        private string DbUserID;        //Asyen_22-08-25 : 데이터베이스 - 유저 ID
        private string DbPassword;      //Asyen_22-08-25 : 데이터베이스 - 패스워드
        private int CountPanelColumn;   //Asyen_22-08-24 : 패널의 가로 개수
        private int CountPanelRow;      //Asyen_22-08-24 : 패널의 세로 개수
        private int MinPanelColumn;     //Asyen_22-08-24 : 패널의 최소 가로 길이
        private int MinPanelRow;        //Asyen_22-08-24 : 패널의 최소 세로 길이
        private int MaxPanelColumn;     //Asyen_22-08-24 : 패널의 최대 가로 길이
        private int MaxPanelRow;        //Asyen_22-08-24 : 패널의 최대 세로 길이
        private int AntNumFontSize;     //Asyen_22-08-25 : 패널의 안테나 넘버 사이즈
        private string AntNumFont;      //Asyen_22-08-25 : 패널의 안테나 넘버 폰트
        private int TagCountFontSize;   //Asyen_22-08-25 : 패널의 안테나 카운트 사이즈
        private string TagCountFont;    //Asyen_22-08-25 : 패널의 안테나 카운트 폰트

        private BufferSettingForm.TagBufferLengthArray m_fnBufferLength;

        private string ConnectMariaDB;

        private string ComPort; // 컴포트
        private int Baudrate;   // 보레이트
        private int AntCount;   // 안테나 개수

        private int AccessTimeout;  // 장비 접근 시간 
        private int AccessRetryInterval;
        private int RfTxOnTime;
        private int RfTxOffTime;
        private int RfTxTime;
        private bool AutoStart;
        private string RfSession;
        private bool RfToggle;
        private bool AutoStartCheck = false;

        private bool[] AntEnable;
        private int[] AntPower;
        private int[] AntDwellTime;

        private string IpAddress = null;

        //private int mRfidBlockAccessPermaLockAction = RFID.BlockPermaLockAction.READ;
        private int mRfidTagTotalCounter = 0;

        private double mOldSec = 0;
        private double mTimeInMillisec = 0;
        private double mStartTimeInMillisec = 0;
        private double mOldTotalCount = 0;

        private bool mRfidInventoryHoldTriggerEnabled = true;
        private bool mRfidInventoryFilterEnabled = true;
        //  private bool mRfidInventorySoundEnabled = false;
        private bool mRfidTagTraceFormShown = false;
        //   private bool mBeepUniqueOnlyEnabled = false;
        private bool mRfidInventoryStarted = false;
        private bool mRfidTriggerLedFormShown = false;

        private System.Windows.Forms.Timer mStopWatchTimer;

        //Asyen_22-08-24 : 변수
        private int UserDataID;
        private string ConfigLoad;
        private TagDataView[] tagDataViews;

        private const bool D = false;

        private TagItemList mTagItemList = new TagItemList();
        private SoundUtil mSoundUtil = new SoundUtil();

        private string[][] SaveTagDatasEpc = new string[128][];

        private const int SIZE_RFID_LISTVIEW_COLUMN_ReadingTime = 140;
        private const int SIZE_RFID_LISTVIEW_COLUMN_TAGVALUE = 215;
        private const int SIZE_RFID_LISTVIEW_COLUMN_RSSI = 0;
        private const int SIZE_RFID_LISTVIEW_COLUMN_PHASE = 0;
        private const int SIZE_RFID_LISTVIEW_COLUMN_CHANNEL = 0;
        private const int SIZE_RFID_LISTVIEW_COLUMN_FASTID = 0;
        private const int SIZE_RFID_LISTVIEW_COLUMN_PORT = 38;
        private const int SIZE_RFID_LISTVIEW_COLUMN_COUNT = 48;
        private const int SIZE_RFID_LISTVIEW_COLUMN_SELECTTAGVALUE = 145;
        private const int SIZE_RFID_LISTVIEW_COLUMN_COLORVALUE = 35;

        //220828

        private int g_TagBufferCurLocation = 0;//현재 점유중인 위치
        private int[] g_TagBufferCurLocationArray = new int[128];
        private int[] g_TagBufferCurLocationCheck = new int[128];//현재 점유중인 위치가 변경되었는지 체크

        private int g_TagTimerAntNo = 0;//현재 점유중인 위치가 변경되었는지 체크
        private int g_TagCurAntNo = 0;//이벤트 안테나 번호
        private int g_TagTimerAddCount = 0;//현재 점유중인 위치가 변경되었는지 체크

        private int g_TagBufferDataLength = 0; //g_TagBufferData의 태그 개수 저장고
        private string[,,][] g_TagBufferData = new string[128, 11, 3][];//안테나, 총 길이, 데이터 종류(0->epc, 1->rssi,2->아직 안정함)

        //private int g_TagBufferTotalDataLength = 0; //g_TagBufferData의 태그 개수 저장고
        private string[,][] g_TagBufferTotalData = new string[128, 3][];//안테나, 데이터 종류(0->epc, 1->rssi,2->아직 안정함)

        private string[,][] g_TagBufferTotal_IN_Tag = new string[128, 0][];//입고 안테나별
        private string[,][] g_TagBufferTotal_OUT_Tag = new string[128, 0][];//출고 안테나별

        // 각 포트에 대해 설정된 버퍼 길이를 가져오는 배열
        private int[] BufferSettingLength = new int[128];
        // 이전 포트에 설정되 었던 버퍼 길이 저장 배열
        private int[] BufferPreviousSettingLength = new int[128];

        private ToolStripMenuItem mCurrentLanguageItem;
        private string CultureString = string.Empty;

        public ATMW(int userDataID)
        {
            UserDataID = userDataID;
            GetCultureInfo();
            InitializeComponent();
            InitializeArray();
            InitializeEvent();
            InitializeLoadConfig();
            InitializeCreateConfig();
            InitializeSettingConfig();
            InitializePorts();
            IntializeStopWatchTimer();
            InitListView();
            tableModeToolStripMenuItem.Checked = true;
            m_fnBufferLength = new BufferSettingForm.TagBufferLengthArray(BufferLengthReturn);
            BufferSettingForm.BufferSettingEvent += m_fnBufferLength;

            for (int i = 0; i < 128; i++)
            {
                BufferPreviousSettingLength[i] = 2;
                BufferSettingLength[i] = 2;
                g_TagBufferCurLocationArray[i] = 0;
            }

            for (int i = 0; i < 128; i++)
            {
                string[] items = new string[15];
                items[0] = (i + 1).ToString();
                ListViewItem item = new ListViewItem(items);
                LVDebugMode.Items.Add(item);
                //checknumber[i] = 0;
                g_TagBufferCurLocationCheck[i] = 0;
            }

            // 태그 리스트 가져오는 메서드 등록
            GetTagListEvent += new GetTagList(GetTagDataList);
        }

        // 디버그모드 리스트 0으로 초기화
        private void InitDebugModeList()
        {
            for (int i = 0; i < AntCount; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    LVDebugMode.Items[i].SubItems[j + 1].Text = "0";
                }
            }
        }

        private void InitListView()
        {
            // Width of -2 indicates auto-size.
            listViewTagDataView.Columns.Add("ID", 150, HorizontalAlignment.Left);
            listViewTagDataView.Columns.Add("Port", 100, HorizontalAlignment.Left);
            listViewTagDataView.Columns.Add("Epc", 500, HorizontalAlignment.Left);
            listViewTagDataView.Columns.Add("Rssi", 200, HorizontalAlignment.Left);
            listViewTagDataView.Columns.Add("Time", 500, HorizontalAlignment.Left);
        }

        private void GetCultureInfo()
        {
            CultureInfo current = CultureInfo.CurrentCulture;
            if (current.Name.Contains("en-"))
            {
                CultureString = "en";
            }
            else if (current.Name.Contains("ko-"))
            {
                CultureString = "ko";
            }
            else
            {
                return;
            }
        }

        private void BufferLengthReturn(int[] BufferLength)
        {
            for (int i = 0; i < BufferLength.Length; i++)
            {
                BufferPreviousSettingLength[i] = BufferSettingLength[i];
                BufferSettingLength[i] = BufferLength[i];
                //Debug.WriteLine(BufferSettingLength[i].ToString());
            }

            // 새로 세팅할 버퍼
            string[,,][] BufferSwitching = new string[128, 11, 3][];
            // 버퍼 설정하기
            // 안테나 수만큼
            for (int i = 0; i < 128; i++)
            {
                int BufferCur = g_TagBufferCurLocationArray[CurentPort];

                // 현재 버퍼 길이보다 설정할 버퍼 길이가 작다면?
                if (BufferPreviousSettingLength[i] > BufferSettingLength[i])
                {
                    // 현재 버퍼부터 업데이트한 버퍼 길이만큼의 이전 버퍼 가져오기
                    for (int num = 0; num < BufferSettingLength[i]; num++)
                    {
                        BufferSwitching[i, num, 0] = g_TagBufferData[i, BufferCur, 0];
                        BufferSwitching[i, num, 1] = g_TagBufferData[i, BufferCur, 1];
                        BufferSwitching[i, num, 2] = g_TagBufferData[i, BufferCur, 2];

                        BufferCur--;

                        if (BufferCur < 0)
                        {
                            BufferCur = BufferPreviousSettingLength[i] - 1;
                        }

                        g_TagBufferData[i, num, 0] = BufferSwitching[i, num, 0];
                        g_TagBufferData[i, num, 1] = BufferSwitching[i, num, 1];
                        g_TagBufferData[i, num, 2] = BufferSwitching[i, num, 2];

                        //Debug.WriteLine(g_TagBufferData[i, num, 0].Length.ToString());
                    }

                    // 기존 버퍼 리셋
                    // 버퍼 길이 만큼
                    for (int j = 0; j < 11; j++)
                    {
                        if (j >= BufferSettingLength[i])
                        {
                            g_TagBufferData[i, j, 0] = null;
                            g_TagBufferData[i, j, 1] = null;
                            g_TagBufferData[i, j, 2] = null;
                        }
                    }

                    g_TagBufferCurLocationArray[i] = 0;
                }
                // 현재 버퍼 길이보다 설정할 버퍼 길이가 크다면?
                else if (BufferPreviousSettingLength[i] < BufferSettingLength[i])
                {

                    for (int j = BufferPreviousSettingLength[i]; j < BufferSettingLength[i]; j++)
                    {
                        if (g_TagBufferData[i, j, 0] != null)
                        {
                            Array.Resize(ref g_TagBufferData[i, j, 0], 0);
                            Array.Resize(ref g_TagBufferData[i, j, 0], 1);
                        }
                    }

                }

            }

            // 세팅완료된 버퍼를 리스트에 출력
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < AntCount; j++)
                {
                    if (g_TagBufferData[j, i, 0] != null)
                        LVDebugMode.Items[j].SubItems[i + 1].Text = g_TagBufferData[j, i, 0].Length.ToString();
                    else
                        LVDebugMode.Items[j].SubItems[i + 1].Text = " ";
                }
            }
        }

        private void InitializeArray()
        {
            tagDataViews = new TagDataView[]
            {
                tagDataView1, tagDataView2, tagDataView3, tagDataView4, tagDataView5,
                tagDataView6, tagDataView7, tagDataView8, tagDataView9, tagDataView10,
                tagDataView11, tagDataView12, tagDataView13, tagDataView14, tagDataView15,
                tagDataView16, tagDataView17, tagDataView18, tagDataView19, tagDataView20,
                tagDataView21, tagDataView22, tagDataView23, tagDataView24, tagDataView25,
                tagDataView26, tagDataView27, tagDataView28, tagDataView29, tagDataView30,
                tagDataView31, tagDataView32, tagDataView33, tagDataView34, tagDataView35,
                tagDataView36, tagDataView37, tagDataView38, tagDataView39, tagDataView40,
                tagDataView41, tagDataView42, tagDataView43, tagDataView44, tagDataView45,
                tagDataView46, tagDataView47, tagDataView48, tagDataView49, tagDataView50,
                tagDataView51, tagDataView52, tagDataView53, tagDataView54, tagDataView55,
                tagDataView56, tagDataView57, tagDataView58, tagDataView59, tagDataView60,
                tagDataView61, tagDataView62, tagDataView63, tagDataView64, tagDataView65,
                tagDataView66, tagDataView67, tagDataView68, tagDataView69, tagDataView70,
                tagDataView71, tagDataView72, tagDataView73, tagDataView74, tagDataView75,
                tagDataView76, tagDataView77, tagDataView78, tagDataView79, tagDataView80,
                tagDataView81, tagDataView82, tagDataView83, tagDataView84, tagDataView85,
                tagDataView86, tagDataView87, tagDataView88, tagDataView89, tagDataView90,
                tagDataView91, tagDataView92, tagDataView93, tagDataView94, tagDataView95,
                tagDataView96, tagDataView97, tagDataView98, tagDataView99, tagDataView100,
                tagDataView101, tagDataView102, tagDataView103, tagDataView104, tagDataView105,
                tagDataView106, tagDataView107, tagDataView108, tagDataView109, tagDataView110,
                tagDataView111, tagDataView112, tagDataView113, tagDataView114, tagDataView115,
                tagDataView116, tagDataView117, tagDataView118, tagDataView119, tagDataView120,
                tagDataView121, tagDataView122, tagDataView123, tagDataView124, tagDataView125,
                tagDataView126, tagDataView127, tagDataView128
            };

        }

        private void InitializeEvent()
        {
            for (int i = 0; i < tagDataViews.Length; i++)
            {
                tagDataViews[i].labelTagCount.Click += new EventHandler(tagDataViews_Click);
            }
        }

        public void InitializeLoadConfig() //Asyen : exe 파일이 있는 폴더에 Config 파일 위치 설정
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;
        }

        private void InitializeCreateConfig()
        {
            CreateConfig createConfig = new CreateConfig();
            CreateConfig.MainConfig();
        }

        private void InitializeSetting()
        {
            XmlDocument ConfigSetting = new XmlDocument();
            ConfigSetting.Load(ConfigLoad);

            CountPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelColumn").InnerText);
            CountPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelRow").InnerText);
            MinPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinColumn").InnerText);
            MinPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinRow").InnerText);
            MaxPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxColumn").InnerText);
            MaxPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxRow").InnerText);
            RssiCheck = Convert.ToBoolean(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiCheckInfo").InnerText);
            RssiValue = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/RssiValue").InnerText);
        }

        private void InitializeSettingConfig()
        {
            try
            {
                XmlDocument ConfigSetting = new XmlDocument();
                ConfigSetting.Load(ConfigLoad);

                DbPort = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Database/Port").InnerText);
                DbAddress = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Database/Address").InnerText);
                DbDatabase = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Database/Database").InnerText);
                DbUserID = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Database/UserID").InnerText);
                DbPassword = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Database/Password").InnerText);

                CountPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelColumn").InnerText);
                CountPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/PanelRow").InnerText);
                MinPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinColumn").InnerText);
                MinPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MinRow").InnerText);
                MaxPanelColumn = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxColumn").InnerText);
                MaxPanelRow = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/MaxRow").InnerText);
                AntNumFontSize = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/AntNumFontSize").InnerText);
                AntNumFont = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/AntNumFont").InnerText);
                TagCountFontSize = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/TagCountFontSize ").InnerText);
                TagCountFont = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/TableLayoutPanelTagDataView/TagCountFont").InnerText);

                for (int i = 0; i < 128; i++)
                {
                    tagDataViews[i].labelAntNum.Font = new System.Drawing.Font(AntNumFont, AntNumFontSize, System.Drawing.FontStyle.Bold);
                    tagDataViews[i].labelTagCount.Font = new System.Drawing.Font(TagCountFont, TagCountFontSize, System.Drawing.FontStyle.Bold);
                }



                ComPort = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/ComPort").InnerText);
                Baudrate = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Baudrate").InnerText);
                AntCount = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/AntCount").InnerText);

                AccessTimeout = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Timeout").InnerText);
                AccessRetryInterval = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Interval").InnerText);
                RfTxOnTime = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOnTime").InnerText);
                RfTxOffTime = Convert.ToInt32(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/TxOffTime").InnerText);
                AutoStart = Convert.ToBoolean(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/AutoStart").InnerText);
                RfSession = Convert.ToString(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Session").InnerText);
                RfToggle = Convert.ToBoolean(ConfigSetting.SelectSingleNode("Configuration/Setting/Function/Device/Toggle").InnerText);

                RfTxTime = RfTxOnTime + RfTxOffTime;


                AntPower = new int[128];
                AntEnable = new bool[128];
                AntDwellTime = new int[128];

                for (int i = 0; i < 128; i++)
                {
                    AntEnable[i] = Convert.ToBoolean(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntEnable/Ant" + (i + 1).ToString() + "Enable").InnerText);
                    AntPower[i] = Convert.ToInt32(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntPower/Ant" + (i + 1).ToString() + "Power").InnerText);
                    AntDwellTime[i] = Convert.ToInt32(ConfigSetting.SelectSingleNode
                        ("Configuration/Setting/Function/AntDwellTimeInfo/Ant" + (i + 1).ToString() + "DwellTimeInfo").InnerText);
                }
            }
            catch (Exception)
            {
                CountPanelColumn = 4;
                CountPanelRow = 2;
                MinPanelColumn = 166;
                MinPanelRow = 144;
                MaxPanelColumn = 498;
                MaxPanelRow = 438;

                AntNumFontSize = 16;
                AntNumFont = "맑은 고딕";
                TagCountFontSize = 32;
                TagCountFont = "맑은 고딕";

                for (int i = 0; i < 128; i++)
                {
                    tagDataViews[i].labelAntNum.Font = new System.Drawing.Font(AntNumFont, AntNumFontSize, System.Drawing.FontStyle.Bold);
                    tagDataViews[i].labelTagCount.Font = new System.Drawing.Font(TagCountFont, TagCountFontSize, System.Drawing.FontStyle.Bold);
                }
            }
        }


        private const int INTERVAL_TIMER = 100;

        private void IntializeStopWatchTimer()
        {
            mStopWatchTimer = new System.Windows.Forms.Timer
            {
                Interval = INTERVAL_TIMER
            };
            mStopWatchTimer.Tick += new System.EventHandler(StopWatchTimerTask);
        }

        private void StopWatchTimerTask(object sender, EventArgs e)
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            mTimeInMillisec = timeSpan.TotalMilliseconds - mStartTimeInMillisec;

            Invoke(new Action(delegate ()
            {
                UpdateStopWatchTimer();
            }));

            mStopWatchTimer.Start();
        }

        private void UpdateStopWatchTimer()
        {
            string stopWatchTime = StopWatchTime(mTimeInMillisec);
            labelRfidInventoryElapsedTimeValue.Text = stopWatchTime;// 리딩 타임
        }

        private static string StopWatchTime(double time)
        {
            int timeBase = (int)(time / 100.0);
            int millisec = timeBase % 10;
            timeBase /= 10;
            int seconds = timeBase % 60;
            timeBase /= 60;
            int minutes = timeBase % 60;
            int hours = timeBase / 60;

            return string.Format(
                CultureInfo.CurrentCulture,
                "{0:D2}:{1:D2}:{2:D2}.{3:D}",
                hours,
                minutes,
                seconds,
                millisec);
        }


        //Asyen_22-08-25 : 화면 버퍼 방지
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public virtual void OnReaderDeviceStateChanged(DeviceEvent state)
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "OnReaderDeviceStateChanged() state=" + state);

            switch (state)
            {
                case DeviceEvent.CONNECTED:
                    break;

                case DeviceEvent.DISCONNECTED:
                    Invoke(new Action(delegate ()
                    {
                        if (SharedValues.ReaderConnected)
                        {
                            SharedValues.Reader = null;
                            buttonRfidConnect.Text =
                            Properties.Resources.StringConnect.ToUpper(CultureInfo.CurrentCulture);
                            //EnabledRfidAllControls(false);
                            buttonRfidConnect.Enabled = true;
                            //tabPageRfidInventory.Select();
                            SharedValues.ReaderConnected = false;
                        }

                        //toolStripMenuItemViewModule.Enabled = false;

                        System.Threading.Thread t =
                            new System.Threading.Thread(() =>
                                Popup.Show(Properties.Resources.StringConnectionIsClosed));
                        t.Start();
                    }));
                    break;
            }
        }

        //Asyen : 리더 이벤트 (인벤토리 스타트, 인벤토리 스탑)
        public virtual void OnReaderEvent(int eventId, int result, string data)
        {

            LogUtil.Log(LogUtil.LV_D, D, TAG,
                "OnReaderEvent() eventId=" + eventId
                + ", result=" + result
                + ", data=" + data);

            switch (eventId)
            {
                case Reader.READER_CALLBACK_EVENT_INVENTORY:
                    if (result == RfidResult.SUCCESS)
                    {
                        if ((data != null) && !mRfidTagTraceFormShown)
                        {
                            ProcessRfidTagData(data);
                        }
                    }
                    break;
                case Reader.READER_CALLBACK_EVENT_START_INVENTORY:
                    if (!mRfidInventoryStarted)
                    {
                        Invoke(new Action(delegate ()
                        {
                            StartStopWatchTimer();
                            mRfidInventoryStarted = true;
                            //EnableRfidInventorySettingWidgets(false);
                            ToggleRfidInventoryButton();
                            //EnableTabNavigation(false);
                        }));
                    }
                    break;

                case Reader.READER_CALLBACK_EVENT_STOP_INVENTORY:
                    if (mRfidInventoryStarted)
                    {
                        Invoke(new Action(delegate ()
                        {
                            mRfidInventoryStarted = false;
                            //EnableRfidInventorySettingWidgets(true);
                            //PauseStopWatchTimer();
                            ToggleRfidInventoryButton();
                            //EnableTabNavigation(true);
                        }));
                    }
                    break;
            }
        }

        private void StartStopWatchTimer()
        {
            if (mStopWatchTimer.Enabled)
            {
                mStopWatchTimer.Stop();
            }

            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            mStartTimeInMillisec = timeSpan.TotalMilliseconds;

            mStopWatchTimer.Start();
        }

        public virtual void OnReaderRemoteKeyEvent(int action, int keyCode)
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG,
                "OnReaderRemoteKeyEvent() action=" + action
                + ", keyCode=" + keyCode);

            Invoke(new Action(delegate ()
            {
                if (keyCode == KeyEvent.KEYCODE_SHIFT_RIGHT)
                {
                    if (action == KeyEvent.ACTION_DOWN)
                    {
                        ProcessReaderRemoteKeyDown();
                    }
                    else if (action == KeyEvent.ACTION_UP)
                    {
                        ProcessReaderRemoteKeyUp();
                    }
                }
            }));
        }

        public virtual void OnReaderRemoteSettingChanged(int type, object value)
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG,
                "OnReaderRemoteSettingChanged() type=" + type
                + ", value=" + value);
        }

        private void ProcessReaderRemoteKeyUp()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "ProcessReaderRemoteKeyUp()");

            ProcessInventoryRemoteKeyUp();
        }

        private void ProcessReaderRemoteKeyDown()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "ProcessReaderRemoteKeyDown()");

            ProcessInventoryRemoteKeyDown();
        }

        private async void ProcessInventoryRemoteKeyDown()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "ProcessInventoryRemoteKeyDown()");

            if (mRfidTagTraceFormShown || mRfidTriggerLedFormShown)
            {
                return;
            }

            await ToggleRfidInventory().ConfigureAwait(true);
        }

        private async void ProcessInventoryRemoteKeyUp()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "ProcessInventoryRemoteKeyUp()");

            if (mRfidTagTraceFormShown || mRfidTriggerLedFormShown)
            {
                return;
            }

            if (!mRfidInventoryHoldTriggerEnabled)
            {
                await ToggleRfidInventory().ConfigureAwait(true);
            }
        }

        private void ToggleRfidInventoryButton()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "ToggleRfidInventoryButton()");

            if (buttonRfidInventory.InvokeRequired)
            {
                buttonRfidInventory.Invoke(new MethodInvoker(delegate
                {
                    if (mRfidInventoryStarted)
                    {
                        buttonRfidInventory.Text = Properties.Resources.StringStopInventory.ToUpper(CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        buttonRfidInventory.Text = Properties.Resources.StringStartInventory.ToUpper(CultureInfo.CurrentCulture);
                    }
                }));
            }
            else
            {
                if (mRfidInventoryStarted)
                {
                    buttonRfidInventory.Text = Properties.Resources.StringStopInventory.ToUpper(CultureInfo.CurrentCulture);
                }
                else
                {
                    buttonRfidInventory.Text = Properties.Resources.StringStartInventory.ToUpper(CultureInfo.CurrentCulture);
                }
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Activate();
                }));
            }


            if (buttonRfidInventory.InvokeRequired)
            {
                buttonRfidInventory.Invoke(new MethodInvoker(delegate
                {
                    buttonRfidInventory.Focus();
                }));
            }

        }

        private void UpdateRfidInventorySpeedCountText()
        {
            double value;
            double totalCount = mRfidTagTotalCounter;

            double sec = mTimeInMillisec / 1000;

            if ((totalCount > 0) && ((sec - mOldSec) >= 1))
            {
                value = (double)((int)(((totalCount - mOldTotalCount) / (sec - mOldSec)) * 10)) / 10;
                mOldTotalCount = totalCount;
                mOldSec = sec;
                //Asyen : 리딩 시간 표시
                //labelRfidInventorySpeedValue.Text = value.ToString(CultureInfo.CurrentCulture) + " " + Properties.Resources.StringUnitCountPerSecond;
            }
        }

        private void UpdateRfidInventoryCountText()
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "UpdateRfidInventoryCountText()");

            //Asyen : 모든 태그 개수 업데이트(list Items Count)
            //labelRfidInventoryCounterValue.Text = listViewRfidInventoryTagData.Items.Count.ToString(CultureInfo.CurrentCulture);

            //Asyen : 모든 태그 토탈 개수 업데이트(Total Count)
            //labelRfidInventoryTotalCounterVaue.Text = listViewRfidInventoryTagData.Items.Count.ToString(CultureInfo.CurrentCulture);
        }

        private void ProcessRfidTagData(string data)
        {
            string epc = Properties.Resources.StringEmptyString;
            string rssi = Properties.Resources.StringEmptyString;
            string phase = Properties.Resources.StringEmptyString;
            string fastID = Properties.Resources.StringEmptyString;
            string channel = Properties.Resources.StringEmptyString;
            string port = Properties.Resources.StringEmptyString;

            string[] dataItems = data.Split(';');
            foreach (string dataItem in dataItems)
            {
                if (dataItem.Contains("rssi"))
                {
                    int point = dataItem.IndexOf(':') + 1;
                    rssi = dataItem.Substring(point, dataItem.Length - point);
                }
                else if (dataItem.Contains("phase"))
                {
                    int point = dataItem.IndexOf(':') + 1;
                    phase = dataItem.Substring(point, dataItem.Length - point);
                }
                else if (dataItem.Contains("fastID"))
                {
                    int point = dataItem.IndexOf(':') + 1;
                    fastID = dataItem.Substring(point, dataItem.Length - point);
                }
                else if (dataItem.Contains("channel"))
                {
                    int point = dataItem.IndexOf(':') + 1;
                    channel = dataItem.Substring(point, dataItem.Length - point);
                }
                else if (dataItem.Contains("antenna"))
                {
                    int point = dataItem.IndexOf(':') + 1;
                    //Asyen : port = dataItem.Substring(point, dataItem.Length - point);

                    //Asyen : antenna:0 -> string 0 추출해서 + 1 
                    int portplus = Convert.ToInt32(dataItem.Substring(point, dataItem.Length - point)) + 1;
                    port = portplus.ToString();
                }
                else
                {
                    epc = dataItem;
                }
            }

            LogUtil.Log(LogUtil.LV_D, D, TAG,
                "ProcessRfidTagData() epc=" + epc + " rssi=" + rssi + " phase=" + phase + " fastID=" + fastID + " channel=" + channel + " port=" + port);

            if (!Visible)
            {
                string injectionString = Properties.Resources.StringEmptyString;
                if (SharedValues.HidSettingPrefix != null)
                {
                    injectionString = SharedValues.HidSettingPrefix;
                }

                injectionString += epc;

                if (SharedValues.HidSettingSuffix != null)
                {
                    injectionString += SharedValues.HidSettingSuffix;
                }

                if (SharedValues.HidSettingTerminator != HidTerminator.NONE)
                {
                    if (SharedValues.HidSettingTerminator == HidTerminator.CR)
                    {
                        injectionString += "\n";
                    }
                    else if (SharedValues.HidSettingTerminator == HidTerminator.CRLR)
                    {
                        injectionString += "\n\r";
                    }
                    else if (SharedValues.HidSettingTerminator == HidTerminator.LRCR)
                    {
                        injectionString += "\r\n";
                    }
                }

                Invoke(new Action(delegate ()
                {
                    KeyInput.SendKeyByClipboard(injectionString);
                }));

                return;
            }

            Invoke(new Action(delegate ()
            {
                AddTagItem(epc, rssi, phase, fastID, channel, port, mRfidInventoryFilterEnabled);
            }));

            Invoke(new Action(delegate ()
            {
                TagBufferTotalSum(epc, rssi, phase, fastID, channel, port);
            }));

            Invoke(new Action(delegate ()
            {
                EventAntTagItem(epc, rssi, phase, fastID, channel, port);
            }));

            /*Invoke(new Action(delegate ()
            {
                CheckTagItem(epc, rssi, port);
            }));*/

            mRfidTagTotalCounter++;

            if (mRfidInventoryStarted)
            {
                Invoke(new Action(delegate ()
                {
                    UpdateRfidInventoryCountText();             //Asyen : 리딩된 토탈 카운트 
                    //UpdateRfidInventorySpeedCountText();        //Asyen : 리딩된 속도
                    //UpdateRfidInventoryAvrSpeedCountText();     //Asyen : 리딩된 속도 (평균) 
                }));
            }
        }

        private void AddTagItem(string epc,
                                string rssi,
                                string phase,
                                string fastID,
                                string channel,
                                string port,
                                bool filterEanbled)
        {
            LogUtil.Log(LogUtil.LV_D, D, TAG, "AddTagItem() epc=" + epc +
                                              " rssi=" + rssi +
                                              " phase=" + phase +
                                              " fastID=" + fastID +
                                              " channel=" + channel +
                                              " port=" + port +
                                              " filterEnabled=" + filterEanbled);

            mTagItemList.AddItem(epc, rssi, phase, fastID, channel, port, filterEanbled);

            string phaseDegree = Properties.Resources.StringEmptyString.Equals(phase, StringComparison.CurrentCulture) ?
                Properties.Resources.StringEmptyString : phase + "˚";

            //Asyen : 다른 안테나로 읽으면 리스트에 새로운 안테나 포트를 추가 (기존엔 없었던 코드)
            bool CheckEpc = true;

        }

        private void EventAntTagItem(string epc,
                                string rssi,
                                string phase,
                                string fastID,
                                string channel,
                                string port)
        {
            listViewAntTagData.Items.Clear();
            if (labelEventAntNum.Text != "0")
            {
                if (g_TagBufferTotalData[Int32.Parse(labelEventAntNum.Text) - 1, 0] != null)
                {
                    for (int i = 0; i < g_TagBufferTotalData[Int32.Parse(labelEventAntNum.Text) - 1, 0].Length; i++)
                    {
                        string[] items = new string[2];
                        items[0] = g_TagBufferTotalData[Int32.Parse(labelEventAntNum.Text) - 1, 0][i];
                        items[1] = g_TagBufferTotalData[Int32.Parse(labelEventAntNum.Text) - 1, 1][i];
                        ListViewItem item = new ListViewItem(items);

                        listViewAntTagData.BeginUpdate();
                        listViewAntTagData.Items.Add(item);
                        listViewAntTagData.EndUpdate();
                    }
                }
            }
        }

        private int CurentPort = 0;
        private string workerId = "anonymous";
        private bool workercheck = false;
        private bool TagReadCheck = false;
        private string CurPort = string.Empty;
        private string CurEpc = string.Empty;

        private void TagBufferTotalSum(string epc,
                                string rssi,
                                string phase,
                                string fastID,
                                string channel,
                                string port)
        {

            // 네트워크 모드일때 
            // 태그 리스트 조회해서 등록안되어있으면 리턴
            if (NetWorkModeCheck)
            {
                bool result = TagList.Contains(epc);

                if (!result)
                    return;
            }

            // Rssi로 거리조절이 애매함 캐비넷에 읽혔을 경우의 Rssi와 캐비넷 밖에서 읽혔을 경우의 Rssi가 같은 경우가 생김
            bool ConvertCheck = float.TryParse(rssi, out float distance);

            if (RssiCheck)
            {
                if (ConvertCheck)
                {
                    if (-1 * distance > RssiValue)
                        return;
                }
            }

            if (!workercheck)
            {
                CurPort = port;
                CurEpc = epc;

                // 태그가 읽혔는지 체크
                TagReadCheck = true;

                CurentPort = Convert.ToInt32(port) - 1;
                g_TagTimerAddCount = 0;

                g_TagCurAntNo = Int32.Parse(port) - 1; //배열에 사용 용도의 포트 값 저장(안테나1 -> 0, 안테나 128 -> 127)

                g_TagTimerAntNo = g_TagCurAntNo;//타이머안테나넘버를 현재 읽은 안테나 넘버로 동기화

                UpdateList(DeviceName, port, epc, rssi);

                //CompareCabinet(epc);
                //TagTotalCompareRssi(epc, rssi, CurentPort);
                bool result = TagCompareRssi(epc, rssi, CurentPort);
                if (!result)
                    return;

                int TagCurExtCHK = TagBufferDataCheck(epc);//현재 읽힌 안테나에서 중복값이 있는지 체크
                TagCurBufferUpdate(TagCurExtCHK, epc, rssi);//개별 버퍼에 업데이트

                TagTotalBufferUpdate(g_TagCurAntNo);//토탈 버퍼에 업데이트

                TagTotalCount();//모든 태그의 토탈 카운트 체크

            }
            workercheck = false;
        }


        private void UpdateList(string deviceId, string Port, string Epc, string Rssi)
        {
            string[] items = new string[5];
            items[0] = deviceId;
            items[1] = Port;
            items[2] = Epc;
            items[3] = Rssi;
            items[4] = DateTime.Now.ToString();
            ListViewItem item = new ListViewItem(items);

            listViewTagDataView.BeginUpdate();
            listViewTagDataView.Items.Add(item);
            listViewTagDataView.EndUpdate();
        }



        private void SkipTagBufferTotalSum(int SkipPort)
        {
            // 업데이트 하기 전 기존 토탈 데이터 값을 백업해둠
            List<string> BackUpTagTatalLists = new List<string>();

            // 여기서 대상 포트에 대한 백업 데이터가 
            if (g_TagBufferTotalData[SkipPort, 0] != null)
            {
                //  존재하면
                //  
                BackUpTagTatalLists = new List<string>(g_TagBufferTotalData[SkipPort, 0]);
            }
            // 존재하지 않으면 
            TagTotalBufferUpdate(SkipPort);//토탈 버퍼에 업데이트

            //TagTotalCheckInOut(BackUpTagTatalLists, SkipPort);
            //TagCheckInOut();
        }

        // 날짜 비교를 위한 타임
        private string Beforetime = null;
        private void DateCheck()
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd");

            if (Beforetime != date)
            {
                inputCount = 0;
                outputCount = 0;
            }

            Beforetime = date;

            return;
        }

        private string datetime;

        private void CompareCabinet(string epc)
        {
            // 현재 태그가 읽혔는데, 이 태그가 다른 선반에도 있는지 검사
            // 안테나 수만큼
            for (int i = 0; i < AntCount; i++)
            {
                //  동일 선반이 아닐때
                if (i != g_TagCurAntNo)
                {
                    // 토탈 카운트가 null이 아니라면
                    if (g_TagBufferTotalData[i, 0] != null)
                    {
                        // 버퍼에 현재 읽은 태그가 있는지 검사
                        int index = Array.IndexOf(g_TagBufferTotalData[i, 0], epc);
                        // 있다면
                        if (index != -1)
                        {
                            // 토탈 버퍼에 태그 값 지워주고
                            List<string> tmpTotalBuffer = new List<string>(g_TagBufferTotalData[i, 0]);
                            tmpTotalBuffer.RemoveAt(index);
                            g_TagBufferTotalData[i, 0] = tmpTotalBuffer.ToArray();

                            tmpTotalBuffer = new List<string>(g_TagBufferTotalData[i, 1]);
                            tmpTotalBuffer.RemoveAt(index);
                            g_TagBufferTotalData[i, 1] = tmpTotalBuffer.ToArray();

                            // 각 버퍼에 태그 값 지워준다.
                            for (int j = 0; j < BufferSettingLength[CurentPort]; j++)
                            {
                                if (g_TagBufferData[i, j, 0] != null)
                                {
                                    if (g_TagBufferData[i, j, 0].Length != 0)
                                    {
                                        index = Array.IndexOf(g_TagBufferData[i, j, 0], epc);
                                        if (index != -1)
                                        {
                                            tmpTotalBuffer = new List<string>(g_TagBufferData[i, j, 0]);
                                            tmpTotalBuffer.RemoveAt(index);
                                            g_TagBufferData[i, j, 0] = tmpTotalBuffer.ToArray();

                                            tmpTotalBuffer = new List<string>(g_TagBufferData[i, j, 1]);
                                            tmpTotalBuffer.RemoveAt(index);
                                            g_TagBufferData[i, j, 1] = tmpTotalBuffer.ToArray();

                                            if (g_TagBufferTotalData[i, 0].Length == 0)
                                            {
                                                LVDebugMode.Items[i].SubItems[j + 1].Text = "0";
                                            }
                                            else
                                                LVDebugMode.Items[i].SubItems[j + 1].Text = g_TagBufferTotalData[i, 0].Length.ToString();


                                            if (workerId != null)
                                            {
                                                outputCount++;
                                                stockCount--;
                                                Debug.WriteLine(workerId + "님이 " + epc + "를 출고하셨습니다.");
                                            }
                                            datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                                            tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                                        }
                                    }

                                }
                            }
                            DateCheck();

                            LVDebugMode.Items[i].SubItems[11].Text = g_TagBufferTotalData[i, 0].Length.ToString();
                            //tagDataViews[i].labelTagCount.Text = g_TagBufferTotalData[i, 0].Length.ToString();
                        }
                    }
                }
            }
        }


        private bool TagCompareRssi(string epc, string rssi, int curentPort)
        {
            for (int i = 0; i < AntCount; i++)
            {
                if (curentPort != i)
                {
                    if (g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0] != null)
                    {
                        // 만약 현재 버퍼에 동일 태그가 있다면
                        if (g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Contains(epc))
                        {
                            int index = Array.IndexOf(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0], epc);

                            // Rssi 비교
                            if ((Convert.ToDouble(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 1][index]) < Convert.ToDouble(rssi)))
                            {
                                // 현재 rssi가 더 크다면
                                // 기존 버퍼에 있던거 지워주자
                                List<string> tmpTagBuffer = new List<string>(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0]);
                                tmpTagBuffer.RemoveAt(index);
                                g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0] = tmpTagBuffer.ToArray();

                                return true;
                            }
                            else
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        // CompareRssi
        private bool TagCompareRssi1(string epc, string rssi, int curentPort)
        {
            string[,,][] g_NewTagBufferData = new string[128, 11, 3][];
            string CompareRssi1 = string.Empty;
            string CompareRssi2 = string.Empty;
            string CompareRssi = string.Empty;
            int index1 = 0;
            int index2 = 0;
            int index;

            // 안테나 개수 만큼
            for (int i = 0; i < AntCount; i++)
            {
                if (curentPort != i)
                {
                    bool CompareCheck = false;
                    // 각 포트의 버퍼들을 순환한다.
                    for (int j = 0; j < 2; j++)
                    {
                        if (g_TagBufferData[i, j, 0] != null)
                        {
                            //만약 현재 읽은 태그와 동일 태그가 다른 포트에도 있는지 확인한다.
                            if (g_TagBufferData[i, j, 0].Contains(epc))
                            {
                                // 잇다면 CompareCheck를 true
                                CompareCheck = true;
                            }
                        }
                    }

                    if (CompareCheck)
                    {
                        int CheckCount = 0;
                        // 각 버퍼에 동일 값이 모두 존재하는지 체크
                        for (int k = 0; k < 2; k++)
                        {
                            if (g_TagBufferData[i, k, 0] != null)
                            {
                                if (g_TagBufferData[i, k, 0].Contains(epc))
                                {
                                    CheckCount++;
                                }
                                else
                                    break;
                            }
                        }
                        // CheckCount 2면 모든 버퍼에 존재한다는 뜻
                        if (CheckCount == 2)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                // 버퍼의 데이터 카운트 만큼 돌면서 epc값을 찾아냄
                                if (j == 0)
                                {
                                    index1 = Array.IndexOf(g_TagBufferData[i, j, 0], epc);
                                    CompareRssi1 = g_TagBufferData[i, j, 0][index1];
                                }
                                else
                                {
                                    index2 = Array.IndexOf(g_TagBufferData[i, j, 0], epc);
                                    CompareRssi2 = g_TagBufferData[i, j, 0][index2];
                                }
                            }

                            if (Convert.ToDouble(CompareRssi1) < Convert.ToDouble(CompareRssi2))
                            {
                                index = index2;
                                CompareRssi = CompareRssi2;
                            }
                            else
                            {
                                index = index1;
                                CompareRssi = CompareRssi1;
                            }

                            // 현재 Rssi랑 비교
                            if (Convert.ToDouble(CompareRssi) < Convert.ToDouble(rssi))
                            {
                                // 현재 Rssi가 더 크다면 (더 좁은거리)
                                // 기존 버퍼에 있던 값들 다 지워주기
                                List<string> tmpTagBuffer = new List<string>(g_TagBufferData[i, 0, 0]);
                                tmpTagBuffer.RemoveAt(index);
                                g_TagBufferData[i, 0, 0] = tmpTagBuffer.ToArray();

                                tmpTagBuffer = new List<string>(g_TagBufferData[i, 0, 1]);
                                tmpTagBuffer.RemoveAt(index);
                                g_TagBufferData[i, 0, 1] = tmpTagBuffer.ToArray();

                                tmpTagBuffer = new List<string>(g_TagBufferData[i, 1, 0]);
                                tmpTagBuffer.RemoveAt(index);
                                g_TagBufferData[i, 1, 0] = tmpTagBuffer.ToArray();

                                tmpTagBuffer = new List<string>(g_TagBufferData[i, 1, 1]);
                                tmpTagBuffer.RemoveAt(index);
                                g_TagBufferData[i, 1, 1] = tmpTagBuffer.ToArray();

                                tagDataViews[i].labelTagCount.Text = g_TagBufferData[i, 1, 0].Length.ToString();
                                tagDataViews[i].labelStateOUT.BackColor = Color.Blue;

                                if (NetWorkModeCheck)
                                {
                                    RequestJSON2(DeviceId, "anonymous", i + 1, epc, --Count[i], 0, ++OutputCount[i]);
                                }

                                return true;
                            }
                            else
                                return false;
                        }
                        // CheckCount 0이거나 1이면 하나의 버퍼에 존재한다는 뜻
                        if (CheckCount == 0 || CheckCount == 1)
                        {
                            // 첫번째 버퍼에 값이 있다는 뜻
                            if (CheckCount == 1)
                            {
                                index = Array.IndexOf(g_TagBufferData[i, 0, 0], epc);
                                CompareRssi = g_TagBufferData[i, 0, 1][index];

                                if (Convert.ToDouble(CompareRssi) < Convert.ToDouble(rssi))
                                {
                                    // 현재 Rssi가 더 크다면 (더 좁은거리)
                                    // 기존 버퍼에 있던 값들 다 지워주기

                                    List<string> tmpTagBuffer = new List<string>(g_TagBufferData[i, 0, 0]);
                                    tmpTagBuffer.RemoveAt(index);
                                    g_TagBufferData[i, 0, 0] = tmpTagBuffer.ToArray();

                                    tmpTagBuffer = new List<string>(g_TagBufferData[i, 0, 1]);
                                    tmpTagBuffer.RemoveAt(index);
                                    g_TagBufferData[i, 0, 1] = tmpTagBuffer.ToArray();

                                    tagDataViews[i].labelTagCount.Text = g_TagBufferData[i, 0, 0].Length.ToString();
                                    tagDataViews[i].labelStateOUT.BackColor = Color.Blue;

                                    if (NetWorkModeCheck)
                                    {
                                        RequestJSON2(DeviceId, "anonymous", i + 1, epc, --Count[i], 0, ++OutputCount[i]);
                                    }

                                    return true;
                                }
                                else
                                    return false;
                            }
                            // 두번째 버퍼에 값이 있다는 뜻
                            if (CheckCount == 0)
                            {
                                index = Array.IndexOf(g_TagBufferData[i, 1, 0], epc);
                                CompareRssi = g_TagBufferData[i, 1, 1][index];

                                if (Convert.ToDouble(CompareRssi) < Convert.ToDouble(rssi))
                                {
                                    // 현재 Rssi가 더 크다면 (더 좁은거리)
                                    // 기존 버퍼에 있던 값들 다 지워주기
                                    List<string> tmpTagBuffer = new List<string>(g_TagBufferData[i, 1, 0]);
                                    tmpTagBuffer.RemoveAt(index);
                                    g_TagBufferData[i, 1, 0] = tmpTagBuffer.ToArray();

                                    tmpTagBuffer = new List<string>(g_TagBufferData[i, 1, 1]);
                                    tmpTagBuffer.RemoveAt(index);
                                    g_TagBufferData[i, 1, 1] = tmpTagBuffer.ToArray();


                                    if (NetWorkModeCheck)
                                    {
                                        RequestJSON2(DeviceId, "anonymous", i + 1, epc, --Count[i], 0, ++OutputCount[i]);
                                    }

                                    tagDataViews[i].labelTagCount.Text = g_TagBufferData[i, 1, 0].Length.ToString();
                                    tagDataViews[i].labelStateOUT.BackColor = Color.Blue;

                                    return true;
                                }
                                else
                                    return false;
                            }

                        }

                    }
                    else
                        return true;
                }
            }
            return false;
        }


        private bool TagTotalCompareRssi(string epc, string rssi, int TagCurAntNo)
        {
            bool Testbool = true;

            List<int> SaveAntNo = new List<int>();

            for (int i = 0; i < AntCount; i++)
            {
                if (i != TagCurAntNo)
                {
                    if (g_TagBufferTotalData[i, 0] != null)
                    {
                        int index = Array.IndexOf(g_TagBufferTotalData[i, 0], epc);

                        if (index != -1)//있을 때
                        {
                            SaveAntNo.Add(i + 1);

                            if (Convert.ToDouble(g_TagBufferTotalData[i, 1][index]) < Convert.ToDouble(rssi))
                            {
                                List<string> tmpTotalBuffer = new List<string>(g_TagBufferTotalData[i, 0]);
                                tmpTotalBuffer.RemoveAt(index);
                                g_TagBufferTotalData[i, 0] = tmpTotalBuffer.ToArray();

                                tmpTotalBuffer = new List<string>(g_TagBufferTotalData[i, 1]);
                                tmpTotalBuffer.RemoveAt(index);
                                g_TagBufferTotalData[i, 1] = tmpTotalBuffer.ToArray();

                                for (int j = 0; j < BufferSettingLength[CurentPort]; j++)
                                {
                                    if (g_TagBufferData[i, j, 0] != null)
                                    {
                                        if (g_TagBufferData[i, j, 0].Length != 0)
                                        {
                                            index = Array.IndexOf(g_TagBufferData[i, j, 0], epc);
                                            if (index != -1)
                                            {
                                                tmpTotalBuffer = new List<string>(g_TagBufferData[i, j, 0]);
                                                tmpTotalBuffer.RemoveAt(index);
                                                g_TagBufferData[i, j, 0] = tmpTotalBuffer.ToArray();

                                                tmpTotalBuffer = new List<string>(g_TagBufferData[i, j, 1]);
                                                tmpTotalBuffer.RemoveAt(index);
                                                g_TagBufferData[i, j, 1] = tmpTotalBuffer.ToArray();

                                                if (g_TagBufferTotalData[i, 0].Length == 0)
                                                {
                                                    LVDebugMode.Items[i].SubItems[j + 1].Text = "0";
                                                }
                                                else
                                                    LVDebugMode.Items[i].SubItems[j + 1].Text = g_TagBufferTotalData[i, 0].Length.ToString();

                                                if (NetWorkModeCheck)
                                                {
                                                    //++OutputCount[i];
                                                    //--Count[i];
                                                    RequestJSON2(DeviceId, "anonymous", i + 1, epc, --Count[i], 0, ++OutputCount[i]);
                                                    //InputCount--;
                                                    //Thread.Sleep(50);
                                                }
                                                tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                                            }
                                        }

                                    }
                                }
                                DateCheck();

                                LVDebugMode.Items[i].SubItems[11].Text = g_TagBufferTotalData[i, 0].Length.ToString();
                                //tagDataViews[i].labelTagCount.Text = g_TagBufferTotalData[i, 0].Length.ToString();

                            }
                            else
                            {
                                Testbool = false;
                            }
                        }
                    }
                }
            }


            if (SaveAntNo.Count > 0)
            {
                string SaveAntData = " ";
                for (int i = 0; i < SaveAntNo.Count; i++)
                {
                    SaveAntData = SaveAntData + (SaveAntNo[i]).ToString() + " ";
                }


            }


            return Testbool;
        }

        private int TagBufferDataCheck(string epc)
        {
            int TagCurExtCHK = -1;

            if (g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0] != null)
            {
                TagCurExtCHK = Array.IndexOf(g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0], epc);//중복 값 체크
            }

            return TagCurExtCHK;
        }


        private void TagCurBufferUpdate(int TagCurExtCHK, string epc, string rssi)
        {
            if (epc == null)
            {
                return;
            }

            if (TagCurExtCHK == -1)//배열 내에 같은 값이 없다면
            {
                g_TagBufferDataLength = 0;

                //현재 배열의 크기 저장
                if (g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0] != null)
                {
                    g_TagBufferDataLength = g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0].Length;
                }

                for (int i = 0; i < 3; i++)//배열 크기 변경 ()
                {
                    Array.Resize(ref g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], i],
                    g_TagBufferDataLength + 1);
                }

                g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0][g_TagBufferDataLength] = epc;//epc 저장

                g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 1][g_TagBufferDataLength] = rssi;//rssi저장

                //tagDataViews[g_TagCurAntNo].labelTagCount.Text = g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0].Length.ToString();
            }
            else
            {
                if (Convert.ToDouble(g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 1][TagCurExtCHK]) <
                    Convert.ToDouble(rssi))
                {
                    //저장 되어있는 동일한 태그 값의 rssi가 지금 읽힌 rssi 보다 작다면 
                    g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 1][TagCurExtCHK] = rssi;
                }
            }

            LVDebugMode.Items[g_TagCurAntNo].SubItems[g_TagBufferCurLocationArray[CurentPort] + 1].Text
             = g_TagBufferData[g_TagCurAntNo, g_TagBufferCurLocationArray[CurentPort], 0].Length.ToString();

        }

        private void TagTotalBufferUpdate(int TagCurAntNo)
        {

            Array.Resize(ref g_TagBufferTotalData[TagCurAntNo, 0], 0);
            Array.Resize(ref g_TagBufferTotalData[TagCurAntNo, 1], 0);

            // 버퍼 총 길이만큼 
            for (int i = 0; i < BufferSettingLength[CurentPort]; i++)
            {
                // 각 버퍼에 태그 데이터가 존재하면
                if (g_TagBufferData[TagCurAntNo, i, 0] != null)
                {
                    // 각 버퍼에 태그 데이터 개수 만큼 
                    for (int j = 0; j < g_TagBufferData[TagCurAntNo, i, 0].Length; j++)
                    {
                        TotalDataUpdate(i, j, TagCurAntNo);// i -> 몇번째 버퍼 ?백업 데이터?, j -> 백업 데이터의 길이
                    }
                }
            }

            //tagDataViews[TagCurAntNo].labelTagCount.Text = g_TagBufferData[TagCurAntNo, g_TagBufferCurLocationArray[TagCurAntNo], 0].Length.ToString(); //g_TagBufferTotalData[TagCurAntNo, 0].Length.ToString();
            LVDebugMode.Items[TagCurAntNo].SubItems[11].Text = g_TagBufferTotalData[TagCurAntNo, 0].Length.ToString() + " OK";

        }

        private void TotalDataUpdate(int i, int j, int TagCurAntNo)//TagTotalBufferUpdate에 선언된 함수 (이름 고민)
        {

            int index = Array.IndexOf(g_TagBufferTotalData[TagCurAntNo, 0], g_TagBufferData[TagCurAntNo, i, 0][j]);

            // 해당 안테나에 대한 전체 데이타 중 찾는 버퍼 데이타가 존재하지 않을 경우  
            if (index == -1)
            {
                Array.Resize(ref g_TagBufferTotalData[TagCurAntNo, 0],
                    g_TagBufferTotalData[TagCurAntNo, 0].Length + 1);

                g_TagBufferTotalData[TagCurAntNo, 0][g_TagBufferTotalData[TagCurAntNo, 0].Length - 1]
                    = g_TagBufferData[TagCurAntNo, i, 0][j];

                Array.Resize(ref g_TagBufferTotalData[TagCurAntNo, 1],
                    g_TagBufferTotalData[TagCurAntNo, 1].Length + 1);

                g_TagBufferTotalData[TagCurAntNo, 1][g_TagBufferTotalData[TagCurAntNo, 1].Length - 1]
                    = g_TagBufferData[TagCurAntNo, i, 1][j];
            }
            else
            {
                if (Convert.ToDouble(g_TagBufferTotalData[TagCurAntNo, 1][index])
                    < Convert.ToDouble(g_TagBufferData[TagCurAntNo, i, 1][j]))
                {
                    g_TagBufferTotalData[TagCurAntNo, 1][index] = g_TagBufferData[TagCurAntNo, i, 1][j];
                }
            }
        }

        private int stockCount = 0;
        private int inputCount = 0;
        private int outputCount = 0;
        private int TotalCount = 0;

        private void TagTotalCount()
        {
            TotalCount = 0;

            for (int i = 0; i < AntCount; i++)
            {
                if (g_TagBufferTotalData[i, 0] != null)
                {
                    TotalCount = TotalCount + g_TagBufferTotalData[i, 0].Length;
                }
            }

        }

        private void timerLocationNoCheck_Tick(object sender, EventArgs e)
        {

            if (g_TagCurAntNo + 1 >= AntCount)
            {
                g_TagCurAntNo = 0;
            }
            else
            {
                g_TagCurAntNo++;
            }

        }

        private int[] savecurentarray = new int[128];
        private bool CycleCheck = false;
        private bool TimerCheck = false;

        private void timerAntNoCheck_Tick(object sender, EventArgs e)
        {
            savecurentarray[CurentPort] = g_TagBufferCurLocationArray[CurentPort];

            for (int i = 0; i < AntCount; i++)
            {
                LVDebugMode.Items[i].SubItems[13].Text = BufferSettingLength[i].ToString();
            }


            // Table View
            if (tableModeToolStripMenuItem.Checked)
            {
                for (int i = 0; i < AntCount; i++)
                {
                    tagDataViews[i].labelAntNum.ForeColor = Color.MediumBlue;
                }
                tagDataViews[g_TagTimerAntNo].labelAntNum.ForeColor = Color.Lime;
            }



            for (int i = 0; i < AntCount; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    LVDebugMode.Items[i].SubItems[j].BackColor = Color.White;
                }
            }

            if (CycleCheck && TimerCheck)
            {
                if (g_TagCurAntNo - 1 > g_TagTimerAntNo)
                {
                    if (g_TagBufferCurLocationArray[g_TagTimerAntNo] + 2 <= BufferSettingLength[g_TagTimerAntNo])
                    {
                        LVDebugMode.Items[g_TagTimerAntNo].SubItems[g_TagBufferCurLocationArray[CurentPort] + 2].BackColor = Color.Lime;
                        LVDebugMode.EnsureVisible(g_TagTimerAntNo);
                    }
                    else
                    {
                        LVDebugMode.Items[g_TagTimerAntNo].SubItems[1].BackColor = Color.Lime;
                        LVDebugMode.EnsureVisible(g_TagTimerAntNo);
                    }
                }
                else
                {
                    LVDebugMode.Items[g_TagTimerAntNo].SubItems[g_TagBufferCurLocationArray[CurentPort] + 1].BackColor = Color.Lime;
                    LVDebugMode.EnsureVisible(g_TagTimerAntNo);
                }

            }

            if (TimerCheck)
            {
                g_TagTimerAntNo++;
                g_TagTimerAddCount++;
                CurentPort++; // 이벤트가 돌지 않을 때 다음 포트로 넘어가야함으로
            }

            if (g_TagTimerAntNo > AntCount - 1)// 안테나 타이머 카운트가 안테나 개수를 넘었을때 
            {
                CycleCheck = false;
                TimerCheck = false;
                g_TagTimerAntNo = 0;

            }


            if (CurentPort > AntCount - 1)
            {
                CurentPort = 0;
            }

        }


        private string SavePortData = null;
        private ListView RealTimeTagDatasEpc = new ListView();
        public bool UpdateTagLog(DateTime Time, string Reader, int Ant, string Tag, string Ip_Address, string State)//Asyen : State -> 0
        {
            if (ExecuteProcNonQuery( //State : 1 -> 0
                        ConnectMariaDB,
                        "USP_SET_UPDATE_TAG_LOG",
                        new IDataParameter[]
                        {
                            new MySqlParameter("@P_UPDATE_TIME", Time), //2022-06-10 10:26:33
                            new MySqlParameter("@P_READER", Reader), //a211
                            new MySqlParameter("@P_ANT", (int)Ant), //1
                            new MySqlParameter("@P_TAG", Tag), //300020220720000000000001
                            new MySqlParameter("@P_IP_ADDRESS", Ip_Address),
                            new MySqlParameter("@P_STATE", State)
                        }) <= 0)
            {
                return false; // DB 등록 실패 

            }
            return true; // DB 등록 성공
        }



        public bool SaveTagLog(DateTime Time, string Reader, int Ant, string Tag, string Ip_Address, string State)//Asyen : State -> 0
        {
            if (ExecuteProcNonQuery(// Insert Tag
                        ConnectMariaDB,
                        "USP_SET_INSERT_TAG_LOG",
                        new IDataParameter[]
                        {
                            new MySqlParameter("@P_CREATE_TIME", Time), //2022-06-10 10:26:33
                            new MySqlParameter("@P_READER", Reader), //a211
                            new MySqlParameter("@P_ANT", (int)Ant), //1
                            new MySqlParameter("@P_TAG", Tag), //300020220720000000000001
                            new MySqlParameter("@P_IP_ADDRESS", Ip_Address),
                            new MySqlParameter("@P_STATE", State)
                        }) <= 0)
            {
                return false; // DB 등록 실패 

            }
            return true; // DB 등록 성공
        }

        public int ExecuteProcNonQuery(string connStr, string procName, IDataParameter[] param)
        {
            DbConnection conn;
            DbCommand cmd;

            int result = 0;

            Task.Run(() =>
            {
                try
                {
                    using (conn = new MySqlConnection(connStr))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            return -1;
                        }
                        using (cmd = new MySqlCommand(procName, (MySqlConnection)conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (param != null)
                            {
                                foreach (IDataParameter p in param)
                                    cmd.Parameters.Add(p);
                            }
                            result = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        conn.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return result;
            });

            return 1;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tagDataViewsSetting();
            tableLayoutPanelTagDataViewSetting(CountPanelColumn, CountPanelRow);
            SaveDoubleBuffered();
            /*
            // 네트워크모드, 일반모드 선택 폼
            using (SelectModeForm form = new SelectModeForm())
            {
                form.ModeSelection_NetWork += new SelectModeForm.NetWorkMode(NetWorkMode);
                form.ModeSelection_Nomal += new SelectModeForm.NomalMode(NomalMode);

                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
            */
        }

        private bool NetWorkModeCheck = false;
        public string DeviceName;
        public string DeviceId;
        public string ApiUri;
        public string GsUri;

        private void LoadDeviceItem(string DeviceName, string DeviceId, string ApiUri, string GsUri, bool NetWorkCheck)
        {
            this.DeviceName = DeviceName;
            this.DeviceId = DeviceId;
            this.ApiUri = ApiUri;
            this.GsUri = GsUri;
            this.NetWorkModeCheck = NetWorkCheck;

            // DB에 디바이스 ID가 등록된게 맞는지 확인
            DeviceIdRequest();
        }



        //Asyen_22-08-25 : tagDataView 마다 기본 값 setting
        private void tagDataViewsSetting()
        {

            for (int i = 0; i < tagDataViews.Length; i++)
            {
                tagDataViews[i].labelAntNum.Text = Convert.ToString(i + 1);
                tagDataViews[i].labelTagCount.Text = "0";
            }
        }

        //Asyen_22-08-25 : tableLayoutPanelTagDataView의 크기 재설정
        private void tableLayoutPanelTagDataViewSetting(int Column, int Row)
        {
            int num = 0;
            int ColumnSize = Column * MinPanelColumn;
            int RowSize = Row * MinPanelRow;

            tableLayoutPanelTagDataView.ColumnCount = Column;
            tableLayoutPanelTagDataView.RowCount = Row;

            this.tableLayoutPanelTagDataView.Size = new System.Drawing.Size(ColumnSize, RowSize);

            for (int i = 0; i < Row; i++)
            {
                tableLayoutPanelTagDataView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                for (int j = 0; j < Column; j++)
                {
                    tableLayoutPanelTagDataView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

                    tagDataViews[num].Dock = System.Windows.Forms.DockStyle.Fill;
                    tagDataViews[num].Visible = true;
                    tableLayoutPanelTagDataView.Controls.Add(tagDataViews[num], j, i);
                    num++;
                }
            }

            for (int i = num; i < tagDataViews.Length; i++)
            {
                tagDataViews[i].Dock = System.Windows.Forms.DockStyle.None;
                tagDataViews[i].Visible = false;
            }
        }

        private void SaveDoubleBuffered()
        {
            listViewAntTagData.DoubleBuffered(true);
            LVDebugMode.DoubleBuffered(true);
        }

        //Asyen_22-08-25 : 태그 View 클릭 이벤트
        private void tagDataViews_Click(object sender, EventArgs e)
        {
            int EventAntNum = Convert.ToInt32(((Control)sender).Parent.Name.Substring(11));
            labelEventAntNum.Text = EventAntNum.ToString();
            listViewAntTagData.Items.Clear();

            if (g_TagBufferTotalData[EventAntNum - 1, 0] != null)
            {
                for (int i = 0; i < g_TagBufferTotalData[EventAntNum - 1, 0].Length; i++)
                {
                    string[] items = new string[2];
                    items[0] = g_TagBufferTotalData[EventAntNum - 1, 0][i];
                    items[1] = g_TagBufferTotalData[EventAntNum - 1, 1][i];
                    ListViewItem item = new ListViewItem(items);

                    listViewAntTagData.BeginUpdate();
                    listViewAntTagData.Items.Add(item);
                    listViewAntTagData.EndUpdate();
                }
            }
        }

        private void tableLayoutPanelTagDataView_Resize(object sender, EventArgs e)
        {
            InitializeLoadConfig();
            InitializeCreateConfig();
            InitializeSetting();
            FormResize();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FormResize();
        }

        private void FormResize() //Asyen_22-08-24 : 폼 사이즈 변화에 따른 크기 재설정
        {
            int ColumnCount = CountPanelColumn;             //Asyen_22-08-24 : 패널의 가로 개수
            int RowCount = CountPanelRow;                   //Asyen_22-08-24 : 패널의 세로 개수

            int ColumnSize = MinPanelColumn * ColumnCount;  //Asyen_22-08-24 : 패널의 가로 사이즈
            int RowSize = MinPanelRow * RowCount;           //Asyen_22-08-24 : 패널의 세로 사이즈

            if (panelTagDataView.Width / ColumnSize <= panelTagDataView.Height / RowSize)
            {
                if (ColumnSize < panelTagDataView.Width)
                {
                    if (panelTagDataView.Width / ColumnCount > MaxPanelColumn)
                    {
                        tableLayoutPanelTagDataView.Width = MaxPanelColumn * ColumnCount;
                    }
                    else
                    {
                        tableLayoutPanelTagDataView.Width = panelTagDataView.Width - 50;
                    }

                    tableLayoutPanelTagDataView.Height = tableLayoutPanelTagDataView.Width * RowSize / ColumnSize;
                }
                else
                {
                    if (MinPanelColumn <= panelTagDataView.Width / ColumnCount)
                    {
                        ColumnSize = panelTagDataView.Width / ColumnCount;

                        if (panelTagDataView.Width / ColumnCount > MaxPanelColumn)
                        {
                            tableLayoutPanelTagDataView.Width = MaxPanelColumn * ColumnCount;
                        }
                        else
                        {
                            tableLayoutPanelTagDataView.Width = panelTagDataView.Width - 50;
                        }

                        tableLayoutPanelTagDataView.Height = tableLayoutPanelTagDataView.Width * RowSize / ColumnSize;
                    }
                    else
                    {
                        tableLayoutPanelTagDataView.Width = MinPanelColumn * ColumnCount;
                        tableLayoutPanelTagDataView.Height = MinPanelRow * RowCount;
                    }
                }
            }
            else if (panelTagDataView.Width / ColumnSize > panelTagDataView.Height / RowSize)
            {
                if (RowSize < panelTagDataView.Height)
                {
                    if (panelTagDataView.Height / RowCount > MaxPanelRow)
                    {
                        tableLayoutPanelTagDataView.Height = MaxPanelRow * RowCount;
                    }
                    else
                    {
                        tableLayoutPanelTagDataView.Height = panelTagDataView.Height - 50;
                    }

                    tableLayoutPanelTagDataView.Width = tableLayoutPanelTagDataView.Height * ColumnSize / RowSize;
                }
                else
                {
                    if (MinPanelRow <= panelTagDataView.Height / RowCount)
                    {
                        RowSize = panelTagDataView.Height / RowCount;

                        if (panelTagDataView.Width / ColumnCount > MaxPanelColumn)
                        {
                            tableLayoutPanelTagDataView.Width = MaxPanelColumn * ColumnCount;
                        }
                        else
                        {
                            tableLayoutPanelTagDataView.Width = panelTagDataView.Width - 50;
                        }

                        tableLayoutPanelTagDataView.Height = tableLayoutPanelTagDataView.Width * RowSize / ColumnSize;
                    }
                    else
                    {
                        tableLayoutPanelTagDataView.Width = MinPanelColumn * ColumnCount;
                        tableLayoutPanelTagDataView.Height = MinPanelRow * RowCount;
                    }
                }
            }

            tableLayoutPanelTagDataView.Left = (this.panelTagDataView.Width - tableLayoutPanelTagDataView.Width) / 2;
            tableLayoutPanelTagDataView.Top = (this.panelTagDataView.Height - tableLayoutPanelTagDataView.Height) / 2;

            if (tableLayoutPanelTagDataView.Location.X < 0 && tableLayoutPanelTagDataView.Location.Y < 0)
            {
                tableLayoutPanelTagDataView.Location = new System.Drawing.Point(0, 0);
            }
            else
            {
                if (tableLayoutPanelTagDataView.Location.X < 0)
                {
                    tableLayoutPanelTagDataView.Location = new System.Drawing.Point(0, 0);
                    tableLayoutPanelTagDataView.Top = (this.panelTagDataView.Height - tableLayoutPanelTagDataView.Height) / 2;
                }
                else if (tableLayoutPanelTagDataView.Location.Y < 0)
                {
                    tableLayoutPanelTagDataView.Location = new System.Drawing.Point(0, 0);
                    tableLayoutPanelTagDataView.Left = (this.panelTagDataView.Width - tableLayoutPanelTagDataView.Width) / 2;
                }
            }
        }

        private void readerSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigDeviceForm dlg = new ConfigDeviceForm(UserDataID))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }


        public async void RfidConnect()
        {
            if (buttonRfidConnect.InvokeRequired)
            {
                buttonRfidConnect.Invoke(new MethodInvoker(delegate
                {
                    buttonRfidConnect.Enabled = false;
                }));
            }

            if (SharedValues.Reader == null)
            {
                UpdateConfigComPort();

                SharedValues.Reader =
                    await Reader.GetReaderAsync(/*SharedValues.ComPort*/ ComPort,
                                                /*SharedValues.Baudrate*/ Baudrate,
                                                /*SharedValues.NumberOfAntennaPorts*/ AntCount).ConfigureAwait(true);

                if (SharedValues.Reader != null)//Asyen : a213과 연결이 되지 않은 상태일 때
                {
                    SharedValues.Reader.SetEventListener((ReaderEventListener)this);

                    if (await SharedValues.Reader.StartAsync().ConfigureAwait(true))//Asyen : a213과 연결에 성공했을 경우 
                    {
                        await LoadRfidInformation().ConfigureAwait(true);
                        await LoadRfidSettings().ConfigureAwait(true);

                        await SharedValues.Reader.SetInventoryAntennaPortReportStateAsync(RFID.ON).ConfigureAwait(true);

                        if (buttonRfidConnect.InvokeRequired)
                        {
                            buttonRfidConnect.Invoke(new MethodInvoker(delegate
                            {
                                buttonRfidConnect.BackColor = Color.Red;//임시 테스트
                            }));
                        }

                        ConnectEnabledCheck(true);//a211과 Connect 시 

                        if (AutoStart && AutoStartCheck)
                        {
                            if (buttonRfidInventory.InvokeRequired)
                            {
                                buttonRfidInventory.Invoke(new MethodInvoker(delegate
                                {
                                    buttonRfidInventory.PerformClick();
                                }));
                            }
                            AutoStartCheck = false;
                        }
                    }
                    else
                    {
                        await SharedValues.Reader.DestroyAsync().ConfigureAwait(true);
                        SharedValues.Reader = null;

                        Popup.Show(Properties.Resources.StringFailedToConnectRemoteReader);
                    }
                }
                else
                {
                    Popup.Show(Properties.Resources.StringFailedToGetRemoteReaderInstance);
                }
            }
        }

        public async void DisConnect()
        {
            if (buttonRfidConnect.InvokeRequired)
            {
                buttonRfidConnect.Invoke(new MethodInvoker(delegate
                {
                    buttonRfidConnect.Enabled = false;
                }));
            }
            else
                buttonRfidConnect.Enabled = false;

            if (SharedValues.Reader != null)
            {
                if (await SharedValues.Reader.GetConnectionStatusAsync().ConfigureAwait(true))
                {
                    await SharedValues.Reader.StopAsync().ConfigureAwait(true);
                    await SharedValues.Reader.DestroyAsync().ConfigureAwait(true);
                    ClearRfidInventoryAll();
                }
                SharedValues.Reader.RemoveEventListener((ReaderEventListener)this);
                SharedValues.Reader = null;

                SharedValues.ReaderConnected = false;

                ConnectEnabledCheck(false);//a211과 Disconnect 시

                buttonRfidConnect.BackColor = Color.Gainsboro;

            }
            buttonRfidConnect.Enabled = true;
        }

        private async void buttonRfidConnect_Click(object sender, EventArgs e)
        {
            buttonRfidConnect.Enabled = false;
            if (SharedValues.Reader == null)
            {
                UpdateConfigComPort();

                SharedValues.Reader =
                    await Reader.GetReaderAsync(/*SharedValues.ComPort*/ ComPort,
                                                /*SharedValues.Baudrate*/ Baudrate,
                                                /*SharedValues.NumberOfAntennaPorts*/ AntCount).ConfigureAwait(true);

                if (SharedValues.Reader != null)//Asyen : a213과 연결이 되지 않은 상태일 때
                {
                    SharedValues.Reader.SetEventListener((ReaderEventListener)this);

                    if (await SharedValues.Reader.StartAsync().ConfigureAwait(true))//Asyen : a213과 연결에 성공했을 경우 
                    {
                        ATrace.i(TAG, I, "INFO. StartAsync");

                        InitReaderSetting();

                        await LoadRfidSettings().ConfigureAwait(true);
                        await SharedValues.Reader.SetInventoryAntennaPortReportStateAsync(RFID.ON).ConfigureAwait(true);
                        LoadRfidSettingValue();
                        buttonRfidConnect.BackColor = Color.Red;//임시 테스트
                        Properties.Resources.Culture = new CultureInfo(CultureString);
                        buttonRfidConnect.Text = Properties.Resources.StringConnectState;

                        ConnectEnabledCheck(true);//a211과 Connect 시 

                        if (AutoStart && AutoStartCheck)
                        {
                            buttonRfidInventory.PerformClick();
                            AutoStartCheck = false;
                        }

                        InventoryOperationSettingsRemoteFilter();
                    }
                    else
                    {
                        await SharedValues.Reader.DestroyAsync().ConfigureAwait(true);
                        SharedValues.Reader = null;

                        ATrace.e(TAG, E, "INFO. StartAsync - Failed");
                        Popup.Show(Properties.Resources.StringFailedToConnectRemoteReader);
                    }
                }
                else
                {
                    Popup.Show(Properties.Resources.StringFailedToGetRemoteReaderInstance);
                }
            }
            else
            {
                if (await SharedValues.Reader.GetConnectionStatusAsync().ConfigureAwait(true))
                {
                    await SharedValues.Reader.StopAsync().ConfigureAwait(true);
                    await SharedValues.Reader.DestroyAsync().ConfigureAwait(true);
                    ClearRfidInventoryAll();
                    ClearCount();
                    for (int i = 0; i < AntCount; i++)
                    {
                        listViewAntTagData.Items.Clear();
                        listViewTagDataView.Items.Clear();
                        tagDataViews[i].labelTagCount.Text = "0";
                    }
                }
                SharedValues.Reader.RemoveEventListener((ReaderEventListener)this);
                SharedValues.Reader = null;

                SharedValues.ReaderConnected = false;

                ConnectEnabledCheck(false);//a211과 Disconnect 시
                buttonRfidConnect.BackColor = Color.Gainsboro;
                Properties.Resources.Culture = new CultureInfo(CultureString);
                buttonRfidConnect.Text = Properties.Resources.StringDisConnectState;
            }
            buttonRfidConnect.Enabled = true;
        }

        private void UpdateConfigComPort()
        {
            if (comboBoxComList.InvokeRequired)
            {
                comboBoxComList.Invoke(new MethodInvoker(delegate
                {
                    ComPort = comboBoxComList.Text;
                }));
            }
            else
                ComPort = comboBoxComList.Text;

            XmlDocument XmlSetting = new XmlDocument();
            XmlSetting.Load(ConfigLoad);
            XmlNode ReComPort = XmlSetting.SelectSingleNode("Configuration/Setting/Function/Device/ComPort");

            ReComPort.InnerText = ComPort;
            XmlSetting.Save(ConfigLoad);
        }

        private void ConnectEnabledCheck(bool enable)
        {
            if (buttonRfidInventory.InvokeRequired)
            {
                buttonRfidInventory.Invoke(new MethodInvoker(delegate
                {
                    buttonRfidInventory.Enabled = enable;
                }));
            }
            else
                buttonRfidInventory.Enabled = enable;


            if (comboBoxComList.InvokeRequired)
            {
                comboBoxComList.Invoke(new MethodInvoker(delegate
                {
                    comboBoxComList.Enabled = !enable;
                }));
            }
            else
                comboBoxComList.Enabled = !enable;

            if (buttonComSearch.InvokeRequired)
            {
                buttonComSearch.Invoke(new MethodInvoker(delegate
                {
                    buttonComSearch.Enabled = !enable;
                }));
            }
            else
                buttonComSearch.Enabled = !enable;

            if (buttonRfidInventory.InvokeRequired)
            {
                buttonRfidInventory.Invoke(new MethodInvoker(delegate
                {
                    buttonRfidInventory.Enabled = enable;
                }));
            }
            else
                buttonRfidInventory.Enabled = enable;
        }

        private async Task LoadRfidInformation()
        {
            if (SharedValues.Reader != null)
            {
                string moduleName = await SharedValues.Reader.GetModuleNameAsync().ConfigureAwait(true);
                string sn = await SharedValues.Reader.GetModuleSnAsync().ConfigureAwait(true);
                string versionCode = await SharedValues.Reader.GetRFIDVersionAsync().ConfigureAwait(true);
                int version = Convert.ToInt32(versionCode ?? "0", CultureInfo.CurrentCulture);
                string versionString = string.Format(CultureInfo.CurrentCulture,
                                                     "v{0:D}.{1:D}.{2:D}.{3:D}",
                                                     (version >> 24) & 0xff,
                                                     (version >> 16) & 0xff,
                                                     (version >> 8) & 0xff,
                                                     version & 0xff);

                //string region = SharedValues.RfidRegulatoryRegionsArray[await SharedValues.Reader.GetRegulatoryRegionAsync().ConfigureAwait(true)];
                //string globalBand = SharedValues.RfidRegionsArray[await SharedValues.Reader.GetRegionAsync().ConfigureAwait(true)];

                /*labelModuleValue.Text = moduleName + "[" + sn + "]" + " " + region + " " + globalBand + " " + versionString;
                labelModuleValue.Left = labelModule.Right + 5;*/
            }
        }

        private async Task LoadRfidSettings()
        {

            if (SharedValues.NumberOfAntennaPorts > 0)
            {
                await UpdateRfidPowerGainInformation().ConfigureAwait(true);

            }
        }

        private void LoadRfidSettingValue()
        {
            int SessionNumber = 0;
            int ToggleState = 0;
            switch (RfSession)
            {
                case "S0":
                    SessionNumber = 0;
                    break;
                case "S1":
                    SessionNumber = 1;
                    break;
                case "S2":
                    SessionNumber = 2;
                    break;
                default: break;
            }
            switch (RfToggle)
            {
                case true:
                    // ON
                    ToggleState = 1;
                    break;
                case false:
                    // OFF
                    ToggleState = 0;
                    break;
            }
            // 안테나 세션 값 로드
            SharedValues.Reader.SetSession(SessionNumber);
            // 안테나 토글 여부 로드
            SharedValues.Reader.SetToggle(ToggleState);

            for (int i = 0; i < AntCount; i++)
            {
                // 안테나 파워 값 로드
                SharedValues.Reader.SetRadioPower(i, AntPower[i]);
                // 안테나 DwellTiem 값 로드
                int reuslt = SharedValues.Reader.SetDwellTime(i, AntDwellTime[i]);
                /*
                switch (reuslt)
                {
                    case (int)ErrorCode.SUCCESS:
                        break;
                    case (int)ErrorCode.OTHER_CMD_RUNNING_ERROR:
                        break;
                    case (int)ErrorCode.READER_OR_SERIAL_STATUS_ERROR:
                        break;
                    default:
                        break;
                }
                */

                try
                {
                    dwells[i] = SharedValues.Reader.GetDwellTime(i);
                    Debug.WriteLine(dwells[i].ToString());
                }
                catch
                {
                    MessageBox.Show("Error");
                }

            }

        }

        enum ErrorCode
        {
            SUCCESS,
            ARGUMENT_ERROR = -3,
            OTHER_CMD_RUNNING_ERROR = -4,
            READER_OR_SERIAL_STATUS_ERROR = -7
        }

        private async Task UpdateRfidPowerGainInformation()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < /*SharedValues.NumberOfAntennaPorts*/AntCount; i++)
            {
                //Asyen : i 번째 안테나의 파워 -> mAntennaPowerGains[i]
                SharedValues.Reader.SetRadioPower(i, AntPower[i]);

                int powerGain = await SharedValues.Reader.GetRadioPowerAsync(i).ConfigureAwait(true);
                int antennaPowerGain = (powerGain >= RFID.Power.MIN_POWER) && (powerGain <= RFID.Power.MAX_POWER) ? powerGain : RFID.Power.MAX_POWER;
                if (i == SharedValues.NumberOfAntennaPorts - 1)
                {
                    sb.Append(antennaPowerGain + Properties.Resources.StringUnitDbm);
                }
                else
                {
                    sb.Append(antennaPowerGain + ", ");
                }

                //Asyen : Asyen 안테나 Enabled -> true: 
                SharedValues.Reader.SetAntennaPortState(i, AntEnable[i] ? RFID.ON : RFID.OFF);
            }

            string powerGainInfo = sb.ToString();

            SaveSettingValues();
        }

        private void SaveSettingValues()
        {
            //Asyen : 타임 아웃
            int result = SharedValues.Reader.SetAccessTimeout(AccessTimeout);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(Properties.Resources.StringFailedToSetAccessTimeout);
            }

            //Asyen : 주기
            result = SharedValues.Reader.SetAccessRetryInterval(AccessRetryInterval);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(Properties.Resources.StringFailedToSetRetryInterval);
            }

            //Asyen : On Time
            result = SharedValues.Reader.SetTxOnTime(RfTxOnTime);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(Properties.Resources.StringFailedToSetTxOnTime);
            }

            //Asyen : OffTime
            result = SharedValues.Reader.SetTxOffTime(RfTxOffTime);
            if (result != RfidResult.SUCCESS)
            {
                Popup.Show(Properties.Resources.StringFailedToSetTxOffTime);
            }
        }

        public async void RfidInventory()
        {
            InventoryTimer.Stop();
            timerAntNoCheck.Stop();
            // 인벤토리 종료
            await ToggleRfidInventory().ConfigureAwait(true);

        }

        private async void buttonRfidInventory_Click(object sender, EventArgs e)
        {
            StopCheck = false;
            varClear();
            await ToggleRfidInventory().ConfigureAwait(true);

            btnInvenStop.Enabled = true;
            ((Control)tabPageListMode).Enabled = false;
        }

        private void buttonRfidClear_Click(object sender, EventArgs e)
        {
            ClearRfidInventoryAll();
        }

        private void ClearRfidInventoryAll()
        {

            mRfidTagTotalCounter = 0;


            mTagItemList.Clear();

            InitializeArray();

            mOldTotalCount = 0;
            mOldSec = 0;

            UpdateRfidInventorySpeedCountText();

            if (mRfidInventoryStarted)
            {
                StartStopWatchTimer();
            }

            for (int i = 0; i < tagDataViews.Length; i++)
            {
                tagDataViews[i].labelAntNum.BackColor = Color.Transparent;
                tagDataViews[i].labelTagCount.BackColor = Color.White;
            }


            //인벤토리 종료되며 DB에 상태값 0
            InventoryStopTagLog(IpAddress);
        }

        private void varClear()
        {
            g_TagBufferCurLocation = 0;//현재 점유중인 위치
            //g_TagBufferCurLocationCheck = 0;//현재 백업 데이터의 위치가 변경되었는지 체크

            g_TagTimerAntNo = 0;//타이머로 인해 점유중인 안테나의 위치가 변경되었는지 체크
            g_TagCurAntNo = 0;//이벤트 안테나 번호
            g_TagTimerAddCount = 0;
            g_TagBufferDataLength = 0;

            g_TagBufferDataLength = 0; //g_TagBufferData의 태그 개수 저장고
            g_TagBufferData = new string[128, 11, 3][];//총 길이, 안테나, 데이터 종류(0->epc, 1->rssi,2->아직 안정함)

            //g_TagBufferTotalDataLength = 0; //g_TagBufferData의 태그 개수 저장고
            g_TagBufferTotalData = new string[128, 3][];//안테나, 데이터 종류(0->epc, 1->rssi,2->아직 안정함)

            g_TagBufferTotal_IN_Tag = new string[128, 0][];//입고 안테나별
            g_TagBufferTotal_OUT_Tag = new string[128, 0][];//출고 안테나별

        }

        private string RequestDeviceSerialNumber(string DeviceId)
        {
            HttpWebRequest wReq;
            Uri uri = new Uri("http://192.168.0.206:8000/alertDeviceStartEvent/" + DeviceId);
            wReq = (HttpWebRequest)WebRequest.Create(uri);
            wReq.Method = "GET";
            wReq.ServicePoint.Expect100Continue = false;
            wReq.CookieContainer = new CookieContainer();

            WebResponse response = wReq.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }


        public bool InventoryStopTagLog(string Ip_Address)//Asyen : State -> 0
        {
            if (ExecuteProcNonQuery(//Inventory Stop 
                        ConnectMariaDB,
                        "USP_SET_TAG_LOG_STATE_0",
                        new IDataParameter[]
                        {
                            new MySqlParameter("@P_IP_ADDRESS", Ip_Address),
                        }) <= 0)
            {
                return false; // DB 등록 실패 

            }
            return true; // DB 등록 성공
        }

        private async Task ToggleRfidStopInventory()
        {
            int result;

            //Asyen : 인벤토리 중이라면
            if (mRfidInventoryStarted)
            {
                if (buttonRfidConnect.InvokeRequired)
                {
                    buttonRfidConnect.Invoke(new MethodInvoker(delegate
                    {
                        buttonRfidConnect.Enabled = true;
                    }));
                }
                else
                    buttonRfidConnect.Enabled = true;

                result = await SharedValues.Reader.StopOperationAsync().ConfigureAwait(true);

                //Asyen : 인벤토리 stop
                if (result == RfidResult.SUCCESS ||
                    result == RfidResult.NOT_INVENTORY_STATE)
                {

                    timerAntNoCheck.Enabled = false;

                    mRfidInventoryStarted = false;
                    ToggleRfidInventoryButton();
                    PauseStopWatchTimer();

                    ClearCount();

                    //인벤토리 종료되며 DB에 상태값 0
                    InventoryStopTagLog(IpAddress);

                    Properties.Resources.Culture = new CultureInfo(CultureString);
                    buttonRfidInventory.Text = Properties.Resources.StringInitInvenButton;
                }
                else
                {
                    Popup.Show(Properties.Resources.StringFailedToStopInventory);
                }
            }
        }

        private void ClearCount()
        {
            // Clear 메소드 : 지정한 구간을 지운다. int 형 배열은 0으로 채우고, string 형 배열은 문자열을 비운다.
            Array.Clear(InputCount, 0, InputCount.Length);
            Array.Clear(OutputCount, 0, OutputCount.Length);
            Array.Clear(Count, 0, Count.Length);
        }

        private bool StopCheck = false;
        private async Task ToggleRfidInventory()
        {
            int result;

            //Asyen : 인벤토리 중이라면
            if (mRfidInventoryStarted)
            {
                if (buttonRfidConnect.InvokeRequired)
                {
                    buttonRfidConnect.Invoke(new MethodInvoker(delegate
                    {
                        buttonRfidConnect.Enabled = true;
                    }));
                }
                else
                    buttonRfidConnect.Enabled = true;

                result = await SharedValues.Reader.StopOperationAsync().ConfigureAwait(true);

                //Asyen : 인벤토리 stop
                if (result == RfidResult.SUCCESS ||
                    result == RfidResult.NOT_INVENTORY_STATE)
                {
                    ATrace.i(TAG, I, "INFO StopInventory");
                    // 네트워크 모드이면 태그 리스트 가져오기
                    if (NetWorkModeCheck)
                        ReqeustTagList();

                    timerAntNoCheck.Enabled = false;

                    mRfidInventoryStarted = false;
                    ToggleRfidInventoryButton();
                    PauseStopWatchTimer();

                    //인벤토리 종료되며 DB에 상태값 0
                    InventoryStopTagLog(IpAddress);

                    Properties.Resources.Culture = new CultureInfo(CultureString);
                    buttonRfidInventory.Text = Properties.Resources.StringInvenStop;

                    // 버퍼 업데이트
                    UpdateBufferLocation();

                    TagReadCheck = false;

                    if (!StopCheck)
                    {
                        // 다시 인벤토리 시작
                        //StartInventory();
                        await ToggleRfidInventory().ConfigureAwait(true);
                    }
                }
                else
                {
                    string msg = Convert.ToString(result);
                    ATrace.i(TAG, I, "INFO StopInventory - Failed " + msg);
                    Popup.Show(Properties.Resources.StringFailedToStopInventory);
                }

            }
            //Asyen : 인벤토리 중이 아니라면
            else
            {
                buttonRfidConnect.Enabled = false;

                result = await SharedValues.Reader.StartInventoryAsync().ConfigureAwait(true);
                //Asyen : 인벤토리 start
                if (result == RfidResult.SUCCESS)
                {
                    ATrace.i(TAG, I, "INFO StartInventory");
                    if (dwells?[0] == null)
                        timerAntNoCheck.Interval = iDwells;
                    else
                        timerAntNoCheck.Interval = dwells[0];

                    // 인벤토리 시간에 맞춰 타이머 설정 
                    if (dwells?[0] == null)
                        InventoryTimer.Interval = (int)(iDwells * AntCount * 1.1);
                    else
                        InventoryTimer.Interval = (int)(dwells[0] * AntCount * 1.1);

                    InventoryTimer.Start();
                    timerAntNoCheck.Start();

                    SavePortData = null;

                    mRfidInventoryStarted = true;
                    ToggleRfidInventoryButton();

                    ClearRfidInventoryAll();

                    Properties.Resources.Culture = new CultureInfo(CultureString);
                    buttonRfidInventory.Text = Properties.Resources.StringInvenStart;

                    CycleCheck = true;
                    TimerCheck = true;
                }
                else
                {
                    string msg = Convert.ToString(result);
                    ATrace.i(TAG, I, "INFO StartInventory - Failed " + msg);
                    Popup.Show(Properties.Resources.StringFailedToStartInventory);
                }
            }
        }

        private string[] TagList;
        private void GetTagDataList(string[] TagList)
        {
            this.TagList = TagList;
        }


        private async void StartInventory()
        {
            buttonRfidConnect.Enabled = false;
            int result;

            result = await SharedValues.Reader.StartInventoryAsync().ConfigureAwait(true);
            //Asyen : 인벤토리 start
            if (result == RfidResult.SUCCESS)
            {
                ATrace.i(TAG, I, "INFO StartInventory");
                if (dwells?[0] == null)
                    timerAntNoCheck.Interval = iDwells;
                else
                    timerAntNoCheck.Interval = dwells[0];
                timerAntNoCheck.Enabled = true;

                // 인벤토리 시간에 맞춰 타이머 설정 
                if (dwells?[0] == null)
                    InventoryTimer.Interval = (int)(iDwells * AntCount * 1.1);
                else
                    InventoryTimer.Interval = (int)(dwells[0] * AntCount * 1.1);
                InventoryTimer.Start();

                SavePortData = null;

                mRfidInventoryStarted = true;
                ToggleRfidInventoryButton();

                ClearRfidInventoryAll();

                Properties.Resources.Culture = new CultureInfo(CultureString);
                buttonRfidInventory.Text = Properties.Resources.StringInvenStart;

                CycleCheck = true;
                TimerCheck = true;
            }
            else
            {
                string msg = Convert.ToString(result);
                ATrace.i(TAG, I, "INFO StartInventory - Failed " + msg);
                Popup.Show(Properties.Resources.StringFailedToStartInventory);
            }
        }

        // 카운트 배열로 빼자
        private int[] InputCount = new int[128];
        private int[] OutputCount = new int[128];
        private int[] Count = new int[128];

        private void UpdateBufferLocation()
        {
            // 태그 입출고 표시 리셋
            for (int i = 0; i < AntCount; i++)
            {
                tagDataViews[i].labelStateIN.BackColor = Color.Transparent;
                tagDataViews[i].labelStateOUT.BackColor = Color.Transparent;
            }

            // 인벤토리가 끝났으니 다음 버퍼로 이동
            // 이동하기 전 현재 인벤토리가 끝난 버퍼와 이전(화면에 보여졌던) 버퍼와 카운트를 비교
            for (int i = 0; i < AntCount; i++)
            {
                // 이전버퍼
                int BeforeBufferLocation = g_TagBufferCurLocationArray[i] + 1;
                // 이전버퍼가 2로 넘어가면 0으로 바꿔줌 (현재 버퍼는 2개 뿐이므로)
                if (BeforeBufferLocation > 1)
                    BeforeBufferLocation = 0;
                // 현재 버퍼가 비워져 있고 이전 버퍼에 값이 있다면 출고처리
                else if (g_TagBufferData[i, BeforeBufferLocation, 0] != null && g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0] == null)
                {
                    for (int j = 0; j < g_TagBufferData[i, BeforeBufferLocation, 0].Length; j++)
                    {
                        if (NetWorkModeCheck)
                        {
                            RequestJSON2(DeviceId, "anonymous", i + 1, g_TagBufferData[i, BeforeBufferLocation, 0][j], --Count[i], 0, ++OutputCount[i]);
                        }
                    }

                    // 태그 카운트 출력
                    tagDataViews[i].labelTagCount.Text = "0";
                    tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                }
                // 이전 버퍼가 비워져 있다면 현재 버퍼에 있는 값들은 전부 입고상태이겟지
                if (g_TagBufferData[i, BeforeBufferLocation, 0] == null && g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0] != null)
                {
                    // 태그 카운트 출력
                    tagDataViews[i].labelTagCount.Text = g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length.ToString();
                    tagDataViews[i].labelStateIN.BackColor = Color.Red;
                    for (int j = 0; j < g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length; j++)
                    {
                        if (NetWorkModeCheck)
                        {
                            RequestJSON1(DeviceId, "anonymous", i + 1, g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j], ++Count[i], 0, ++InputCount[i]);
                        }
                    }
                    tagDataViews[i].labelStateIN.BackColor = Color.Red;
                } 
                // 이전 버퍼에 값이 있으면 비교를 해서 입고인지 출고인지 확인을 해야겠지
                else if (g_TagBufferData[i, BeforeBufferLocation, 0] != null && g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0] != null)
                {
                    string[,,][] g_SaveData = new string[128, 11, 3][];
                    // 현재 버퍼의 카운트가 더 크다면 입고처리
                    if (g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length > g_TagBufferData[i, BeforeBufferLocation, 0].Length)
                    {
                        /*
                        g_SaveData = g_TagBufferData;
                        for (int j = 0; j < g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length; j++)
                        {
                            bool result = g_TagBufferData[i, BeforeBufferLocation, 0].Contains(g_SaveData[i, g_TagBufferCurLocationArray[i], 0][j]);
                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON1(DeviceId, "anonymous", i + 1, g_SaveData[i, g_TagBufferCurLocationArray[i], 0][j], ++Count, 0, ++InputCount);
                                    //OutputCount--;
                                    Thread.Sleep(50);
                                    Debug.WriteLine("Request1");
                                }
                            }
                        }
                        tagDataViews[i].labelStateIN.BackColor = Color.Red;
                        */
                        // 현재 버퍼의 카운트가 더 크다해도 이전 버퍼와 비교했을때 이전 버퍼에 있는 태그값이 현재 버퍼에는 없는 경우도 존재한다.
                        // 또한 현재 버퍼에 있는 태그값이 이전 버퍼에 없는 경우가 있겠지.

                        // 현재 버퍼와 이전 버퍼 값을 비교
                        // 이전 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, BeforeBufferLocation, 0].Length; j++)
                        {

                            bool result = g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Contains(g_TagBufferData[i, BeforeBufferLocation, 0][j]);

                            // 만약 이전 버퍼에 있던 값이 현재 버퍼에 없다면 출고처리
                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON2(DeviceId, "anonymous", i + 1, g_TagBufferData[i, BeforeBufferLocation, 0][j], --Count[i], 0, ++OutputCount[i]);
                                    //Thread.Sleep(30);
                                }
                                tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                            }
                        }
                        // 현재 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length; j++)
                        {
                            bool result = g_TagBufferData[i, BeforeBufferLocation, 0].Contains(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j]);

                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON1(DeviceId, "anonymous", i + 1, g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j], ++Count[i], 0, ++InputCount[i]);
                                    //Thread.Sleep(30);
                                }
                                tagDataViews[i].labelStateIN.BackColor = Color.Red;
                            }
                        }
                        
                       

                    }
                    // 이전 버퍼의 카운트가 더 크다면 출고처리
                    else if (g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length < g_TagBufferData[i, BeforeBufferLocation, 0].Length)
                    {/*
                        g_SaveData = g_TagBufferData;
                        for (int j = 0; j < g_TagBufferData[i, BeforeBufferLocation, 0].Length; j++)
                        {
                            bool result = g_SaveData[i, g_TagBufferCurLocationArray[i], 0].Contains(g_TagBufferData[i, BeforeBufferLocation, 0][j]);
                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON2(DeviceId, "anonymous", i + 1, g_SaveData[i, BeforeBufferLocation, 0][j], --Count, 0, ++OutputCount);
                                    //InputCount--;
                                    Thread.Sleep(50);
                                    Debug.WriteLine("Request2");
                                }
                            }
                        }
                        tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                        */
                        // 현재 버퍼와 이전 버퍼 값을 비교
                        // 이전 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, BeforeBufferLocation, 0].Length; j++)
                        {

                            bool result = g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Contains(g_TagBufferData[i, BeforeBufferLocation, 0][j]);

                            // 만약 이전 버퍼에 있던 값이 현재 버퍼에 없다면 출고처리
                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON2(DeviceId, "anonymous", i + 1, g_TagBufferData[i, BeforeBufferLocation, 0][j], --Count[i], 0, ++OutputCount[i]);
                                    //Thread.Sleep(30);
                                }
                                tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                            }
                        }
                        // 현재 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length; j++)
                        {
                            bool result = g_TagBufferData[i, BeforeBufferLocation, 0].Contains(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j]);

                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON1(DeviceId, "anonymous", i + 1, g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j], ++Count[i], 0, ++InputCount[i]);
                                    //Thread.Sleep(30);  
                                }
                                tagDataViews[i].labelStateIN.BackColor = Color.Red;
                            }
                        }
                        
                        
                    }
                    // 버퍼의 카운트가 서로 같다면
                    else
                    {
                        // 이전 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, BeforeBufferLocation, 0].Length; j++)
                        {

                            bool result = g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Contains(g_TagBufferData[i, BeforeBufferLocation, 0][j]);

                            // 만약 이전 버퍼에 있던 값이 현재 버퍼에 없다면 출고처리
                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON2(DeviceId, "anonymous", i + 1, g_TagBufferData[i, BeforeBufferLocation, 0][j], --Count[i], 0, ++OutputCount[i]);
                                    //Thread.Sleep(30);
                                }
                                tagDataViews[i].labelStateOUT.BackColor = Color.Blue;
                            }
                        }
                        // 현재 버퍼와 이전 버퍼 값을 비교
                        // 현재 버퍼의 데이터 개수 만큼 비교
                        for (int j = 0; j < g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length; j++)
                        {
                            bool result = g_TagBufferData[i, BeforeBufferLocation, 0].Contains(g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j]);

                            if (!result)
                            {
                                if (NetWorkModeCheck)
                                {
                                    RequestJSON1(DeviceId, "anonymous", i + 1, g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0][j], ++Count[i], 0, ++InputCount[i]);
                                    //OutputCount--;
                                    //Thread.Sleep(30);
                                }
                                tagDataViews[i].labelStateIN.BackColor = Color.Red;
                            }
                        }
                        
                        

                    }
                    // 태그 카운트 출력
                    tagDataViews[i].labelTagCount.Text = g_TagBufferData[i, g_TagBufferCurLocationArray[i], 0].Length.ToString();

                }
                // 전부 비워져 있다면 넘어가기
                else
                {
                    // 태그 카운트 출력
                    tagDataViews[i].labelTagCount.Text = "0";
                }


                // 다음 버퍼로 넘어가자
                int UpdateBufferLocation = g_TagBufferCurLocationArray[i] + 1;
                if (BeforeBufferLocation > 1)
                    BeforeBufferLocation = 0;

                g_TagBufferCurLocationArray[i] = g_TagBufferCurLocationArray[i] + 1;
                if (g_TagBufferCurLocationArray[i] > 1)
                    g_TagBufferCurLocationArray[i] = 0;

                // 다음 버퍼 위치로 이동했다면 그 버퍼들을 모두 초기화
                if (g_TagBufferData[i, BeforeBufferLocation, 0] != null)
                {
                    Array.Resize(ref g_TagBufferData[i, BeforeBufferLocation, 0], 0);
                    Array.Resize(ref g_TagBufferData[i, BeforeBufferLocation, 1], 0);

                }

                LVDebugMode.Items[i].SubItems[BeforeBufferLocation + 1].Text = "0";
                LVDebugMode.Items[i].UseItemStyleForSubItems = false;
                SkipTagBufferTotalSum(i);

                //Debug.WriteLine("Input : " + InputCount.ToString());
                //Debug.WriteLine("Output : " + OutputCount.ToString());
                //Debug.WriteLine("Count : " + Count.ToString());
            }

        }


        private void PauseStopWatchTimer()
        {
            if (mStopWatchTimer.Enabled)
            {
                mStopWatchTimer.Stop();
                mOldSec = mTimeInMillisec / 1000;
                mRfidTagTotalCounter = 0;
            }
        }

        //Todo : 제거 예정 시작

        private void buttonComSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxComList.Enabled == true)
            {
                if (!InitializePorts())
                {
                    MessageBox.Show("포트가 연결되지 않았습니다.");
                    return;
                }
            }
        }
        private bool InitializePorts() //Asyen : 기본 포트 값 설정
        {
            comboBoxComList.Items.Clear();

            List<String> lstComPorts = new List<string>();

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%Com%'"))
            {
                var ComPortnames = SerialPort.GetPortNames();
                var ComPorts = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());
                var ComPortList = ComPortnames.Select(n => /* n + " - " + */
                        ComPorts.FirstOrDefault(s => s.Contains(n))).ToArray();

                string comPort;
                for (int i = 0; i < ComPortList.Length; i++)
                {
                    comPort = Convert.ToString(ComPortList[i]);
                    if (!String.IsNullOrEmpty(comPort)) { lstComPorts.Add(comPort); }
                }
            }

            string[] comPorts = lstComPorts.ToArray();
            if (comPorts.Length > 0)
            {
                //Asyen : comboBoxComList.Items.AddRange(comPorts);

                for (int i = 0; i < comPorts.Length; i++)
                {
                    comboBoxComList.Items.Add(new { ComListName = comPorts[i].Substring(comPorts[i].IndexOf("(") + 1, comPorts[i].IndexOf(")") - comPorts[i].IndexOf("(") - 1), ComListContent = comPorts[i] });
                }

                for (int i = 0; i < comboBoxComList.Items.Count; i++)
                {
                    string PortName = comPorts[i].Substring(comPorts[i].IndexOf("(") + 1, comPorts[i].IndexOf(")") - comPorts[i].IndexOf("(") - 1), ComListContent = comPorts[i];
                    if (ComPort == PortName)
                    {
                        comboBoxComList.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        comboBoxComList.SelectedIndex = 0;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void comboBoxComList_DropDown(object sender, EventArgs e)
        {
            string a = comboBoxComList.Text;
            comboBoxComList.DisplayMember = "ComListContent";
            comboBoxComList.Text = a;
        }

        private void comboBoxComList_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxComList.DisplayMember = "ComListName";
        }

        //Todo : 제거 예정 끝

        private void buttonSearchTag_Click(object sender, EventArgs e)
        {
            string asd = textBoxSearchTag.Text;
            int index = -1;
            for (int i = 0; i < AntCount; i++)
            {
                if (g_TagBufferTotalData[i, 0] != null)
                {
                    for (int j = 0; j < g_TagBufferTotalData[i, 0].Length; j++)
                    {
                        index = g_TagBufferTotalData[i, 0][j].IndexOf(asd);
                        if (index != -1)
                        {
                            tagDataViews[i].labelTagCount.BackColor = Color.Yellow;
                        }
                    }

                }
            }
        }

        private async void devSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingDefaultForm dlg = new SettingDefaultForm(CultureString))
            {
                dlg.EventDeviceInfo += new SettingDefaultForm.EventDeviceSettings(GetDeviceSettingInfo);
                dlg.EventRssi += new SettingDefaultForm.EventRssiSettings(GetRssiFilterInfo);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    InitializeSettingConfig();
                    tableLayoutPanelTagDataViewSetting(CountPanelColumn, CountPanelRow);
                }
            }
        }

        private bool[] states = null;
        private int[] gains = null;
        private int[] dwells = new int[128];
        private int[] counts = null;
        private string Panelrow = null;
        private string Panelcolumn = null;


        private int iDwells;

        private void InitReaderSetting()
        {
            iDwells = SharedValues.Reader.GetDwellTime();
            SharedValues.Reader.SetSession(RFID.Session.SESSION_S1);
        }

        private void GetDeviceSettingInfo(bool[] states, int[] gains, int[] dwells, int[] counts, string Panelrow, string Panelcolumn, string DeviceName)
        {
            if (states != null)
                this.states = states;
            if (gains != null)
                this.gains = gains;
            if (dwells != null)
                this.dwells = dwells;
            if (counts != null)
                this.counts = counts;
            this.Panelrow = Panelrow;
            this.Panelcolumn = Panelcolumn;
            if (DeviceName != null)
                this.DeviceName = DeviceName;

        }

        private bool RssiCheck;
        private int RssiValue;

        private void GetRssiFilterInfo(bool RssiCheck, int RssiValue)
        {
            this.RssiCheck = RssiCheck;
            this.RssiValue = RssiValue;
        }

        private void tableModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listModeToolStripMenuItem.Checked = false;
            tableModeToolStripMenuItem.Checked = true;
            debugModeToolStripMenuItem.Checked = false;
            listViewTagDataView.Visible = false;
            panelTagDataView.Visible = true;
            LVDebugMode.Visible = false;
        }

        private void listModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listModeToolStripMenuItem.Checked = true;
            tableModeToolStripMenuItem.Checked = false;
            debugModeToolStripMenuItem.Checked = false;
            listViewTagDataView.Visible = true;
            panelTagDataView.Visible = false;
            LVDebugMode.Visible = false;
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listModeToolStripMenuItem.Checked = false;
            tableModeToolStripMenuItem.Checked = false;
            debugModeToolStripMenuItem.Checked = true;
            panelTagDataView.Visible = false;
            listViewTagDataView.Visible = false;
            LVDebugMode.Visible = true;
            for (int i = 0; i < 128; i++)
            {
                string[] items = new string[15];
                items[0] = (i + 1).ToString();
                ListViewItem item = new ListViewItem(items);
                LVDebugMode.Items.Add(item);
            }
            InitDebugModeList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txbBufferUpLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bufferSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BufferSettingForm bfsform = new BufferSettingForm())
            {
                if (bfsform.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }


        private void LVDebugMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void listViewTagDataView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewRfidInventoryTagData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InventoryTimer_Tick(object sender, EventArgs e)
        {
            RfidInventory();
        }

        private void InventoryStopTImer_Tick(object sender, EventArgs e)
        {
            InventoryStopTImer.Stop();
            InventoryTimer.Start();
        }

        private async void btnInvenStop_Click(object sender, EventArgs e)
        {
            StopCheck = true;
            await ToggleRfidStopInventory().ConfigureAwait(true);
            InventoryStopTImer.Stop();
            InventoryTimer.Stop();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlModeTab.SelectedTab == tabPageTableMode)
            {
                listViewTagDataView.Visible = false;
                LVDebugMode.Visible = false;
                btnExcelSave.Enabled = false;
                btnExcelSave.Visible = false;

                panelTagDataView.Visible = true;
                buttonRfidInventory.Visible = true;
                btnInvenStop.Visible = true;
            }
            else
            {
                StopCheck = true;
                await ToggleRfidStopInventory().ConfigureAwait(true);
                InventoryStopTImer.Stop();
                InventoryTimer.Stop();

                panelTagDataView.Visible = false;
                LVDebugMode.Visible = false;
                buttonRfidInventory.Visible = false;
                btnInvenStop.Visible = false;

                listViewTagDataView.Visible = true;
                btnExcelSave.Enabled = true;
                btnExcelSave.Visible = true;
            }
        }

        private void korToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture
               = new System.Globalization.CultureInfo("ko-KR");
            Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo("ko-KR");

            Properties.Resources.Culture = new CultureInfo(CultureString);

            this.Controls.Clear();

            InitializeComponent();
            InitializeArray();
            tagDataViewsSetting();
            tableLayoutPanelTagDataViewSetting(CountPanelColumn, CountPanelRow);
            InitializeEvent();
            InitializeLoadConfig();
            InitializeCreateConfig();
            InitializeSettingConfig();
            InitializePorts();
            IntializeStopWatchTimer();
            btnExcelSave.Visible = false;
            korToolStripMenuItem.Checked = true;
            engToolStripMenuItem.Checked = false;
            CultureString = "ko";
            Properties.Resources.Culture = new CultureInfo(CultureString);
            tableModeToolStripMenuItem.Checked = true;
            m_fnBufferLength = new BufferSettingForm.TagBufferLengthArray(BufferLengthReturn);
            BufferSettingForm.BufferSettingEvent += m_fnBufferLength;
        }

        private void engToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture
               = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo("en");

            this.Controls.Clear();

            InitializeComponent();
            InitializeArray();
            tagDataViewsSetting();
            tableLayoutPanelTagDataViewSetting(CountPanelColumn, CountPanelRow);
            InitializeEvent();
            InitializeLoadConfig();
            InitializeCreateConfig();
            InitializeSettingConfig();
            InitializePorts();
            IntializeStopWatchTimer();
            korToolStripMenuItem.Checked = false;
            engToolStripMenuItem.Checked = true;
            btnExcelSave.Visible = false;
            CultureString = "en";
            Properties.Resources.Culture = new CultureInfo(CultureString);
            tableModeToolStripMenuItem.Checked = true;
            m_fnBufferLength = new BufferSettingForm.TagBufferLengthArray(BufferLengthReturn);
            BufferSettingForm.BufferSettingEvent += m_fnBufferLength;

            for (int i = 0; i < 128; i++)
            {
                BufferPreviousSettingLength[i] = 2;
                BufferSettingLength[i] = 2;
                g_TagBufferCurLocationArray[i] = 0;
            }

            for (int i = 0; i < 128; i++)
            {
                string[] items = new string[15];
                items[0] = (i + 1).ToString();
                ListViewItem item = new ListViewItem(items);
                LVDebugMode.Items.Add(item);
                //checknumber[i] = 0;
                g_TagBufferCurLocationCheck[i] = 0;
            }

        }

        private void tabPageListMode_Click(object sender, EventArgs e)
        {

        }

        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            using (new CenterDialog(this))
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 바탕화면 경로
                    dlg.Filter = Resources.ExcelFileFilter;
                    dlg.Title = Resources.ExcelSaveTitle;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // 엑셀 파일 저장
                        SaveExcelFile(dlg.FileName);
                    }
                }
            }
            //SaveExcel();
        }




        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormHelpAbout Form = new FormHelpAbout())
            {
                if (Form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void modeSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Uri 설정 및 네트워크 모드 세팅 폼  
            using (DeviceNotificationListForm form = new DeviceNotificationListForm())
            {
                form.SelectedDeviceItem += new DeviceNotificationListForm.DeviceListData(LoadDeviceItem);
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
                // 네트워크 모드이면 태그 리스트 가져오기
                if (NetWorkModeCheck)
                    ReqeustTagList();
            }
        }

        private void memuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
