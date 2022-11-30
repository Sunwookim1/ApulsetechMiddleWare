using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Datas
{
    public class ReaderInfo
    {
        public int ReaderID { get; set; }
        public string ReaderName { get; set; }
        public string IpAddress { get; set; }
        public string ComPort { get; set; }
        public int Baudrate { get; set; }
        public string DevType { get; set; }
        public int AntCount { get; set; }
        public int DwellTime { get; set; }
        public int TxOnTime { get; set; }
        public int TxOffTime { get; set; }
        public string UseYN { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public AccessType Type { get; set; }

        public ReaderInfo()
        {
            ReaderID = default(int);
            ReaderName = default(string);
            IpAddress = default(string);
            ComPort = default(string);
            Baudrate = default(int);
            DevType = default(string);
            AntCount = default(int);
            DwellTime = default(int);
            TxOnTime = default(int);
            TxOffTime = default(int);
            UseYN = default(string);
            CreateTime = default(DateTime);
            UpdateTime = default(DateTime);
            Type = AccessType.None;
        }

        public ReaderInfo(int readerId, string readerName, string ipAddress, string comPort, 
            int baudrate, string devType, int antCount, int dwellTime, int txOnTime, int txOffTime, 
            string useYN, DateTime createTime, DateTime updateTime, AccessType type)
        {
            ReaderID = readerId;
            ReaderName = readerName;
            IpAddress = ipAddress;
            ComPort = comPort;
            Baudrate = baudrate;
            DevType = devType;
            AntCount = antCount;
            DwellTime = dwellTime;
            TxOnTime = txOnTime;
            TxOffTime = txOffTime;
            UseYN = useYN;
            CreateTime = createTime;
            UpdateTime = updateTime;
            Type = type;
        }
    }
}
