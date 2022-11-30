using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace a211_AutoCabinet.Controls
{
    public class XmlConfigManagercs
    {
        private static readonly string TAG = typeof(XmlConfigManagercs).Name;
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
                return null;
            }
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
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
