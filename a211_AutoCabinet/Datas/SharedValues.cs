using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apulsetech.Barcode;
using Apulsetech.Remote.Type;
using Apulsetech.Rfid;
using Apulsetech.Rfid.Vendor.Tag.Sensor;
using Apulsetech.Type;
using System.Globalization;

namespace a211_AutoCabinet.Datas
{
    public static class SharedValues
    {
        public static readonly string[] LogLevelArray = { "None", "Error", "Warning", "Info", "Debug" };
        public const int MaxRetryCountCmdRunningCheck = 50;
        public const int MaxRetryCountReadSensor = 10;
        public const int MaxRfidRssiValueQueueSize = 20;
        public const int MaxRfidPhaseValueQueueSize = 20;
        public static readonly int[] ScanTimeoutValueArray =
            { 30000, 60000, 120000, 180000 };
        public static readonly int[] ConnectionTimeoutValueArray =
            { 10000, 20000, 30000, 40000, 50000, 60000, 120000, 180000 };

        public static readonly int[] BaudrateArray =
            { 115200, 230400, 460800, 921600 };

        private static string[] BarcodeLengthTypeArray =
            { "One Discrete Length", "Two Discrete Length", "Length Within Range", "Any Length" };

        public static readonly string[] BarcodeDecodeUpcEanSupplementalRedundancyArray =
        {
            "2 time", "3 time", "4 time", "5 time", "6 time", "7 time", "8 time", "9 time", "10 time",
            "11 time", "12 time", "13 time", "14 time", "15 time", "16 time", "17 time", "18 time",
            "19 time", "20 time", "21 time", "22 time", "23 time", "24 time", "25 time", "26 time",
            "27 time", "28 time", "29 time", "30 time"
        };

        public static readonly string[] RfidMemTypesArray =
            { "Reserved", "EPC", "TID", "User" };
        public static readonly string[] RfidSelctionMemTypesArray =
            { "FileType", "EPC", "TID", "User" };
        public static readonly string[] RfidSecurityMemTypesArray =
            { "EPC", "TID", "User" };
        public static readonly string[] RfidSelectionTargetsArray =
            { "Inventoried(S0)", "Inventoried(S1)", "Inventoried(S2)", "Inventoried(S3)", "SL" };
        public static readonly string[] RfidSelectionTargetsShortArray =
            { "S0", "S1", "S2", "S3", "SL" };
        public static readonly string[] RfidSelectionActionsArray = {
            "Assert SL or inventoried → A, Deassert SL or inventoried → B",
            "Assert SL or inventoried → A, Do nothing",
            "Do nothing, Deassert SL or inventoried → B",
            "Negate SL or (A → B, B → A), Do nothing",
            "Deassert SL or inventoried → B, Assert SL or inventoried → A",
            "Deassert SL or inventoried → B, Do nothing",
            "Do nothing, Assert SL or inventoried → A",
            "Do nothing, Negate SL or (A → B, B → A)"
        };
        public static readonly string[] RfidInventorySessionArray = { "S0", "S1", "S2", "S3" };
        public static readonly string[] RfidInventoryTargetsArray = { "A", "B" };
        public static readonly string[] RfidInventorySelectionsArray = { "ALL", "~SL", "SL" };
        public static readonly string[] RfidRegulatoryRegionsArray = { "FCC", "ETSI", "Japan", "China" };
        public static readonly string[] RfidRegionsArray = {
            "Korea", "EU", "FCC", "China", "Bangladesh", "Brazil", "Brunei",
            "Australia", "Japan(1W)", "Japan(250mW)", "Hongkong", "India",
            "Indonesia", "Iran", "Israel", "Jordan", "Malaysia", "Morocco",
            "New Zealand", "Pakistan", "Peru", "Philippines", "Singapore",
            "South Africa", "Taiwan", "Thailand", "Uruguay", "Venezuela",
            "Vietnam", "Russia", "Algeria"
        };

        public static readonly string[] RfidFccRegionsArray = {
            "Korea", "FCC", "Bangladesh", "Brazil", "Brunei",
            "Australia", "Hongkong", "Indonesia", "Israel", "Malaysia",
            "New Zealand", "Peru", "Philippines", "Singapore",
            "South Africa", "Taiwan", "Thailand", "Uruguay", "Venezuela",
            "Vietnam", "Algeria"
        };

        public static readonly string[] RfidEtsiRegionsArray = {
            "EU", "India", "Iran", "Jordan", "Morocco", "Pakistan", "Russia"
        };

        public static readonly string[] RfidJapanRegionsArray = {
            "Japan(1W)", "Japan(250mW)"
        };

        public static readonly string[] RfidChinaRegionsArray = {
            "China"
        };

        public static readonly int[] RfidRegionIndexesArray = {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30
        };

        public static readonly int[] RfidRegionFccIndexesArray = {
            0, 2, 4, 5, 6, 7, 10, 12, 14, 16,
            18, 20, 21, 22, 23, 24, 25, 26, 27, 28,
            30
        };

        public static readonly int[] RfidRegionEtsiIndexesArray = {
             1, 11, 13, 15, 17, 19, 29
        };

        public static readonly int[] RfidRegionJapanIndexesArray = {
            8, 9
        };

        public static readonly int[] RfidRegionChinaIndexesArray = {
            3
        };

        public static readonly string[] RfidRfModesArray = {
            "DSB-ASK, FM0, 40KHz",
            "PR-ASK, Miller-4, 250KHz",
            "PR-ASK, Miller-4, 300KHz",
            "DSB-ASK, FM0, 400KHz"
        };

        public static readonly string[] RfidSensorListColumns = { "Model", "S/N or ID", "Freq", "Sensor Info" };

        public static readonly string[] RfidSensorVendorArray = { "RFMicron", "Farsense", "EMMicron" };

        private static string mComPort = null;
        private static int mBaudrate = 0;
        private static int mNumberOfAntennaPorts = 1;

        public static readonly string[] RfidCryptographySuiteNameArray = {
            "AES-128", "PRESENT-90", "ECC-DH", "Grain-128A", "AES-OFB", "XOR", "ECDSA-ECDH", "cryptoGPS", "RAMON", "SIMON", "SPECK"
        };

        public static readonly int[] RfidCrytographySuiteValueArray = {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x09, 0x0B, 0x0C
        };

        private static Scanner mScanner = null;
        private static Reader mReader = null;
        private static SensorReader mSensorReader = null;

        private static bool mScannerConnected = false;
        private static bool mReaderConnected = false;

        private static HidTerminator mHidTerminator = HidTerminator.NONE;
        private static string mHidPrefix = null;
        private static string mHidSurfix = null;

        public static void LoadCultureResources()
        {
            BarcodeLengthTypeArray[0] = Properties.Resources.StringOneDiscreteLength;
            BarcodeLengthTypeArray[1] = Properties.Resources.StringTwoDiscreteLength;
            BarcodeLengthTypeArray[2] = Properties.Resources.StringLengthWithinRange;
            BarcodeLengthTypeArray[3] = Properties.Resources.StringAnyLength;

            for (int i = 5; i <= 30; i++)
            {
                BarcodeDecodeUpcEanSupplementalRedundancyArray[i - 5] =
                    string.Format(CultureInfo.CurrentCulture, "{0:D} {1}", i, Properties.Resources.StringTimes);
            }

            RfidSelectionTargetsArray[0] = Properties.Resources.StringInventoriedS0;
            RfidSelectionTargetsArray[1] = Properties.Resources.StringInventoriedS1;
            RfidSelectionTargetsArray[2] = Properties.Resources.StringInventoriedS2;
            RfidSelectionTargetsArray[3] = Properties.Resources.StringInventoriedS3;
            //RfidSelectionTargetsArray[4] = Properties.Resources.StringSl;

            RfidSelectionActionsArray[0] = Properties.Resources.StringAction1;
            RfidSelectionActionsArray[1] = Properties.Resources.StringAction2;
            RfidSelectionActionsArray[2] = Properties.Resources.StringAction3;
            RfidSelectionActionsArray[3] = Properties.Resources.StringAction4;
            RfidSelectionActionsArray[4] = Properties.Resources.StringAction5;
            RfidSelectionActionsArray[5] = Properties.Resources.StringAction6;
            RfidSelectionActionsArray[6] = Properties.Resources.StringAction7;
            RfidSelectionActionsArray[7] = Properties.Resources.StringAction8;

            RfidRegulatoryRegionsArray[2] = Properties.Resources.StringJapan;
            RfidRegulatoryRegionsArray[3] = Properties.Resources.StringChina;

            RfidRegionsArray[0] = Properties.Resources.StringKorea;
            RfidRegionsArray[1] = Properties.Resources.StringEu;
            RfidRegionsArray[2] = Properties.Resources.StringFcc;
            RfidRegionsArray[3] = Properties.Resources.StringChina;
            RfidRegionsArray[4] = Properties.Resources.StringBangladesh;
            RfidRegionsArray[5] = Properties.Resources.StringBrazil;
            RfidRegionsArray[6] = Properties.Resources.StringBrunei;
            RfidRegionsArray[7] = Properties.Resources.StringAustralia;
            RfidRegionsArray[8] = Properties.Resources.StringJapan1w;
            RfidRegionsArray[9] = Properties.Resources.StringJapan250mW;
            RfidRegionsArray[10] = Properties.Resources.StringHongkong;
            RfidRegionsArray[11] = Properties.Resources.StringIndia;
            RfidRegionsArray[12] = Properties.Resources.StringIndonesia;
            RfidRegionsArray[13] = Properties.Resources.StringIran;
            RfidRegionsArray[14] = Properties.Resources.StringIsrael;
            RfidRegionsArray[15] = Properties.Resources.StringJordan;
            RfidRegionsArray[16] = Properties.Resources.StringMalaysia;
            RfidRegionsArray[17] = Properties.Resources.StringMorocco;
            RfidRegionsArray[18] = Properties.Resources.StringNewZealand;
            RfidRegionsArray[19] = Properties.Resources.StringPakistan;
            RfidRegionsArray[20] = Properties.Resources.StringPeru;
            RfidRegionsArray[21] = Properties.Resources.StringPhilippines;
            RfidRegionsArray[22] = Properties.Resources.StringSingapore;
            RfidRegionsArray[23] = Properties.Resources.StringSouthAfrica;
            RfidRegionsArray[24] = Properties.Resources.StringTaiwan;
            RfidRegionsArray[25] = Properties.Resources.StringThailand;
            RfidRegionsArray[26] = Properties.Resources.StringUruguay;
            RfidRegionsArray[27] = Properties.Resources.StringVenezuela;
            RfidRegionsArray[28] = Properties.Resources.StringVietnam;
            RfidRegionsArray[29] = Properties.Resources.StringRussia;
            RfidRegionsArray[30] = Properties.Resources.StringAlgeria;

            RfidFccRegionsArray[0] = Properties.Resources.StringKorea;
            RfidFccRegionsArray[1] = Properties.Resources.StringFcc;
            RfidFccRegionsArray[2] = Properties.Resources.StringBangladesh;
            RfidFccRegionsArray[3] = Properties.Resources.StringBrazil;
            RfidFccRegionsArray[4] = Properties.Resources.StringBrunei;
            RfidFccRegionsArray[5] = Properties.Resources.StringAustralia;
            RfidFccRegionsArray[6] = Properties.Resources.StringHongkong;
            RfidFccRegionsArray[7] = Properties.Resources.StringIndonesia;
            RfidFccRegionsArray[8] = Properties.Resources.StringIsrael;
            RfidFccRegionsArray[9] = Properties.Resources.StringMalaysia;
            RfidFccRegionsArray[10] = Properties.Resources.StringNewZealand;
            RfidFccRegionsArray[11] = Properties.Resources.StringPeru;
            RfidFccRegionsArray[12] = Properties.Resources.StringPhilippines;
            RfidFccRegionsArray[13] = Properties.Resources.StringSingapore;
            RfidFccRegionsArray[14] = Properties.Resources.StringSouthAfrica;
            RfidFccRegionsArray[15] = Properties.Resources.StringTaiwan;
            RfidFccRegionsArray[16] = Properties.Resources.StringThailand;
            RfidFccRegionsArray[17] = Properties.Resources.StringUruguay;
            RfidFccRegionsArray[18] = Properties.Resources.StringVenezuela;
            RfidFccRegionsArray[19] = Properties.Resources.StringVietnam;
            RfidFccRegionsArray[20] = Properties.Resources.StringAlgeria;

            RfidEtsiRegionsArray[0] = Properties.Resources.StringEu;
            RfidEtsiRegionsArray[1] = Properties.Resources.StringIndia;
            RfidEtsiRegionsArray[2] = Properties.Resources.StringIran;
            RfidEtsiRegionsArray[3] = Properties.Resources.StringJordan;
            RfidEtsiRegionsArray[4] = Properties.Resources.StringMorocco;
            RfidEtsiRegionsArray[5] = Properties.Resources.StringPakistan;
            RfidEtsiRegionsArray[6] = Properties.Resources.StringRussia;

            RfidJapanRegionsArray[0] = Properties.Resources.StringJapan1w;
            RfidJapanRegionsArray[1] = Properties.Resources.StringJapan250mW;

            RfidChinaRegionsArray[0] = Properties.Resources.StringChina;
        }

        public static string ComPort
        {
            get => mComPort;
            set => mComPort = value;
        }

        public static int Baudrate
        {
            get => mBaudrate;
            set
            {
                if (value > 0)
                {
                    mBaudrate = value;
                }
            }
        }

        public static int NumberOfAntennaPorts
        {
            get => mNumberOfAntennaPorts;
            set
            {
                if (value > 0)
                {
                    mNumberOfAntennaPorts = value;
                }
            }
        }

        public static Scanner Scanner
        {
            get => mScanner;
            set => mScanner = value;
        }

        public static Reader Reader
        {
            get => mReader;
            set
            {
                mReader = value;
                if (value != null)
                {
                    mSensorReader = new SensorReader(value);
                }
                else
                {
                    mSensorReader = null;
                }
            }
        }

        public static SensorReader SensorReader
        {
            get => mSensorReader;
        }

        public static bool ScannerConnected
        {
            get => mScannerConnected;
            set => mScannerConnected = value;
        }

        public static bool ReaderConnected
        {
            get => mReaderConnected;
            set => mReaderConnected = value;
        }

        public static void SetHidSettings(HidTerminator terminator, string prefix, string surfix)
        {
            mHidTerminator = terminator;
            mHidPrefix = prefix;
            mHidSurfix = surfix;
        }

        public static HidTerminator HidSettingTerminator
        {
            get => mHidTerminator;
            set => mHidTerminator = value;
        }

        public static string HidSettingPrefix
        {
            get => mHidPrefix;
            set => mHidPrefix = value;
        }

        public static string HidSettingSuffix
        {
            get => mHidSurfix;
            set => mHidSurfix = value;
        }
    }
}
