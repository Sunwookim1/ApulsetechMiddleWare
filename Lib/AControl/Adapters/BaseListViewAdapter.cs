using System.Windows.Forms;

namespace AControl.Adapters
{
    public abstract class BaseListViewAdapter
    {
        protected ListViewItem[] m_Cache;
        protected int m_nFirstItem;

        public BaseListViewAdapter()
        {
            m_Cache = null;
            m_nFirstItem = -1;
        }

        // VirtualItem 검색 
        public void CacheVirtualItems(CacheVirtualItemsEventArgs e)
        {
            if (m_Cache != null && e.StartIndex >= m_nFirstItem &&
                e.EndIndex <= m_nFirstItem + m_Cache.Length)
                return;
            m_nFirstItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            m_Cache = new ListViewItem[length];
            for (int i = 0; i < length; i++)
                m_Cache[i] = GetItem(i);
        }

        public void RetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            if (m_Cache != null && e.ItemIndex >= m_nFirstItem &&
                e.ItemIndex < m_nFirstItem + m_Cache.Length)
            {
                e.Item = m_Cache[e.ItemIndex - m_nFirstItem];
                UpdateItem(e.ItemIndex, e.Item);
            }
            else
            {
                e.Item = GetItem(e.ItemIndex);
            }
        }

        public abstract void Clear();
        public abstract int Count { get; }

        private ListViewItem GetItem(int index)
        {
            ListViewItem item = GetNewItem();
            UpdateItem(index, item);
            return item;
        }
        protected abstract ListViewItem GetNewItem();
        protected abstract void UpdateItem(int index, ListViewItem item);
    }
}
