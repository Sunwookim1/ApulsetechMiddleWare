using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Diagnotics
{
    public static class Log
    {
        // 대리자 선언
        public delegate void LogEventHandler(string msg);

        // 이벤트 선언
        public static event LogEventHandler OutputLog;

        public static void WriteLine(string msg)
        {
            // 이벤트 등록이 안되어 있다면
            if (OutputLog == null)
                return;

            string log = String.Format("{0} {1}{2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                msg, Environment.NewLine);
            OutputLog(log);
        }

        public static void WriteLine()
        { WriteLine(String.Empty); }

        // params : 함수의 매개변수를 넘길때 1,2,3개 이렇게 개수를 지정해주었던 것과 달리,
        // params 키워드를 사용하면 개수의 제한 없이 매개변수를 넘길 수 있습니다.
        // https://blockdmask.tistory.com/317
        public static void WriteLine(string format, params object[] args)
        { WriteLine(String.Format(format, args)); }
        public static void WriteLine(string format, object arg0)
        { WriteLine(String.Format(format, arg0)); }
        public static void WriteLine(string format, object arg0, object arg1)
        { WriteLine(String.Format(format, arg0, arg1)); }
        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        { WriteLine(String.Format(format, arg0, arg1, arg2)); }
        
        
    }
}
