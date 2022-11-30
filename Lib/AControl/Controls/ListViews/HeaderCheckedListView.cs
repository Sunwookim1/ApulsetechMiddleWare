using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AControl.Controls.ListViews
{
    public partial class HeaderCheckedListView : ListView
    {
        private CheckBoxState m_LastState;
        private CheckBoxState m_CheckBoxState;

        public HeaderCheckedListView()
        {
            InitializeComponent();

            CheckBoxes = true;
            FullRowSelect = true;
            GridLines = true;
            HideSelection = false;
            OwnerDraw = true;

            m_LastState = m_CheckBoxState = CheckBoxState.CheckedNormal;
            this.DoubleBuffered(true);
        }

        public void UpdateHeader()
        {
            int checkCount = 0;
            int uncheckCount = 0;

            foreach (ListViewItem item in Items)
            {
                if (item.Checked)
                    checkCount++;
                else
                    uncheckCount++;
            }
            if (checkCount == Items.Count && uncheckCount == 0)
            {
                if (m_CheckBoxState != CheckBoxState.MixedNormal)
                    m_LastState = m_CheckBoxState;
                m_CheckBoxState = CheckBoxState.CheckedNormal;
            }
            else if (checkCount == 0 && uncheckCount == Items.Count)
            {
                if (m_CheckBoxState != CheckBoxState.MixedNormal)
                    m_LastState = m_CheckBoxState;
                m_CheckBoxState = CheckBoxState.UncheckedNormal;
            }
            else
            {
                m_CheckBoxState = CheckBoxState.MixedNormal;
            }
            InvalidateHeader(0);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.DrawBackground();
                CheckBoxRenderer.DrawCheckBox(e.Graphics,
                    new Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                    m_CheckBoxState);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected override void OnItemChecked(ItemCheckedEventArgs e)
        {
            UpdateHeader();
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                if (HitTest(e.Column))
                {
                    bool check = false;
                    int checkCount = 0;
                    int uncheckCount = 0;

                    switch (m_CheckBoxState)
                    {
                        case CheckBoxState.CheckedNormal:
                            m_LastState = m_CheckBoxState;
                            m_CheckBoxState = CheckBoxState.UncheckedNormal;
                            check = false;
                            break;
                        case CheckBoxState.UncheckedNormal:
                            m_LastState = m_CheckBoxState;
                            m_CheckBoxState = CheckBoxState.CheckedNormal;
                            check = true;
                            break;
                        case CheckBoxState.MixedNormal:
                            foreach (ListViewItem item in Items)
                            {
                                if (item.Checked)
                                    checkCount++;
                                else
                                    uncheckCount++;
                            }
                            if (checkCount > uncheckCount)
                            {
                                m_LastState = CheckBoxState.UncheckedNormal;
                                m_CheckBoxState = CheckBoxState.CheckedNormal;
                                check = true;
                            }
                            else if (checkCount < uncheckCount)
                            {
                                m_LastState = CheckBoxState.CheckedNormal;
                                m_CheckBoxState = CheckBoxState.UncheckedNormal;
                                check = false;
                            }
                            else
                            {
                                switch (m_LastState)
                                {
                                    case CheckBoxState.CheckedNormal:
                                        m_CheckBoxState = CheckBoxState.UncheckedNormal;
                                        check = false;
                                        break;
                                    case CheckBoxState.UncheckedNormal:
                                        m_CheckBoxState = CheckBoxState.CheckedNormal;
                                        check = true;
                                        break;
                                }
                            }
                            break;
                    }
                    foreach (ListViewItem item in Items)
                        item.Checked = check;
                    Invalidate();
                }
                return;
            }
            base.OnColumnClick(e);
        }

        private bool HitTest(int index)
        {
            Point pt = Cursor.Position;
            pt = PointToClient(pt);
            IntPtr headerCtrl = GetHeaderControl(this);
            IntPtr hdc = GetDC(headerCtrl);
            Graphics g = Graphics.FromHdc(hdc);
            Rectangle rc = GetHeaderBound(this, headerCtrl, index);
            Size size = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
            Rectangle bound = new Rectangle(rc.Left + 4, rc.Top + 4, size.Width, size.Height);
            bool contain = bound.Contains(pt);
            ReleaseDC(headerCtrl, hdc);
            return contain;
        }

        private void InvalidateHeader(int index)
        {
            IntPtr headerCtrl = GetHeaderControl(this);
            Rectangle bound = GetHeaderBound(this, headerCtrl, index);
            RECT rc = new RECT();
            rc.Left = bound.Left;
            rc.Top = bound.Top;
            rc.Right = bound.Right;
            rc.Bottom = bound.Bottom;
            InvalidateRect(headerCtrl, ref rc, true);
        }

        private Rectangle GetHeaderBound(System.Windows.Forms.ListView list, IntPtr hwnd, int index)
        {
            Rectangle rc = GetHeaderRect(hwnd);
            int x = 0;
            for (int i = 0; i < list.Columns.Count; i++)
            {
                if (i == index)
                {
                    return new Rectangle(x, rc.Top, list.Columns[i].Width, rc.Height);
                }
                x += list.Columns[i].Width;
            }
            return new Rectangle();
        }

        private const uint LVM_GETHEADER = 0x1000 + 31;
        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [Serializable, StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public Int32 x;
            public Int32 y;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hwnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);
        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
        [DllImport("user32.dll")]
        private static extern bool InvalidateRect(IntPtr hWnd, ref RECT lpRect, bool bErase);

        private static IntPtr GetHeaderControl(System.Windows.Forms.ListView list)
        {
            return SendMessage(list.Handle, LVM_GETHEADER, 0, 0);
        }
        private static Rectangle GetHeaderRect(IntPtr hwnd)
        {
            RECT rc = new RECT();
            if (!GetWindowRect(new HandleRef(null, hwnd), out rc))
            {
                return new Rectangle();
            }
            POINT pt1 = ScreenToClient(hwnd, rc.Left, rc.Top);
            POINT pt2 = ScreenToClient(hwnd, rc.Right, rc.Bottom);

            return new Rectangle(pt1.x, pt1.y, pt2.x - pt1.x, pt2.y - pt1.y);
        }

        private static POINT ScreenToClient(IntPtr hwnd, int x, int y)
        {
            POINT pt = new POINT();
            pt.x = x;
            pt.y = y;
            ScreenToClient(hwnd, ref pt);
            return pt;
        }
    }
}
