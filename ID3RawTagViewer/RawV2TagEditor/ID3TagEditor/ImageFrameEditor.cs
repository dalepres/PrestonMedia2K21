using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ID3Lib;

namespace Preston.Media.ID3TagEditor
{
    public partial class ImageFrameEditor : UserControl
    {
        private object picFrame;
        private bool readOnly;
        private Image picture;
        private bool isSelected;

        public event ImageFrameEditorEventHandler Selected;


        public ImageFrameEditor()
        {
            InitializeComponent();
            cbImageTypes.DataSource = ID3PictureTypes.GetPictureTypes();
            cbImageTypes.DisplayMember = "PictureType";
        }

        #region Properties

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                cbImageTypes.Visible = !value;
                txtImageType.Enabled = !value;
                txtDescription.Enabled = !value;
                cbImageTypes.Enabled = !value;
                txtImageType.Visible = value;
            }
        }

        public Image Picture
        {
            get { return picture; }
        }

        public object PicFrame
        {
            get { return picFrame; }
            set
            {
                picFrame = value;

                if (picFrame is APICFrame)
                {
                    APICFrame aFrame = (APICFrame)picFrame;
                    pictureBox.Image = aFrame.GetPicture(pictureBox.Size);
                    this.picture = pictureBox.Image;
                    txtDescription.Text = aFrame.Description;
                    txtImageType.Text = ID3Lib.ID3PictureTypes.GetPictureType(aFrame.PictureType).PictureType;
                    cbImageTypes.Text = ID3Lib.ID3PictureTypes.GetPictureType(aFrame.PictureType).PictureType;
                }
                else if (picFrame is PICFrame)
                {
                }
            }
        }


        public bool IsSelected
        {
            get { return isSelected; }
            set { Unselect(); }
        }

        #endregion Properties


        protected virtual void OnSelected(ImageFrameEditorEventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }


        public void CopyFromImageFrameEditor(ImageFrameEditor editor)
        {
            this.picFrame = editor.PicFrame;
            this.pictureBox.Image = editor.picture;
            this.cbImageTypes.SelectedItem = editor.cbImageTypes.SelectedItem;
            this.txtDescription.Text = editor.txtDescription.Text;
        }


        private void cbImageTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtImageType.Text = ID3PictureTypes.GetPictureType(cbImageTypes.Text).PictureType;
        }

        private void ImageFrameEditor_Load(object sender, EventArgs e)
        {

        }

        private void ImageFrameEditor_Enter(object sender, EventArgs e)
        {
            ImageFrameEditorEventArgs args = new ImageFrameEditorEventArgs(
                this.picFrame,
                (ID3PictureType)cbImageTypes.SelectedItem,
                txtDescription.Text,
                pictureBox.Image);

            OnSelected(args);
        }

        private void ImageFrameEditor_Leave(object sender, EventArgs e)
       {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ImageFrameEditor_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ImageFrameEditor_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Image img = Image.FromFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
                Image img2 = (Image)img.Clone();
                img.Dispose();
                img = null;
                APICFrame frame = new APICFrame(
                    GetDescription(), GetPictureType(), img2);
                this.picFrame = frame;
                pictureBox.Image = frame.GetPicture(pictureBox.Size);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private byte GetPictureType()
        {
            return ((ID3PictureType)cbImageTypes.SelectedItem).PictureTypeId;
        }

        private string GetDescription()
        {
            return txtDescription.Text.Trim();
        }

        internal void Clear()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescription.Enabled = true;
            this.txtDescription.Text = string.Empty;
            this.cbImageTypes.Enabled = true;
            this.cbImageTypes.SelectedIndex = 0;
            this.txtImageType.Text = cbImageTypes.Text;
            this.PicFrame = null;
            this.picture = null;
        }

        internal void Unselect()
        {
            this.isSelected = false;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescription.BackColor = Color.FromKnownColor(KnownColor.Control);
            txtImageType.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        internal new void Select()
        {
            this.isSelected = true;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.txtDescription.BackColor = Color.FromKnownColor(KnownColor.Window);
            this.txtImageType.BackColor = Color.FromKnownColor(KnownColor.Window);
        }
    }

    public delegate void ImageFrameEditorEventHandler(object sender, ImageFrameEditorEventArgs e);

    public class ImageFrameEditorEventArgs
    {

        private object frame;
        private ID3PictureType pictureType;
        private string description;
        private Image picture;

        public ImageFrameEditorEventArgs(
            object frame,
            ID3PictureType pictureType,
            string description,
            Image picture)
        {
            this.frame = frame;
            this.pictureType = pictureType;
            this.description = description;
            this.picture = picture;
        }

        public ID3PictureType PictureType
        {
            get { return pictureType; }
        }

        public string Description
        {
            get { return description; }
        }

        public Image Picture
        {
            get { return picture; }
        }

        public object Frame
        {
            get { return frame; }
        }
    }
}
