using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Controls
{
    public static class SysUtil
    {
        //Retrieve Module Name
        public static string GetModuleName()
        { return GetModuleName(Assembly.GetCallingAssembly()); }
        public static string GetModuleName(Assembly assembly)
        {
            return Path.GetFileNameWithoutExtension(assembly.ManifestModule.Name);
        }

        // Retrieve Module Path
        public static string GetModulePath()
        { return GetModulePath(Assembly.GetCallingAssembly()); }
        public static string GetModulePath(Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }
    }
}
