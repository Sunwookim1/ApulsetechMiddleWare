using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AUtil.Utils
{
    public static class SysUtil
    {
        // Retrieve Module Name
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

        // Retrieve Module Version
        public static string GetVersion()
        { return GetVersion(Assembly.GetCallingAssembly()); }
        public static string GetVersion(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        [DllImport("KERNEL32.dll")]
        public extern static void Beep(int freq, int duration);
    }
}
