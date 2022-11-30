using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Datas
{
    public class JsonData
    {
        public string Device_id { get; set; }
        public string Worker_id { get; set; }
        public int Location { get; set; }
        public string Date { get; set; }
        public string EPC { get; set; }
        public int Flag { get; set; }
        public int Count { get; set; }
        public int StockCount { get; set; }
        public int InputCount { get; set; }
        public int OutputCount { get; set; }

    }

    public class JsonData2
    {
        public string Device_id { get; set; }
        public int Location { get; set; }
        public int Count { get; set; }
    }

    
}
