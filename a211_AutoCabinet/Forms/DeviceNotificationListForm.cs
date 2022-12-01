using a211_AutoCabinet.Class;
using a211_AutoCabinet.Datas;
using a211_AutoCabinet.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace a211_AutoCabinet.Forms
{
    public partial class DeviceNotificationListForm : Form
    {
        //대리자
        public delegate void DeviceListData(string DeviceName, string DeviceId, string ApiUri, string GsUri, bool NetWorkMode);
        //이벤트 
        public event DeviceListData SelectedDeviceItem;

        // #Define 대신
        // https://midason.tistory.com/119
        public const int DEVICE_COUNT_MAX = 999;

        private int SelectedDeviceId = 0;

        // 리스트 아이템을 선택했는지 여부
        private bool CheckSelectedItem = false;
        // 디바이스를 정상적으로 로드했는지 여부
        private bool CheckLoadDeviceItem = false;
        // 네트워크 모드 선택 여부
        private bool NetWorkMode = false;


        Jsonparents jsonparents;

        public DeviceNotificationListForm()
        {
            InitializeComponent();
        }

        public DeviceNotificationListForm(string DeviceId)
        {
            InitializeComponent();
        }


        private void DeviceNotificationListForm_Load(object sender, EventArgs e)
        {
            // API 호출
            //RequestJSON(); 
        }

        // Get, Post 
        // https://steemd.com/hive-101145/@realmankwon/realmankwon-posting-2020-09-10-17-41
        private void RequestJSON()
        {
            // 데이터 요청
            string[] DeviceID_Arr = new string[DEVICE_COUNT_MAX];
            string[] DeviceName_Arr = new string[DEVICE_COUNT_MAX];
            string result = string.Empty;
            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Uri uri = new Uri(ApiUri + "/mwCon/getDeviceList");
            wReq = (HttpWebRequest)WebRequest.Create(uri);
            wReq.Method = "GET";

            // 요청 데이터 받기
            try
            {
                wResp = (HttpWebResponse)wReq.GetResponse();
                using (StreamReader streamReader = new StreamReader(wResp.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                // json 파싱
                JObject obj = JObject.Parse(result);

                string objstring = obj.ToString();

                // json 노드값
                //string notice = obj["dataList"].ToString();

                // 클래스 객체화하기
                // https://bigenergy.tistory.com/entry/c-json-%ED%8C%8C%EC%8B%B1%EC%9D%84-%ED%81%B4%EB%9E%98%EC%8A%A4-%EA%B0%9D%EC%B2%B4%ED%99%94%ED%95%98%EA%B8%B0-serialize%EC%99%80-Deserialize
                jsonparents = new Jsonparents();
                jsonparents = JsonConvert.DeserializeObject<Jsonparents>(objstring);

                // 리스트뷰 초기화
                ListviewInit(jsonparents);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        // 리스트뷰 초기화
        private void ListviewInit(Jsonparents jsonparents)
        {

            // Details 설정
            ListviewDeviceNotify.View = View.Details;

            // Column 추가
            // -2 크기 자동조정
            // 추가기능 -> Insert, RemoveAt
            ListviewDeviceNotify.Columns.Add("No.", 50, HorizontalAlignment.Left);
            ListviewDeviceNotify.Columns.Add("DeviceID", 120, HorizontalAlignment.Left);
            ListviewDeviceNotify.Columns.Add("DeviceName", -2, HorizontalAlignment.Left);


            // 더블버퍼로 깜빡임 없애기 -> 이게 카메라 원리인가
            ListviewDeviceNotify.DoubleBuffered(true);

            // 리스트뷰 Visible 설정
            ListviewDeviceNotify.Visible = true;

            // 리스트뷰 Row 선택하기
            ListviewDeviceNotify.FullRowSelect = true;

            int Device_Count = 0;
            // 리스트에 값 넣기
            foreach (JsonSon value in jsonparents.dataList)
            {
                string[] items = new string[ListviewDeviceNotify.Columns.Count];
                items[0] = Device_Count.ToString();
                ListViewItem item = new ListViewItem(items);
                ListviewDeviceNotify.Items.Add(item);

                ListviewDeviceNotify.Items[Device_Count].SubItems[1].Text = value.DEVICE_NAME.ToString();
                ListviewDeviceNotify.Items[Device_Count].SubItems[2].Text = value.DEVICE_ID.ToString();
                Device_Count++;
            }

            // 저장 버튼 활성화
            button1.Visible = true;
            button1.Enabled = true;
        }

        private void ListviewDeviceNotify_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 이벤트가 2번씩 발생하는 문제점
            // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=ronghuan&logNo=110031935797
            if (ListviewDeviceNotify.SelectedItems.Count > 0)
            {
                // 선택된 행의 인덱스 가져오기 
                SelectedDeviceId = ListviewDeviceNotify.SelectedItems[0].Index;
                CheckSelectedItem = true;
            }
        }

        string ApiUri = String.Empty;
        string GsUri = String.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckSelectedItem)
            {
                // class 멤버 변수 값 가져오기
                string DeviceName = jsonparents.dataList[SelectedDeviceId].DEVICE_NAME;
                string DeviceId = jsonparents.dataList[SelectedDeviceId].DEVICE_ID;


                if (ApiUriTextValue.Text == "" || GsUriTextValue.Text == "")
                {
                    MessageBox.Show("디바이스를 선택해주세요.");
                    return;
                }

                NetWorkMode = true;
                // MainForm에 Device 값 보내기
                SelectedDeviceItem(DeviceName, DeviceId, ApiUri, GsUri, NetWorkMode);

                CheckLoadDeviceItem = true;


                Close();
            }
            else
            {
                MessageBox.Show(Properties.Resources.StringPleaseSelectDevice);
            }
        }


        private void DeviceNotificationListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            if (!CheckLoadDeviceItem)
            {
                MessageBox.Show(Properties.Resources.StringNotSelectedDevice);
                Application.Exit();
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApiUri = "http://" + ApiUriTextValue.Text;
            GsUri = "http://" + GsUriTextValue.Text;

            if (ApiUriTextValue.Text == "" || GsUriTextValue.Text == "")
            {
                MessageBox.Show("Uri를 입력해주세요.");
            }
            else
            {
                // 인터넷 연결 상태 확인
                bool result = InternetConnectedCheck.IsInternetConnected();
                if (result)
                    RequestJSON();
                else
                    MessageBox.Show(Resources.StringInternetConnectState);

            }

        }
    }

    public class Jsonparents
    {
        public string code;
        public string message;
        public List<JsonSon> dataList;
    }

    public class JsonSon
    {
        public string DEVICE_ID;
        public string DEVICE_NAME;
    }

}
