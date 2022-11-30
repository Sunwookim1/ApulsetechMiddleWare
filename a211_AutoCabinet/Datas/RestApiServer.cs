using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using a211_AutoCabinet.Forms;


namespace a211_AutoCabinet.Datas
{
    
    public class RestApiServer
    {
        System.Net.HttpListener httpListener;
        public ATMW mf;

        public RestApiServer(ATMW mainform)
        {
            mf = mainform;
        }

        public bool serverInit()
        {
            if (httpListener == null)
            {
                httpListener = new System.Net.HttpListener();
                httpListener.Prefixes.Add(String.Format("http://+:8686/"));
                //httpListener.Prefixes.Add(String.Format("http://192.168.0.206:8686/"));
                return true;
            }
            return false;
        }

        public void serverStart()
        {
            if (!httpListener.IsListening)
            {
                httpListener.Start();

                if (httpListener.IsListening)
                {
                    // MessageBox.Show("Server is started");
                    //richTextBox1.Text = "Server is started";
                }
                else
                {
                    //MessageBox.Show("Server is failed");
                    //richTextBox1.Text = "Server is failed";
                }


                Task.Factory.StartNew(() =>
                {
                    while (httpListener != null)
                    {
                        // 요청 대기
                        System.Net.HttpListenerContext context = this.httpListener.GetContext();

                        Stream InputStream = context.Request.InputStream;
                        StreamReader Reader = new StreamReader(InputStream);
                        string result1 = Reader.ReadToEnd();
                        string ContentType = context.Request.ContentType;
                        string rawurl = context.Request.RawUrl;
                        string httpmethod = context.Request.HttpMethod;
                        InputStream.Close();
                        string result = "";

                        /*
                        result += string.Format("Data = {0}\r\n", result1);
                        result += string.Format("ContentType = {0}\r\n", ContentType);
                        result += string.Format("httpmethod = {0}\r\n", httpmethod);
                        result += string.Format("rawurl = {0}\r\n", rawurl);

                        if (richTextBox1.InvokeRequired)
                        {
                            richTextBox1.Invoke(new MethodInvoker(delegate
                            {
                                richTextBox1.Text = result;
                            }));
                        }
                        else
                            richTextBox1.Text = result;
                        */

                        if (ContentType == "application/json")
                        {
                            switch (rawurl)
                            {
                                case "/Connect":
                                    mf.RfidConnect();
                                    break;
                                case "/DisConnect":
                                    mf.DisConnect();
                                    break;
                                case "/InventoryStart":
                                    mf.RfidInventory();
                                    break;
                                case "/InventoryStop":
                                    mf.RfidInventory();
                                    break;
                                case "/Clear":
                                    break;
                                case "/DefaultSetting":
                                    break;
                                case "/BufferSetting":
                                    break;
                            }

                        }


                        context.Response.Close();
                    }
                });

            }
        }

    }


}
