using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Datas
{
    public static class InternetConnectedCheck
    {
        const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
        const string NCSI_TEST_RESULT = "Microsoft NCSI";
        const string NCSI_DNS = "dns.msftncsi.com";
        const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

        static InternetConnectedCheck()
        {
            
        }

        public static bool IsInternetConnected()
        {
            // 인터넷 연결 체크하기
            // https://www.csharpstudy.com/Tip/Tip-network-connectivity.aspx
            try
            {
                // Check NCSI test link
                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    return false;
                }

                // Check NCSI DNS IP
                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            return true;
        }
    }

}
