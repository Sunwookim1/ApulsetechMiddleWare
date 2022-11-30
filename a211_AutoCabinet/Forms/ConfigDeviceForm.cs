using a211_AutoCabinet.Class;
using a211_AutoCabinet.Datas;
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
    public partial class ConfigDeviceForm : Form
    {
        //Asyen_22-08-24 : 변수 (Xml에서 값 가져오기)
        private string ConnectMariaDB;
        private int DbPort;             //Asyen_22-08-25 : 데이터베이스 - 포트
        private string DbAddress;       //Asyen_22-08-25 : 데이터베이스 - 주소
        private string DbDatabase;      //Asyen_22-08-25 : 데이터베이스 - DB명
        private string DbUserID;        //Asyen_22-08-25 : 데이터베이스 - 유저 ID
        private string DbPassword;      //Asyen_22-08-25 : 데이터베이스 - 패스워드

        private List<ReaderInfo> m_lstReaders;
        private List<ReaderInfo> m_lstDelReaders;

        private List<AntennaInfo> m_lstAntennas;
        private List<AntennaInfo> m_lstDelAntennas;

        private ListViewItem[] lstItem;//Asyen_22-08-25 : DB에서 받아온 a211 데이터를 저장

        private string ConfigLoad;

        private int UserDataID;

        public ConfigDeviceForm(int userDataID)
        {
            UserDataID = userDataID;
            InitializeComponent();
            InitializeLoadConfig();
            InitializeSettingConfig();
            IntializeConnectDB();
            IntializeList();
        }

        public void InitializeLoadConfig() //Asyen : exe 파일이 있는 폴더에 Config 파일 위치 설정
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var Config_Path = Path.Combine(outPutDirectory, "Setting.Config");
            string config_path = new Uri(Config_Path).LocalPath;

            ConfigLoad = config_path;
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
            }
            catch (Exception)
            {
                DbPort = 3306;
                DbAddress = "localhost";
                DbDatabase = "a211_128port";
                DbUserID = "rfiduser";
                DbPassword = "123456";
            }
        }

        private void IntializeConnectDB()
        {
            ConnectMariaDB = String.Format("SERVER={0};PORT={1};DATABASE={2};UID={3};PASSWORD={4};SSLMODE=NONE;CharSet=utf8",
                   DbAddress, DbPort, DbDatabase, DbUserID, DbPassword);
        }

        private void IntializeList()
        {
            m_lstReaders = new List<ReaderInfo>();
            m_lstDelReaders = new List<ReaderInfo>();
            //m_lstAntennas = new List<DataAntennaInfo>();
            //m_lstDelAntennas = new List<DataAntennaInfo>();
        }

        private void ConfigDeviceForm_Load(object sender, EventArgs e)
        {
            SelectReaderData();
        }

        private void SelectReaderData()
        {
            ProcedureDB procedureDB = new ProcedureDB();
            m_lstReaders = procedureDB.SelectDevData(ConnectMariaDB, UserDataID);//Asyen : Maria DB -> 리더 검색

            lstItem = new ListViewItem[m_lstReaders.Count];

            for (int i = 0; i < m_lstReaders.Count; i++)
            {
                lstItem[i] = new ListViewItem(new string[]
                {
                    (i+1).ToString(),
                    m_lstReaders[i].ReaderName,
                    m_lstReaders[i].IpAddress,
                    m_lstReaders[i].ComPort,
                    m_lstReaders[i].Baudrate.ToString(),
                    m_lstReaders[i].DevType.ToString(),
                    m_lstReaders[i].AntCount.ToString(),
                    m_lstReaders[i].DwellTime.ToString(),
                    m_lstReaders[i].TxOnTime.ToString(),
                    m_lstReaders[i].TxOffTime.ToString(),
                });

                this.lstReaders.Items.Add(lstItem[i]);
            }
        }

        private void lstAntennas_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if ((e.ColumnIndex == 0))
            {
                e.DrawBackground();
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(e.Header.Tag);
                }
                catch (Exception)
                {
                }

                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.Left + 4, e.Bounds.Top + 4), value ?
                System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.ColumnIndex == 0))
            {/*
                CheckBox cck = new CheckBox();
                //Asyen - 주석 : Text = "";
                
                listViewAntSetting.SuspendLayout();  // 컨트롤의 레이아웃 논리를 임시로 일시 중단
                
                //Asyen - 주석 : e.DrawBackground();  // 열 머리글의 배경색을 그리기
                //Asyen - 주석 : cck.BackColor = Color.Transparent;
                //Asyen - 주석 : cck.UseVisualStyleBackColor = true;  // 비주얼 스타일을 사용하여 배경을 그리면 true
                
                //컨트롤의 범위를 지정된 위치와 크기로 설정 (Left x, Top y, width, height)
                cck.SetBounds(e.Bounds.X, e.Bounds.Y, cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height))
                    .Width, cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width);// 컨트롤의 높이와 너비를 가져오거나 설정       
                
                cck.Size = new Size((cck.GetPreferredSize(new Size((e.Bounds.Width - 1), e.Bounds.Height)).
                    Width + 1), e.Bounds.Height);
                
                cck.Location = new Point(4, 0);// 왼쪽 위를 기준으로 컨트롤의 왼쪽 위의 좌표를 가져오거나 설정
                
                listViewAntSetting.Controls.Add(cck);
                cck.Show();

                //cck.BringToFront();       
                //Visible = true;  
                // 컨트롤과 모든 해당 자식 컨트롤이 표시되면 true
                e.DrawText((TextFormatFlags.VerticalCenter | TextFormatFlags.Left));
                cck.Click += new EventHandler(Bink);
                // 컨트롤을 클릭하면 발생       
                // listView1.ResumeLayout(true);  
                // 일반 레이아웃 논리를 다시 시작*/
            }
            else
            {
                //e.DrawDefault = true;
            }
        }

        private void lstAntennas_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstAntennas_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstAntennas_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(this.lstAntennas.Columns[e.Column].Tag);
                }
                catch (Exception)
                {
                }

                this.lstAntennas.Columns[e.Column].Tag = !value;
                foreach (ListViewItem item in this.lstAntennas.Items)
                {
                    item.Checked = !value;
                }

                this.lstAntennas.Invalidate();
            }
        }

        private void lstReaders_MouseClick(object sender, MouseEventArgs e)
        {
            int ClickReaderID = m_lstReaders[lstReaders.SelectedIndices[0]].ReaderID;
            lblClickReaderName.Text = "Reader Name : " + m_lstReaders[lstReaders.SelectedIndices[0]].ReaderName;
            SelectAntennaData(ClickReaderID, UserDataID);
        }

        private void SelectAntennaData(int ReaderID, int UserDataID)
        {
            ProcedureDB procedureDB = new ProcedureDB();
            m_lstAntennas = procedureDB.SelectAntData(ConnectMariaDB, ReaderID, UserDataID);//Asyen : Maria DB -> 리더 검색

            lstItem = new ListViewItem[m_lstAntennas.Count];

            lstAntennas.Items.Clear();

            for (int i = 0; i < m_lstAntennas.Count; i++)
            {
                lstItem[i] = new ListViewItem(new string[]
                {
                    "",
                    m_lstAntennas[i].AntennaSeq.ToString(),
                    m_lstAntennas[i].PowerGain.ToString()
                });

                lstAntennas.Items.Add(lstItem[i]);
            }
        }
    }
}
