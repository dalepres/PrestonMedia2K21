using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Preston.Media
{
    [Serializable]
    public class MediaAttribute
    {
        private string attributeName;
        private bool isReadOnly;
        private bool includeInBackup;
        private bool includeInXlst;
        private int sortOrder;

        public MediaAttribute()
        {
        }

        public MediaAttribute(string attributeName, bool isReadOnly)
        {
            this.attributeName = attributeName;
            this.isReadOnly = isReadOnly;
        }

        #region Properties

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        public string AttributeNodeName
        {
            get { return attributeName.Replace("WM/", "WM_"); }
        }


        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }
        }


        public bool IncludeInBackup
        {
            get { return includeInBackup; }
            set { includeInBackup = value; }
        }


        public bool IncludeInXslt
        {
            get { return includeInXlst; }
            set { includeInXlst = value; }
        }


        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        #endregion Properties

        internal static ListViewItem ToListViewItem(MediaAttribute attribute)
        {
            return ToListViewItem(attribute, false);
        }

        internal static ListViewItem ToListViewItem(MediaAttribute attribute, bool itemIsChecked)
        {
            ListViewItem item = new ListViewItem(attribute.AttributeName);
            item.Tag = attribute;
            item.SubItems.Add(attribute.IsReadOnly.ToString());
            item.Checked = itemIsChecked;
            return item;
        }
    }
}
