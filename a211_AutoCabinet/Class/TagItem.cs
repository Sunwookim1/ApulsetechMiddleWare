using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using a211_AutoCabinet.Datas;

namespace a211_AutoCabinet.Class
{
    public class TagItem
    {
        private string mTagValue;
        private string mRssiValue;
        private Queue<float> mRssiQueue;
        private string mPhaseValue;
        private Queue<float> mPhaseQueue;
        private string mFastIdValue;
        private string mChannelValue;
        private string mPort;
        private int mDupCount;

        public TagItem()
        {
            mTagValue = null;
            mRssiValue = null;
            mRssiQueue = new Queue<float>(SharedValues.MaxRfidRssiValueQueueSize);
            mPhaseValue = null;
            mPhaseQueue = new Queue<float>(SharedValues.MaxRfidPhaseValueQueueSize);
            mFastIdValue = null;
            mPort = null;
            mDupCount = 0;
        }

        public string TagValue
        {
            get
            {
                return mTagValue;
            }

            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    mTagValue = value;
                }
            }
        }

        public string RssiValue
        {
            get
            {
                return mRssiValue;
            }

            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    mRssiValue = value;
                    float rssiValue = 0.0f;
                    try
                    {
                        rssiValue = float.Parse(value, CultureInfo.CurrentCulture);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    catch (ArgumentNullException)
                    {

                    }
                    catch (ArgumentException)
                    {

                    }
                    catch (FormatException)
                    {

                    }
                    catch (OverflowException)
                    {

                    }

                    RssiMa = rssiValue;
                }
            }
        }

        public float RssiMa
        {
            get
            {
                return mRssiQueue.Average();
            }

            set
            {
                if (mRssiQueue.Count >= SharedValues.MaxRfidRssiValueQueueSize)
                {
                    mRssiQueue.Dequeue();
                }
                mRssiQueue.Enqueue(value);
            }
        }

        public string PhaseValue
        {
            get
            {
                return mPhaseValue;
            }

            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    mPhaseValue = value;
                    float phaseValue = 0.0f;
                    try
                    {
                        phaseValue = float.Parse(value, CultureInfo.CurrentCulture);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    catch (ArgumentNullException)
                    {

                    }
                    catch (ArgumentException)
                    {

                    }
                    catch (FormatException)
                    {

                    }
                    catch (OverflowException)
                    {

                    }

                    PhaseMa = phaseValue;
                }
            }
        }

        public float PhaseMa
        {
            get
            {
                return mPhaseQueue.Average();
            }

            set
            {
                if (mPhaseQueue.Count >= SharedValues.MaxRfidPhaseValueQueueSize)
                {
                    mPhaseQueue.Dequeue();
                }
                mPhaseQueue.Enqueue(value);
            }
        }

        public string FastIdValue
        {
            get
            {
                return mFastIdValue;
            }

            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    mFastIdValue = value;
                }
            }
        }

        public string ChannelValue
        {
            get
            {
                return mChannelValue;
            }

            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    mChannelValue = value;
                }
            }
        }

        public string PortValue
        {
            get
            {
                return mPort;
            }

            set
            {
                mPort = value;
            }
        }

        public int DupCount
        {
            get
            {
                return mDupCount;
            }

            set
            {
                mDupCount = value;
            }
        }
    }
}
