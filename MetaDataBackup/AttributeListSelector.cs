using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preston.Media
{
    public partial class AttributeListSelector : Form
    {
        private MediaAttributeCollection collection;

        public AttributeListSelector(MediaAttributeCollection collection)
        {
            InitializeComponent();

            this.collection = collection;
            listView1.Columns.Add("Attribute Name", 220, HorizontalAlignment.Right);
            listView1.Columns.Add("Read-Only", 80, HorizontalAlignment.Center);

            //MediaAttributeCollection mac = new MediaAttributeCollection(Properties.Settings.Default.LastAttributeList.ToArray());

            //LoadListView(MediaAttributeCollectionFactory.CreateDefaultCollection());

            LoadListView(collection);
        }

        private void LoadListView(MediaAttributeCollection mac)
        {
            //MediaAttributeCollection mac = MediaAttributeCollectionFactory.CreateDefaultCollection();
            foreach (MediaAttribute attribute in mac)
            {
                listView1.Items.Add(MediaAttribute.ToListViewItem(attribute, true));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.collection.Clear();

            this.collection.AddRange(this.GetSelectedAttributeList());

            Properties.Settings.Default.LastAttributeList = this.collection;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private MediaAttributeCollection GetSelectedAttributeList()
        {
            MediaAttributeCollection mac = new MediaAttributeCollection();
            for (int count = 0; count < this.listView1.Items.Count; count++)
            {
                if (this.listView1.Items[count].Checked)
                {
                    mac.Add((MediaAttribute)this.listView1.Items[count].Tag);
                }
            }

            return mac;
        }

        private void btnSaveList_Click(object sender, EventArgs e)
        {

        }

        private void defaultListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MediaAttributeCollection mac = MediaAttributeCollectionFactory.CreateDefaultCollection();
            LoadListView(mac);
        }
    }
}