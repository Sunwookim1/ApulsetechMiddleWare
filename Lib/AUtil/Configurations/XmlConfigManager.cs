using AUtil.Diagnostics;
using System;
using System.IO;
using System.Xml.Serialization;

namespace AUtil.Configurations
{
    public class XmlConfigManager
    {
        private static readonly string TAG = typeof(XmlConfigManager).Name;
        private const bool E = true;
        private const bool I = true;

        // 설정 파일 로드
        public static T Load<T>(string fileName) where T : class
        {
            T config = null;

            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open,
                        FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    config = (T)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                ATrace.e(TAG, E, ex, "ERROR. Load([{0}]) - Failed to load xml configuration file [{1}]",
                    fileName, ex.Message);
                return null;
            }
            ATrace.i(TAG, I, "INFO. Load([{0}]) - [{1}]", fileName, config.ToString());
            return config;
        }

        // 설정 파일 저장
        public static bool Save<T>(string fileName, T config)
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(config.GetType());
                    serializer.Serialize(stream, config);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                ATrace.e(TAG, E, ex, "ERROR. Save([{0}], [{1}]) - Failed to save xml configuration file [{2}]",
                    fileName, config.ToString(), ex.Message);
                return false;
            }
            ATrace.i(TAG, I, "INFO. Save([{0}], [{1}])", fileName, config.ToString());
            return true;
        }
    }
}
