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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a211_AutoCabinet.Forms
{
    public partial class ATMW
    {
        // 인벤토리 스탑 때마다 태그 리스트 업데이트하기 위한 이벤트
        public delegate void GetTagList(string[] str);
        public event GetTagList GetTagListEvent;

        ResponseJson responseJson;

        // 디바이스 존재 여부 확인 API
        private bool DeviceIdRequest()
        {
            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Uri uri = new Uri(GsUri+ "/alertDeviceStartEvent/"+DeviceId);
            wReq = (HttpWebRequest)WebRequest.Create(uri);
            wReq.Method = "GET";


            wResp = (HttpWebResponse)wReq.GetResponse();
            using (StreamReader streamReader = new StreamReader(wResp.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();

                switch (result)
                {
                    case "true":
                        return true;
                        break;
                    case "false":
                        return false;
                        break;
                    default:
                        return false;
                        break;
                }
            }

        }

        private void RequestJSON2(string DeviceId, string WorkerId, int Location, string Epc, int Count, int StockCount, int OutputCount)
        {
            JObject JSonData = new JObject();
            JSonData.Add("DEVICE_ID", DeviceId);
            JSonData.Add("WORKER_ID", WorkerId);
            JSonData.Add("LOCATION", Location);
            JSonData.Add("DATE_TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            JSonData.Add("EPC", Epc);
            JSonData.Add("COUNT", Count);
            JSonData.Add("STOCK_COUNT", StockCount);
            JSonData.Add("OUTPUT_COUNT", OutputCount);

            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Uri uri = new Uri(GsUri+"/alertOutputEvent");
            wReq = (HttpWebRequest)WebRequest.Create(uri);
            wReq.Method = "POST";
            wReq.ContentType = "application/json";
            wReq.ContentLength = JSonData.ToString().Length;

            using (StreamWriter streamWriter = new StreamWriter(wReq.GetRequestStream()))
            {
                streamWriter.Write(JSonData.ToString());
            }

            try
            {
                wResp = (HttpWebResponse)wReq.GetResponse();
                using (StreamReader streamReader = new StreamReader(wResp.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();
            }

        }

        private void RequestJSON1(string DeviceId, string WorkerId, int Location, string Epc, int Count, int StockCount, int InputCount)
        {
            JObject JSonData = new JObject();
            JSonData.Add("DEVICE_ID", DeviceId);
            JSonData.Add("WORKER_ID", WorkerId);
            JSonData.Add("LOCATION", Location);
            JSonData.Add("DATE_TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            JSonData.Add("EPC", Epc);
            JSonData.Add("COUNT", Count);
            JSonData.Add("STOCK_COUNT", StockCount);
            JSonData.Add("INPUT_COUNT", InputCount);

            HttpWebRequest wReq;
            HttpWebResponse wResp;
            Uri uri = new Uri(GsUri+"/alertInputEvent");
            wReq = (HttpWebRequest)WebRequest.Create(uri);
            wReq.Method = "POST";
            wReq.ContentType = "application/json";
            wReq.ContentLength = JSonData.ToString().Length;

            using (StreamWriter streamWriter = new StreamWriter(wReq.GetRequestStream()))
            {
                streamWriter.Write(JSonData.ToString());
            }

            try
            {
                wResp = (HttpWebResponse)wReq.GetResponse();
                using (StreamReader streamReader = new StreamReader(wResp.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();
            }

        }

        private void ReqeustTagList()
        {
            HttpWebRequest wReq;
            HttpWebResponse wResp;
            string result = string.Empty;
            Uri uri = new Uri(ApiUri + "/mwCon/getTag");
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

                //json파싱
                JObject obj = JObject.Parse(result);

                string objstring = obj.ToString();

                responseJson = new ResponseJson();
                responseJson = JsonConvert.DeserializeObject<ResponseJson>(objstring);

                string[] TagListArr = new string[responseJson.dataList.Count];
                int count = 0;
                foreach (ValueJson TagData in responseJson.dataList)
                {
                    TagListArr[count] = TagData.TAG;
                    count++;
                }
                if (TagListArr != null)
                    GetTagListEvent(TagListArr);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Status.ToString());
            }
        }
    }

    public class ResponseJson
    {
        public string code;
        public string message;
        public List<ValueJson> dataList;
    }

    public class ValueJson
    {
        public string TAG;
    }
}
