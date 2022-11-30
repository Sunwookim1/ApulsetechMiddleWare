using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace a211_AutoCabinet.Class
{
    public class TagItemList
    {
        //private const string TAG = "TagItemList";
        //private const bool D = false;

        private List<TagItem> mItemList = new List<TagItem>();
        private Dictionary<string, TagItem> mItemDictionary = new Dictionary<string, TagItem>();

        private int mUnfilteredCount = 0;
        private bool mFilterEnabled = false;

        public List<TagItem> ItemList
        {
            get
            {
                return mItemList;
            }
        }

        public int Count
        {
            get
            {
                return mItemList.Count;
            }
        }

        public int UnfilteredCount
        {
            get
            {
                return mUnfilteredCount;
            }
        }

        public int DupCount(int index)
        {
            if ((index >= 0) && (index < mItemList.Count))
            {
                return mItemList[index].DupCount;
            }

            return 0;
        }

        private TagItem FindItem(string tagValue)
        {
            if (tagValue != null)
            {
                if (mItemDictionary.ContainsKey(tagValue))
                {
                    return mItemDictionary[tagValue];
                }
            }

            return null;
        }

        public bool Contains(string tagValue)
        {
            if (tagValue != null)
            {
                return mItemDictionary.ContainsKey(tagValue);
            }

            return false;
        }

        public void AddItem(string tagValue,
                            string rssiValue,
                            string phaseValue,
                            string fastIdValue,
                            string channelValue,
                            string portValue,
                            bool filter)
        {
            if (tagValue == null)
            {
                return;
            }

            mFilterEnabled = filter;

            if (filter)
            {
                TagItem storedItem = FindItem(tagValue);
                if (storedItem != null)
                {
                    storedItem.RssiValue = rssiValue;
                    storedItem.PhaseValue = phaseValue;
                    storedItem.FastIdValue = fastIdValue;
                    storedItem.ChannelValue = channelValue;
                    storedItem.PortValue = portValue;
                    storedItem.DupCount += 1;

                    mUnfilteredCount++;
                    return;
                }

                TagItem item = new TagItem
                {
                    TagValue = tagValue,
                    RssiValue = rssiValue,
                    PhaseValue = phaseValue,
                    FastIdValue = fastIdValue,
                    ChannelValue = channelValue,
                    PortValue = portValue,
                    DupCount = 1
                };

                mItemList.Add(item);
                mItemDictionary.Add(tagValue, item);

                mUnfilteredCount++;
            }
            else
            {
                TagItem item = new TagItem
                {
                    TagValue = tagValue,
                    RssiValue = rssiValue,
                    PhaseValue = phaseValue,
                    FastIdValue = fastIdValue,
                    ChannelValue = channelValue,
                    PortValue = portValue,
                    DupCount = 1
                };

                mItemList.Add(item);
                //mItemDictionary.Add(tagValue, item);

                mUnfilteredCount++;
            }
        }

        public void AddItem(string tagValue,
                    string rssiValue,
                    string phaseValue,
                    string fastIdValue,
                    string channelValue,
                    bool filter)
        {
            AddItem(tagValue, rssiValue, phaseValue, fastIdValue, channelValue, "0", filter);
        }

        public void Clear()
        {
            mItemList.Clear();
            mUnfilteredCount = 0;
            mItemDictionary.Clear();
        }
    }
}
