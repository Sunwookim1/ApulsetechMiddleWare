using a211_AutoCabinet.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using a211_AutoCabinet.Datas;
using System.Reflection;
using a211_AutoCabinet.Controls;
using Apulsetech.Util.Diagnostics;

namespace a211_AutoCabinet
{
    static class Program
    {
        private static readonly string TAG = typeof(Program).Name;
        private const bool I = true;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string moduleName = SysUtil.GetModuleName(assembly);
            ATrace.InitTrace(true);
            ATrace.i(TAG, I, "BEGIN. ==================== {0} ========================================",
                moduleName);

            Config.Load(assembly);

            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ATMW(/*1*/));
            //Application.Run(new LoginForm());
        }
    }
}
