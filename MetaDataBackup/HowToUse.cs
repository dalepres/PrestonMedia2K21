using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Preston.Media
{
    public partial class HowToUse : Form
    {
        public HowToUse()
        {
            InitializeComponent();
        }

        private void HowToUse_Load(object sender, EventArgs e)
        {
            string rtf;
            if (File.Exists("MetaDataBackupHelp.rtf"))
            {
                try
                {
                    FileStream fs = File.Open("MetaDataBackupHelp.rtf", FileMode.Open);

                    byte[] bytes = new byte[fs.Length];

                    fs.Read(bytes, 0, bytes.Length);
                    Encoding enc = System.Text.ASCIIEncoding.UTF8;

                    rtf = enc.GetString(bytes);
                    this.richTextBox1.Rtf = rtf;
                }
                catch
                {
                    richTextBox1.Text = "\r\n\r\nThe local help file could not be opened.  Check for more information about MetaData Backup at http://www.DalePreston.com/";
                }
            }
            else
            {
                richTextBox1.Text = "\r\n\r\nThe local help file was not found.  Check for more information about MetaData Backup at http://www.DalePreston.com/";
            }
        }
    }
}