using System;
using System.Globalization;

namespace AUtil.Data
{
    public static class AConvert
    {
        public static short ToInt16(string value)
        { return ToInt16(value, default(int), false); }
        public static short ToInt16(string value, bool isHex)
        { return ToInt16(value, default(int), isHex); }
        public static short ToInt16(string value, short defaultValue)
        { return ToInt16(value, default, false); }
        public static short ToInt16(string value, short defaultValue, bool isHex)
        {
            short resValue = 0;
            try
            {
                if (isHex)
                    resValue = Int16.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = Int16.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static int ToInt32(string value)
        { return ToInt32(value, default(int), false); }
        public static int ToInt32(string value, bool isHex)
        { return ToInt32(value, default(int), isHex); }
        public static int ToInt32(string value, int defaultValue)
        { return ToInt32(value, default, false); }
        public static int ToInt32(string value, int defaultValue, bool isHex)
        {
            int resValue = 0;
            try
            {
                if (isHex)
                    resValue = Int32.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = Int32.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static long ToInt64(string value)
        { return ToInt64(value, default(int), false); }
        public static long ToInt64(string value, bool isHex)
        { return ToInt64(value, default(int), isHex); }
        public static long ToInt64(string value, long defaultValue)
        { return ToInt64(value, default, false); }
        public static long ToInt64(string value, long defaultValue, bool isHex)
        {
            long resValue = 0;
            try
            {
                if (isHex)
                    resValue = Int64.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = Int64.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }
        public static ushort ToUInt16(string value)
        { return ToUInt16(value, default(int), false); }
        public static ushort ToUInt16(string value, bool isHex)
        { return ToUInt16(value, default(int), isHex); }
        public static ushort ToUInt16(string value, ushort defaultValue)
        { return ToUInt16(value, default, false); }
        public static ushort ToUInt16(string value, ushort defaultValue, bool isHex)
        {
            ushort resValue = 0;
            try
            {
                if (isHex)
                    resValue = UInt16.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = UInt16.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static uint ToUInt32(string value)
        { return ToUInt32(value, default(int), false); }
        public static uint ToUInt32(string value, bool isHex)
        { return ToUInt32(value, default(int), isHex); }
        public static uint ToUInt32(string value, uint defaultValue)
        { return ToUInt32(value, default, false); }
        public static uint ToUInt32(string value, uint defaultValue, bool isHex)
        {
            uint resValue = 0;
            try
            {
                if (isHex)
                {
                    if (value.StartsWith("0x") || value.StartsWith("0X"))
                        value = value.Substring(2);
                    resValue = UInt32.Parse(value, NumberStyles.HexNumber);
                }
                else
                    resValue = UInt32.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static ulong ToUInt64(string value)
        { return ToUInt64(value, default(int), false); }
        public static ulong ToUInt64(string value, bool isHex)
        { return ToUInt64(value, default(int), isHex); }
        public static ulong ToUInt64(string value, ulong defaultValue)
        { return ToUInt64(value, default, false); }
        public static ulong ToUInt64(string value, ulong defaultValue, bool isHex)
        {
            ulong resValue = 0;
            try
            {
                if (isHex)
                    resValue = UInt64.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = UInt64.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static float ToSingle(string value)
        { return ToSingle(value, default(int), false); }
        public static float ToSingle(string value, bool isHex)
        { return ToSingle(value, default(int), isHex); }
        public static float ToSingle(string value, float defaultValue)
        { return ToSingle(value, default, false); }
        public static float ToSingle(string value, float defaultValue, bool isHex)
        {
            float resValue = 0;
            try
            {
                if (isHex)
                    resValue = Single.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = Single.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }

        public static double ToDouble(string value)
        { return ToDouble(value, default(int), false); }
        public static double ToDouble(string value, bool isHex)
        { return ToDouble(value, default(int), isHex); }
        public static double ToDouble(string value, double defaultValue)
        { return ToDouble(value, default, false); }
        public static double ToDouble(string value, double defaultValue, bool isHex)
        {
            double resValue = 0;
            try
            {
                if (isHex)
                    resValue = Double.Parse(value, NumberStyles.HexNumber);
                else
                    resValue = Double.Parse(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return resValue;
        }
    }
}
