using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommonControls;
using ID3Lib;
using System.DirectoryServices;
using ID3AlbumArtFixer.Properties;

namespace ID3AlbumArtFixer
{
    public partial class AlbumArtFixerForm : PrestonMediaForm
    {
        Settings settings = new Settings();
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private int folderCount = 0;
        private int completedCount = 0;
        
        public AlbumArtFixerForm()
        {
            InitializeComponent();
        }

        private void btnCloseCreate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (!ValidateFolderSelection(folderSelector1, true))
            {
                return;
            }

            toolStripStatusLabel1.Text = "Starting job.";
            SaveSettings();
            btnCancel.Enabled = true;
            btnExecute.Enabled = btnCloseCreate.Enabled = false;
            folderCount = GetJobFolderCount();
            completedCount = 0;

            AlbumArtFixerJob job = new AlbumArtFixerJob(
                folderSelector1.SelectedFolder, 
                folderSelector1.IncludeSubfolders,
                new Size((int)nudMaxWidth.Value, (int)nudMaxHeight.Value),
                chkRestrictAlbumArtAccess.Checked ? ((UserAccount)cbFullControl.SelectedItem).AccountName : string.Empty,
                chkRestrictAlbumArtAccess.Checked ? ((UserAccount)cbReadOnly.SelectedItem).AccountName : string.Empty,
                folderCount);

            lblProgress.Text = string.Empty;
            progressBar1.Value = 0;
            textBox1.Text = string.Empty;
            pbProgress.Visible = false;

            backgroundWorker1.RunWorkerAsync(job);
        }

        private int GetJobFolderCount()
        {
            return Directory.GetDirectories(
                folderSelector1.SelectedFolder, 
                "*.*", 
                folderSelector1.IncludeSubfolders 
                    ? SearchOption.AllDirectories 
                    : SearchOption.TopDirectoryOnly
                ).Length + 1;
        }

        private void SaveSettings()
        {
            settings["AlbumArtImageQualityPerCent"] = Convert.ToInt32(cbQuality.SelectedItem);
            settings.Save();
            folderSelector1.SaveSettings();
        }

        private void AlbumArtFixerForm_Load(object sender, EventArgs e)
        {
            cbQuality.SelectedItem = settings.AlbumArtImageQualityPerCent > 0 ? settings.AlbumArtImageQualityPerCent.ToString() : "70";
            if (cbQuality.SelectedIndex == -1)
            {
                cbQuality.SelectedItem = "70";
            }

            directoryEntry.Path = @"WinNT://" + System.Net.Dns.GetHostName();
            chkRestrictAlbumArtAccess.Checked = Settings.Default.SetAlbumArtSecurity;
            BindUserAccountComboBoxes();

            InitializeBackgoundWorker();
        }

        private void BindUserAccountComboBoxes()
        {
            string readOnlyAccount = Settings.Default.ReadOnlyAccount.ToUpper();
            string fullControlAccount = Settings.Default.FullControlAccount.ToUpper();

 
            foreach (DirectoryEntry child in directoryEntry.Children)
            {
                switch (child.SchemaClassName)
                {
                    case "User":
                        cbFullControl.Items.Add(new User(child.Name));
                        cbReadOnly.Items.Add(new User(child.Name));
                        break;
                    case "Group":
                        cbFullControl.Items.Add(new Group(child.Name));
                        cbReadOnly.Items.Add(new Group(child.Name));
                        break;
                }

                if (child.Name.ToUpper() == fullControlAccount)
                {
                    cbFullControl.SelectedItem = cbFullControl.Items[cbFullControl.Items.Count - 1];
                }

                if (child.Name.ToUpper() == readOnlyAccount)
                {
                    cbReadOnly.SelectedItem = cbReadOnly.Items[cbReadOnly.Items.Count - 1];
                }
            }

            SetComboBoxValue(cbFullControl, Settings.Default.FullControlAccount);
            SetComboBoxValue(cbReadOnly, Settings.Default.ReadOnlyAccount);
        }

        private void SetComboBoxValue(ComboBox comboBox, string newValue)
        {

        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            AlbumArtFixerJob job = e.Argument as AlbumArtFixerJob;


            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.

            //e.Result = 
            AlbumArtFixer.UpdateAlbumArt(job.FolderName, job.IncludeSubfolders, job.MaxSize, worker, e);
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
//                resultLabel.Text = "Canceled";
                MessageBox.Show("Operation cancelled.");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
//                resultLabel.Text = e.Result.ToString();
                toolStripStatusLabel1.Text = "Album art job completed.";
                //MessageBox.Show("Operation completed.", "Album Art Fixer");
            }

            btnCloseCreate.Enabled = btnExecute.Enabled = true;

            // Disable the Cancel button.
            btnCancel.Enabled = false;
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            SetExecuteStatus(e);
        }

        private void SetExecuteStatus(ProgressChangedEventArgs e)
        {
            string completedFolder;
            byte[] imageBytes;

            if (e.UserState is byte[])
            {
                imageBytes = e.UserState as byte[];
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    pbProgress.Visible = true;
                    pbProgress.Image = GetProgressImageFromBytes(e.UserState as byte[]);
                }
            }

            //if (imageBytes != null && imageBytes.Length > 0)
            //{
            //}
            //else
            //{
            //    pbProgress.Image = pbProgress.ErrorImage;
            //    pbProgress.Visible = false;
            //}

            if (e.UserState is string)
            {
                completedFolder = e.UserState as string;
                completedCount++;
                lblProgress.Text = completedCount.ToString() + " of " + folderCount.ToString();
                progressBar1.Value = (int)(((double)completedCount / (double)folderCount) * 100d);
                this.toolStripStatusLabel1.Text = completedFolder;
                textBox1.Text += completedFolder + System.Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private Image GetProgressImageFromBytes(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes);
            return Image.FromStream(ms);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            btnCancel.Enabled = false;
        }

        private void chkRestrictAlbumArtAccess_CheckedChanged(object sender, EventArgs e)
        {
            cbFullControl.Enabled = cbReadOnly.Enabled = chkRestrictAlbumArtAccess.Checked;
        }

        private void DrawAccountComboBoxItem(object sender, DrawItemEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            e.DrawBackground();

            UserAccount user = box.SelectedItem as UserAccount;
            Image itemImage;
            if (user == null)
            {
                return;
            }
            else if (user is Group)
            {
                itemImage = Resources.user_group_32x32;
            }
            else if (user is User)
            {
                itemImage = Resources.user_32x32;
            }
            else
            {
                return;
            }

            e.Graphics.DrawImage(itemImage, e.Bounds.Left, e.Bounds.Top);
        }

        private void AccountComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 32;
        }

        private void aboutAlbumArtFixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }
    }
}
