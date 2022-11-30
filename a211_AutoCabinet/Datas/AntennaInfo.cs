using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Datas
{
    public class AntennaInfo
    {
        public int AntennaID { get; set; }
        public int ReaderID { get; set; }
        public int AntennaSeq { get; set; }
        public int PowerGain { get; set; }
        public int State { get; set; }
        public string UseYN { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public AccessType Type { get; set; }

        public AntennaInfo()
        {
            AntennaID = default(int);
            ReaderID = default(int);
            AntennaSeq = default(int);
            PowerGain = default(int);
            State = default(int);
            UseYN = default(string);
            CreateTime = default(DateTime);
            UpdateTime = default(DateTime);
            Type = AccessType.None;
        }

        public AntennaInfo(int antennaId, int readerId, int antennaSeq, int powerGain, 
            int state, string useYN, DateTime createTime, DateTime updateTime, AccessType type)
        {
            AntennaID = antennaId;
            ReaderID = readerId;
            AntennaSeq = antennaSeq;
            PowerGain = powerGain;
            State = state;
            UseYN = useYN;
            CreateTime = createTime;
            UpdateTime = updateTime;
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("[{0}, {1}], {2}, [{3}], {4}, {5}, {6}, {7}, {8}",
                AntennaID, ReaderID, AntennaSeq, PowerGain, State, UseYN, CreateTime, UpdateTime, Type);
        }
    }
}
