using a211_AutoCabinet.Datas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Controls
{
    internal class Config
    {
        public static ExcelOption EcelOption { get; set; }

        public static void Load()
        { Load(Assembly.GetExecutingAssembly()); }
        public static void Load(Assembly assembly)
        {
            string filePath = GetFilePath(assembly);
            Configuration config = XmlConfigManagercs.Load<Configuration>(filePath);
            if (config == null)
            {
                config = new Configuration();
                XmlConfigManagercs.Save(filePath, config);
            }
        }

        private static string GetFilePath(Assembly assembly)
        {
            string filePath = Path.Combine(SysUtil.GetModulePath(assembly),
                String.Format("{0}.config", SysUtil.GetModuleName(assembly)));
            return filePath;
        }


    }



    public class ExcelOption
    {
        public int SheetNo { get; set; }
        public int HeaderRowNo { get; set; }
        public int ReadStartRowNo { get; set; }
        public int ReadStartColNo { get; set; }
        public int ReadEndColNo { get; set; }
        public int ReadEndRowNo { get; set; }

    }
}
