using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preston.Media
{
    public partial class RawV2TagEditor : Form
    {
        ID3V2TagRawViewerUC id3TagRawViewer;
        public RawV2TagEditor()
        {
            InitializeComponent();

            InitializeID3RawTagViewerUC();
        }

        private void InitializeID3RawTagViewerUC()
        {
            id3TagRawViewer = new ID3V2TagRawViewerUC();
            id3TagRawViewer.Top = txtFilePath.Top + txtFilePath.Height + 9;
            id3TagRawViewer.Left = 7;
            this.Controls.Add(id3TagRawViewer);
            id3TagRawViewer.FrameDeleted += new FrameDeletedEventHandler(id3TagRawViewer_FrameDeleted);
        }

        private void RawV2TagEditor_Load(object sender, EventArgs e)
        {

        }

        private void id3TagRawViewer_FrameDeleted(object sender, FrameDeletedEventArgs e)
        {
        }
    }
}