using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ID3Lib;
using Preston.Media.Properties;

namespace Preston.Media
{
    public partial class TrackTagEditor : Form
    {
        private string mp3FileName;
        //private ArrayList tagList = new ArrayList();
        //private ID3V2Editor id3V2Editor = new ID3V2Editor();
        //private ID3V1Editor id3V1Editor = new ID3V1Editor();
        private V1Tag v1Tag;
        private V22Tag v22Tag;
        private List<V2Tag> v2Tags;
        private object currentTag;

        public TrackTagEditor()
        {
            InitializeComponent();
        }

        private void TrackTagEditor_Load(object sender, EventArgs e)
        {
        }


#region Properties

        public string Mp3FileName
        {
            get { return mp3FileName; }
            set
            {
                v1Tag = null;
                v22Tag = null;
                v2Tags = null;
                iD3V2Editor.Visible = false;
                iD3V2Editor.Id3Tag = null;
                lblTrackName.Text = value.Replace("&", "&&");
                btnChangeFileName.Left = lblTrackName.Left + lblTrackName.Width + 4;
                btnChangeFileName.Enabled = true;
                cbTags.Items.Clear();
                cbTags.Enabled = false;
                btnDelete.Enabled = false;
                mp3FileName = value;
                GetID3Tags(value);
            }
        }

        #endregion Properties

        #region Private Methods


        //private void DisplayV2TagEditor()
        //{
        //    panel1.Controls.Clear();
        //    id3V2Editor = new ID3V2Editor();
        //    id3V2Editor.Location = new Point(0, 0);
        //    panel1.Controls.Add(id3V2Editor);
        //}

        //private void DisplayV1TagEditor()
        //{
        //    panel1.Controls.Clear();
        //    id3V1Editor = new ID3V1Editor();
        //    id3V1Editor.Location = new Point(0, 0);
        //    panel1.Controls.Add(id3V1Editor);
        //}


        private void GetID3Tags(string fileName)
        {
            //bool hasTags = false;
            Mp3File file = new Mp3File(fileName);
            //file.SaveTagsAsBackup(fileName);
            //file.SplitFileAndTag();
            try
            {
                v1Tag = file.GetV1Tag();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid ID3V1 tag encountered that could not be displayed.\n\n"
                    + ex.Message);
            }

            try
            {
                v22Tag = file.GetV22Tag();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid ID3V2.2 tag encountered that could not be displayed.\n\n"
                    + ex.Message);
            }

            try
            {
                v2Tags = file.GetV2Tags(false); //!rbNoCopyConvert.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid ID3V2.3 tag encountered that could not be displayed.\n\n"
                    + ex.Message);
            }

            file.Close();


            DisplayID3Tags();
            btnCancel.Enabled = btnApply.Enabled = false;

        }

        private void DisplayID3Tags()
        {
            currentTag = null;
            cbTags.Items.Clear();

            if (v1Tag != null)
            {
                AddV1TagToList(v1Tag);
                currentTag = v1Tag;
                //hasTags = true;
            }

            if (v22Tag != null)
            {
                AddV22TagToList(v22Tag);
                if (currentTag == null)
                {
                    currentTag = v22Tag;
                }
            }

            if (v2Tags != null && v2Tags.Count > 0)
            {
                AddV2TagsToList(v2Tags);
                //iD3V2Editor.Id3Tag = v2Tags[0];

                if (currentTag == null)
                {
                    currentTag = v2Tags[0];
                }

            }

            //if (v1Tag == null)
            //{
            //    cbTags.Items.Add(Resources.NewV1TagPrompt);
            //}

            //if (v2Tags.Count == 0 && v22Tag == null)
            //{
            //    cbTags.Items.Add(Resources.NewV2TagPrompt);
            //}

            btnDelete.Enabled = currentTag != null;

            cbTags.Enabled = true;
            if (cbTags.Items.Count > 0)
            {
                cbTags.SelectedIndex = 0;
            }
        }

        private void AddV22TagToList(V22Tag v22Tag)
        {
            //tagList.Add(v22Tag);
            cbTags.Items.Add(v22Tag); //.ToString());
        }

        private void AddV2TagsToList(List<V2Tag> v2Tags)
        {
            for (int count = 0; count < v2Tags.Count; count++)
            {
                V2Tag tag = v2Tags[count];

                //tagList.Add(tag);
                cbTags.Items.Add(tag); //.ToString());
            }
        }

        private void AddV1TagToList(V1Tag tagToAdd)
        {
            //tagList.Add(tagToAdd);
            cbTags.Items.Add(tagToAdd); //.ToString());
        }

        #endregion Private Methods

        private void cbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTag != null)
            {
                currentTag = null;
            }

            DisplaySelection();
        }

        private void DisplaySelection()
        {
            if (cbTags.SelectedItem is string)
            {
                iD3V2Editor.Visible = false;
                btnDelete.Text = "Create";
                btnDelete.Enabled = true;
            }
            else
            {
                if (cbTags.SelectedItem is V1Tag || cbTags.SelectedItem is V23Tag || cbTags.SelectedItem is V22Tag)
                {
                    iD3V2Editor.Id3Tag = cbTags.SelectedItem;
                    currentTag = cbTags.SelectedItem;
                    iD3V2Editor.Visible = true;
                }

                btnDelete.Text = "Delete Tag";
            }
        }


        private void txtFileName_DragEnter(object sender, DragEventArgs e)
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


        private void txtFileName_DragDrop(object sender, DragEventArgs e)
        {
            btnCancel.Enabled = btnApply.Enabled = btnDelete.Enabled = false;
            Mp3FileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = cbTags.SelectedIndex;
            string newCommand;
            if ((newCommand = cbTags.Items[index] as string) != null)
            {
                //CreateNewTag(index, newCommand);
            }
            else
            {
                DeleteTag(cbTags.Items[index]);
                btnCancel.Enabled = this.btnApply.Enabled = true;
            }

            DisplayID3Tags();
        }

        private void DeleteTag(object tag)
        {
            bool tagDeleted = false;
            if (tag is V1Tag)
            {
                v1Tag = null;
                tagDeleted = true;
            }
            else if (tag is V22Tag)
            {
                v22Tag = null;
                iD3V2Editor.ClearTag();
                tagDeleted = true;
            }
            else if (tag is V23Tag)
            {
                v2Tags.Clear();
                iD3V2Editor.ClearTag();
                tagDeleted = true;
            }

            btnCancel.Enabled = btnApply.Enabled = tagDeleted ? true : btnApply.Enabled;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {

            //SaveTags();

            this.Close();
        }

        private void SaveTags()
        {
            this.Enabled = false;
            Mp3File file = new Mp3File(this.lblTrackName.Text);
            if (file != null)
            {
                file.SaveTags(v2Tags, v1Tag);
            }

            this.Enabled = true;
            this.btnCancel.Enabled = this.btnApply.Enabled = false;
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveTags();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Mp3FileName = mp3FileName;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string lastFolder = Properties.Settings.Default.LastFolder;
            if (!System.IO.Directory.Exists(lastFolder))
            {
                lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Application.ExecutablePath;
                }
            }

            openFileDialog1.InitialDirectory = lastFolder;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mp3FileName = openFileDialog1.FileName;
                Properties.Settings.Default.LastFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void iD3V2Editor_FrameDeleted(object sender, FrameDeletedEventArgs e)
        {
            this.btnCancel.Enabled = this.btnApply.Enabled = true;
        }

       /*
         * TODO:    add elements for editing popular track tags.
         *          add a list of other track tags, such as PRIV in a grid or something
         *          add a hex editor or some kind of viewer for data of unknown or unhandled tag types such as PRIV, etc.
         *          add a view of embedded images including image types
         *          add help and information about the various track tags
         *          add functionality to change file name based on track name information
         *              including capitalization of the first letter of each word
         *          perhaps add a tree view to select multiple files for editing at once like
         *          done in media player.
         */
    }
}