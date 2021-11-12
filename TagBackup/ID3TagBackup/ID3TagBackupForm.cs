using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Preston.Media
{
    public partial class ID3TagBackupForm : Form
    {
        private bool isExecuting;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int filesWorked = 0;
        private TagBackupJob currentJob = null;


        public ID3TagBackupForm()
        {
            InitializeComponent();
            InitializeBackgoundWorker();
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
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

        private void InitializeWork()
        {
            filesWorked = 0;
            listBox1.Items.Clear();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK || (!Directory.Exists(folderBrowserDialog1.SelectedPath)))
            {
                return;
            }

            this.lblSourceFolder.Text = folderBrowserDialog1.SelectedPath;
        }


        private void btnBackup_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!Directory.Exists(lblSourceFolder.Text))
            {
                MessageBox.Show("Directory "
        + lblSourceFolder.Text
        + " could not be located.  Click Browse to select a valid directory to backup.",
        "Preston Media Tag Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int fileCount = (Directory.GetFiles(lblSourceFolder.Text, "*.mp3", SearchOption.AllDirectories)).Length;

            currentJob = new TagBackupJob(lblSourceFolder.Text, chkRecursive.Checked, chkRemoveTag.Checked, fileCount, JobType.Backup);
            startAsync(currentJob);
            return;

        }

		void Mp3File_TrackTagBackedUp(object sender, TagBackedUpEventArgs e)
		{
			listBox1.Items.Add(e.BackupPath);
		}

		private void ID3TagBackupForm_Load(object sender, EventArgs e)
		{
			Mp3File.TrackTagBackedUp += new TrackTagBackedUpEventHandler(Mp3File_TrackTagBackedUp);
		}

		private void ID3TagBackupForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Mp3File.TrackTagBackedUp -= Mp3File_TrackTagBackedUp;
        }

        #region BackgroundWorker Thread

        private void startAsync(TagBackupJob job)
        {
            // Reset the text in the result label.
            resultLabel.Text = String.Empty;

            // Disable the Browse Source button until 
            // the asynchronous operation is done.
            this.btnBrowseSource.Enabled = false;

            // Disable the Start button until 
            // the asynchronous operation is done.
            this.btnBackup.Enabled = false;

            // Enable the Cancel button while 
            // the asynchronous operation runs.
            this.btnCancel.Enabled = true;

            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync(job);
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            filesWorked++;
            this.progressBar1.Value = e.ProgressPercentage;
            this.listBox1.Items.Add((string)e.UserState);
            this.resultLabel.Text = filesWorked.ToString()
                + " ID3 tags " + ((currentJob.JobType == JobType.Backup) ? "backed up." : "restored.");
        }

        private void btnCancel_Click(System.Object sender,
            System.EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            btnCancel.Enabled = false;
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.

            TagBackupJob job = null;
            BackgroundWorker worker = null;

            try
            {
                job = (TagBackupJob)e.Argument;
                worker = sender as BackgroundWorker;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int filesWorked = 0;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            BackupID3Tags(job, worker, e, ref filesWorked);
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
                resultLabel.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //resultLabel.Text = e.Result.ToString();
            }

            // Enable the Browse Source button.
            this.btnBrowseSource.Enabled = true;

            // Enable the Start button.
            btnBackup.Enabled = true;

            // Disable the Cancel button.
            btnCancel.Enabled = false;
        }

        // This is the method that does the actual work. For this
        // example, it computes a Fibonacci number and
        // reports progress as it does its work.
        //private void BackupID3Tags(string folderName, bool recursive, bool removeTags, BackgroundWorker worker, DoWorkEventArgs e)
        //{
        //    BackupID3Tags(folderName, recursive, removeTags, worker, e, fileCount, filesWorked);
        //}

        private void BackupID3Tags(TagBackupJob job, BackgroundWorker worker, DoWorkEventArgs e, ref int filesWorked)
        {
            if (!Directory.Exists(job.StartingFolder))
            {
                return;
            }

            // Abort the operation if the user has canceled.
            // Note that a call to CancelAsync may have set 
            // CancellationPending to true just after the
            // last invocation of this method exits, so this 
            // code will not have the opportunity to set the 
            // DoWorkEventArgs.Cancel flag to true. This means
            // that RunWorkerCompletedEventArgs.Cancelled will
            // not be set to true in your RunWorkerCompleted
            // event handler. This is a race condition.
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                string[] files = Directory.GetFiles(job.StartingFolder,"*.mp3");
                if (files.Length > 0)
                {
                    for (int count = 0; count < files.Length; count++)
                    {
                        if (job.JobType == JobType.Backup)
                        {
                            Mp3File.BackupID3Tags(files[count]);
                            if (job.RemoveTags)
                            {
                                Mp3File.RemoveTrackTags(files[count]);
                            }
                        }
                        else
                        {
                            Mp3File.RestoreTagsFromBackup(files[count]);
                        }

                        filesWorked++;
                        int percentComplete = CalculatePercentComplete(job.FileCount, filesWorked);
                        worker.ReportProgress(percentComplete, files[count]);
                    }
                }

                if (job.Recursive)
                {
                    string[] folders = Directory.GetDirectories(job.StartingFolder);
                    if (folders.Length > 0)
                    {
                        for (int count = 0; count < folders.Length; count++)
                        {
                            TagBackupJob nextJob = new TagBackupJob(folders[count], job.Recursive, job.RemoveTags, job.FileCount, job.JobType);
                            BackupID3Tags(nextJob, worker, e, ref filesWorked);
                        }
                    }
                }
            }

            e.Result = filesWorked;
        }

        private int CalculatePercentComplete(int fileCount, int filesWorked)
        {
            double dFileCount = (double)fileCount;
            double dFilesWorked = (double)filesWorked;
            double dPercent = (dFilesWorked / dFileCount) * 100d;

            return (int)dPercent;
        }

        #endregion BackgroundWorker Thread

        private class TagBackupJob
        {
            private string startingFolder;
            private bool recursive;
            private bool removeTags;
            private int fileCount;
            private JobType jobType;

            private TagBackupJob()
            {
            }

            public TagBackupJob(string startingFolder, bool recursive, bool removeTags, int fileCount, JobType jobType)
            {
                this.startingFolder = startingFolder;
                this.recursive = recursive;
                this.removeTags = removeTags;
                this.fileCount = fileCount;
                this.jobType = jobType;
            }

            public string StartingFolder
            {
                get { return startingFolder; }
                set { startingFolder = value; }
            }


            public bool Recursive
            {
                get { return recursive; }
                set { recursive = value; }
            }


            public bool RemoveTags
            {
                get { return removeTags; }
                set { removeTags = value; }
            }

            public int FileCount
            {
                get { return fileCount; }
                set { fileCount = value; }
            }

            public JobType JobType
            {
                get { return jobType; }
                set { jobType = value; }
            }
        }

        private enum JobType
        {
            Backup,
            Restore
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!Directory.Exists(lblSourceFolder.Text))
            {
                MessageBox.Show("Directory "
        + lblSourceFolder.Text
        + " could not be located.  Click Browse to select a valid directory to restore.",
        "Preston Media Tag Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int fileCount = (Directory.GetFiles(lblSourceFolder.Text, "*.mp3", SearchOption.AllDirectories)).Length;

            currentJob = new TagBackupJob(lblSourceFolder.Text, chkRecursive.Checked, chkRemoveTag.Checked, fileCount, JobType.Restore);
            startAsync(currentJob);
            return;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }
    }
}