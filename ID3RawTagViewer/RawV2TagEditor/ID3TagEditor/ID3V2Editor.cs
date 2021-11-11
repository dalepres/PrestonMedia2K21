using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ID3Lib;
using System.ComponentModel.Design;

namespace Preston.Media
{
    public partial class ID3V2Editor : UserControl, IID3Editor
    {
        object id3Tag;
        ID3TagRawViewerUC id3TagRawViewer;
        //ID3Lib.ID3Genres genreList = new ID3Lib.ID3Genres();
        int id3Encoding = 0;  // TODO:  Add more robust encoding dependent on the user's OS language.

        public ID3V2Editor()
        {
            InitializeComponent();

            Location = new Point(0, 0);
            id3TagRawViewer = new ID3TagRawViewerUC();
            id3TagRawViewer.Top = 9;
            id3TagRawViewer.Left = 0;
            tpRawView.Controls.Add(id3TagRawViewer);
            id3TagRawViewer.FrameDeleted += new FrameDeletedEventHandler(id3TagRawViewer_FrameDeleted);
        }

        void id3TagRawViewer_FrameDeleted(object sender, FrameDeletedEventArgs e)
        {
            DisplayId3Tag();
            if (e.Frame is V2Frame)
            {
                OnFrameDeleted(e);
                //MessageBox.Show(((V2Frame)e.Frame).FrameId + " was deleted.");
            }
        }


        public event FrameDeletedEventHandler FrameDeleted;

        protected virtual void OnFrameDeleted(FrameDeletedEventArgs e)
        {
            if (FrameDeleted != null)
            {
                FrameDeleted(this, e);
            }
        }
        #region Event Handlers

        #endregion Event Handlers

        #region Properties

        #endregion Properties

        #region Private Methods


        #endregion Private Methods

        private void ID3V2Editor_Load(object sender, EventArgs e)
        {
        }

        #region iId3Editor Members

        public object Id3Tag
        {
            get { return id3Tag; }
            set
            {
                id3Tag = value;
                DisplayId3Tag();
            }
        }

        private void DisplayId3Tag()
        {
            id3TagRawViewer.Id3Tag = id3Tag;
            if (id3Tag is V1Tag)
            {
                if (((V1Tag)id3Tag).MajorVersion == 0)
                {
                    lblTitle.Text = "ID3 Tag Version 1.0";
                }
                else
                {
                    lblTitle.Text = "ID3 Tag Version 1.1";
                }
            }
            else if (id3Tag is V22Tag)
            {
                lblTitle.Text = "ID3 Tag Version 2.2";
            }
            else if (id3Tag is V23Tag)
            {
                lblTitle.Text = "ID3 Tag Version 2.3";
            }
            else
            {
                lblTitle.Text = "No tag selected.";
            }
        }


        #endregion


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateId3Tag();
            if (tabControl1.SelectedTab.Name == "tpRawView")
            {
                id3TagRawViewer.ReloadTag();
            }
        }

        private void UpdateId3Tag()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region IId3Editor Members


        public void ClearTag()
        {
            id3Tag = null;

            //this.rbV23.Checked = true;

            this.Visible = false;
        }

        #endregion
    }
}
