using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ID3Lib;

namespace Preston.Media.ID3TagEditor
{

    public partial class ImageFramesEditor : UserControl
    {
        public ImageFramesEditor()
        {
            InitializeComponent();
        }

        private object id3Tag;

        internal void LoadImages(object id3Tag)
        {
            if (id3Tag is V23Tag)
            {
                LoadV23Images((V23Tag)id3Tag);
            }
            else if (id3Tag is V22Tag)
            {
                LoadV22Images((V22Tag)id3Tag);
            }
            else if(id3Tag != null)
            {
                throw new ArgumentException("Not a valid ID3 V2 Tag.");
            }

            this.id3Tag = id3Tag;
        }

        private void LoadV22Images(V22Tag v22Tag)
        {
            PICFrame aFrame;
            tableLayoutPanel1.Controls.Clear();
            foreach (V22Frame frame in v22Tag.Frames)
            {
                if ((aFrame = frame as PICFrame) != null)
                {
                    AddFrame(aFrame);
                }
            }
        }

        private void LoadV23Images(V23Tag v23Tag)
        {
            APICFrame aFrame;
            tableLayoutPanel1.Controls.Clear();
            foreach (V2Frame frame in v23Tag.Frames)
            {
                if ((aFrame = frame as APICFrame) != null)
                {
                    AddFrame(aFrame);
                }
            }
        }

        private void AddFrame(object aFrame)
        {
            ImageFrameEditor editor = new ImageFrameEditor();
            editor.PicFrame = aFrame;
            editor.ReadOnly = true;
            editor.Selected += new ImageFrameEditorEventHandler(PictureFrameEditorSelected);
            tableLayoutPanel1.Controls.Add(editor);
            editor.Anchor = AnchorStyles.Left;
        }


        private void PictureFrameEditorSelected(object sender, ImageFrameEditorEventArgs e)
        {
            ImageFrameEditor ed = (ImageFrameEditor)sender;
            ed.Select();
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is ImageFrameEditor && c != ed)
                {
                    ((ImageFrameEditor)c).Unselect();
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            imageFrameEditor1.Clear();
            imageFrameEditor1.Visible = true;
            btnCancel.Visible = true;
            btnNew.Visible = false;
            btnDone.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            imageFrameEditor1.Clear();
            imageFrameEditor1.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = true;
            btnDone.Visible = false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                object aFrame = imageFrameEditor1.PicFrame;

                if (id3Tag is V23Tag)
                {
                    ((V23Tag)id3Tag).Frames.Add((APICFrame)aFrame);
                }
                else if (id3Tag is V22Tag)
                {
                    ((V22Tag)id3Tag).Frames.Add((PICFrame)aFrame);
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show(fex.Message);
            }

            AddFrame(imageFrameEditor1.PicFrame);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            ImageFrameEditor ed;
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                count++;
                if (c is ImageFrameEditor)
                {
                    ed = (ImageFrameEditor)c;
                    if (ed.IsSelected)
                    {
                        tableLayoutPanel1.Controls.Remove(c);
                        if (count < tableLayoutPanel1.Controls.Count && tableLayoutPanel1.Controls[count] is ImageFrameEditor)
                        {
                            ed = (ImageFrameEditor)tableLayoutPanel1.Controls[count];
                            ed.Select();
                        }

                        break;
                    }
                }
            }
        }
    }
}
