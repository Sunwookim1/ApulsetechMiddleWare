using a211_AutoCabinet.Class;
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
    public partial class LoginForm : Form
    {
        //Asyen_22-08-24 : 변수 (Xml에서 값 가져오기)
        private string ConnectMariaDB;
        private int DbPort;             //Asyen_22-08-25 : 데이터베이스 - 포트
        private string DbAddress;       //Asyen_22-08-25 : 데이터베이스 - 주소
        private string DbDatabase;      //Asyen_22-08-25 : 데이터베이스 - DB명
        private string DbUserID;        //Asyen_22-08-25 : 데이터베이스 - 유저 ID
        private string DbPassword;      //Asyen_22-08-25 : 데이터베이스 - 패스워드

        
        private string ConfigLoad;
        private int UserDataID;
        private string UserID;
        private string UserPW;

        public LoginForm()
        {
            InitializeComponent();
            InitializeLoadConfig();
            InitializeSettingConfig();
            IntializeConnectDB();
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

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            UserID = textBoxId.Text;
            UserPW = textBoxPW.Text;
            SelectReaderData();

            if (UserDataID != 0)
            {
                MessageBox.Show("로그인 성공");
                ATMW mainForm = new ATMW(UserDataID);
                mainForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("아이디 혹은 비밀번호가 잘못되었습니다.");
                textBoxPW.Text = null;
            }
        }

        private void SelectReaderData()
        {
            ProcedureDB procedureDB = new ProcedureDB();
            UserDataID = procedureDB.SelectUserData(ConnectMariaDB, UserID, UserPW);//Asyen : Maria DB -> 리더 검색
        }
    }
}
