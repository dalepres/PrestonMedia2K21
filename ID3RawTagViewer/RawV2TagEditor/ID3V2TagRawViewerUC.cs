using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ID3Lib;

namespace Preston.Media
{
    public partial class ID3V2TagRawViewerUC : UserControl
    {
        object id3Tag;
        ByteViewer byteViewer;

        public ID3V2TagRawViewerUC()
        {
            InitializeComponent();

            byteViewer = new ByteViewer();
            byteViewer.Dock = DockStyle.Fill;
            byteViewer.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            pnlHexView.Controls.Add(byteViewer);
        }

        private void btnDeleteFrame_Click(object sender, EventArgs e)
        {
            object frame;
            int index = cbFrames.SelectedIndex;
            if (id3Tag is V23Tag)
            {
                V23Tag t = (V23Tag)id3Tag;
                frame = t.Frames[index];
                t.Frames.RemoveAt(index);
            }
            else if (id3Tag is V22Tag)
            {
                V22Tag t = (V22Tag)id3Tag;
                frame = t.Frames[index];
                t.Frames.RemoveAt(index);
            }
            else
            {
                return;
            }

            FrameDeletedEventArgs fde = new FrameDeletedEventArgs(frame);
            ReloadTag();
            if (index <= cbFrames.Items.Count - 1)
            {
                cbFrames.SelectedIndex = index;
            }
            else
            {
                cbFrames.SelectedIndex = cbFrames.Items.Count - 1;
            }
            OnFrameDeleted(fde);
        }

        private void RebuildTag()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public event FrameDeletedEventHandler FrameDeleted;

        protected virtual void OnFrameDeleted(FrameDeletedEventArgs e)
        {
            if (FrameDeleted != null)
            {
                FrameDeleted(this, e);
            }
        }

        #region Properties

        public object Id3Tag
        {
            get { return id3Tag; }
            set
            {
                id3Tag = value;
                ReloadTag();
            }
        }

        #endregion Properties


        #region Private Methods

        private void ListV22Frames(V22Tag v22Tag)
        {
            IEnumerator<V22Frame> ie = v22Tag.Frames.GetEnumerator();
            if (ie != null)
            {
                while (ie.MoveNext())
                {
                    cbFrames.Items.Add(ie.Current);
                }
            }

            cbFrames.SelectedIndex = 0;
        }

        private void ListV23Frames(V23Tag v23Tag)
        {
            IEnumerator<V2Frame> ie = v23Tag.Frames.GetEnumerator();
            if (ie != null)
            {
                while (ie.MoveNext())
                {
                    cbFrames.Items.Add(ie.Current);
                }
            }

            if (cbFrames.Items.Count > 0)
            {
                cbFrames.SelectedIndex = 0;
            }
        }

        private void ListV1Frames(V1Tag v1Tag)
        {
            IEnumerator<V1Frame> ie = v1Tag.Frames.GetEnumerator();
            if (ie != null)
            {
                while (ie.MoveNext())
                {
                    cbFrames.Items.Add(ie.Current);
                }
            }

            cbFrames.SelectedIndex = 0;
        }

        private void cbFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFrames.SelectedItem is V1Frame)
            {
                byteViewer.SetBytes(((V1Frame)cbFrames.SelectedItem).FrameValue);
            }
            else if (cbFrames.SelectedItem is V2Frame)
            {
                lblFrameDescription.Text = ((V2Frame)cbFrames.SelectedItem).FrameDescription;
                byteViewer.SetBytes(((V2Frame)cbFrames.SelectedItem).FrameValue);
            }
            else if (cbFrames.SelectedItem is V22Frame)
            {
                byteViewer.SetBytes(((V22Frame)cbFrames.SelectedItem).FrameValue);
            }
        }

        #endregion Private Methods

        internal void ReloadTag()
        {
            cbFrames.Items.Clear();
            byteViewer.SetBytes(new byte[] { });

            if (id3Tag is V22Tag)
            {
                ListV22Frames((V22Tag)id3Tag);
                btnDeleteFrame.Visible = true;
            }
            else if (id3Tag is V23Tag)
            {
                ListV23Frames((V23Tag)id3Tag);
                btnDeleteFrame.Visible = true;
            }
            else if (id3Tag is V1Tag)
            {
                ListV1Frames((V1Tag)id3Tag);
                btnDeleteFrame.Visible = false;
            }
        }
    }

    //public class FrameDeletedEventArgs : System.EventArgs
    //{
    //    object frame;

    //    public FrameDeletedEventArgs(object frame)
    //    {
    //        this.frame = frame;
    //    }

    //    public object Frame
    //    {
    //        get { return frame; }
    //    }
    //}

    //public delegate void FrameDeletedEventHandler(object sender, FrameDeletedEventArgs e);

}
