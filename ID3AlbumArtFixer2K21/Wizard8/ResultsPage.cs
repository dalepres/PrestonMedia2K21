using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using System.IO;
using ID3AlbumArtFixer.Properties;

namespace ID3AlbumArtFixer.Wizard
{
    internal class ResultsPage : InternalWizardPage
    {
        private Panel pnlMain;
        private Panel pnlProgress;
        private GroupBox grpProgress;
        private PictureBox pbProgress;
        private TextBox textBox1;
        private Label lblProgress;
        private ProgressBar progressBar1;
        private BackgroundWorker backgroundWorker1;
        private System.ComponentModel.IContainer components = null;
        private WizardButtons wizardButtons = WizardButtons.Back | WizardButtons.Help | WizardButtons.About | WizardButtons.Finish;

        private int folderCount = 0;
        private int completedCount = 0;

        public ResultsPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            InitializeBackgoundWorker();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pbProgress = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlMain.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(539, 64);
            this.Banner.Subtitle = "Album art fixer job progress.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlProgress);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(539, 196);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.grpProgress);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgress.Location = new System.Drawing.Point(5, 5);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(5);
            this.pnlProgress.Size = new System.Drawing.Size(529, 186);
            this.pnlProgress.TabIndex = 8;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pbProgress);
            this.grpProgress.Controls.Add(this.textBox1);
            this.grpProgress.Controls.Add(this.lblProgress);
            this.grpProgress.Controls.Add(this.progressBar1);
            this.grpProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProgress.Location = new System.Drawing.Point(5, 5);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(519, 176);
            this.grpProgress.TabIndex = 0;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(407, 19);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(100, 100);
            this.pbProgress.TabIndex = 3;
            this.pbProgress.TabStop = false;
            this.pbProgress.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(13, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(388, 100);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(16, 126);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(19, 145);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(488, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // CompletePage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "CompletePage";
            this.Size = new System.Drawing.Size(539, 260);
            this.WizardCancel += new System.ComponentModel.CancelEventHandler(this.CompletePage_WizardCancel);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

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

        private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetWizardButtons(wizardButtons);
        }

        internal void ExecuteAlbumArtJob(AlbumArtFixerJob executeJob)
        {
            folderCount = executeJob.FolderCount = CountFoldersForJob(executeJob);
            completedCount = 0;
            executeJob.IsStarting = true;
            lblProgress.Text = string.Empty;
            progressBar1.Value = 0;
            textBox1.Text = string.Empty;
            pbProgress.Visible = false;
            SetWizardButtons(WizardButtons.None);
            lblProgress.Text = "Starting job...";

            backgroundWorker1.RunWorkerAsync(executeJob);
        }

        private int CountFoldersForJob(AlbumArtFixerJob executeJob)
        {
            if (!executeJob.IncludeSubfolders)
            {
                return 1;
            }

            return Directory.GetDirectories(
                   executeJob.FolderName,
                   "*.*",
                   executeJob.IncludeSubfolders
                       ? SearchOption.AllDirectories
                       : SearchOption.TopDirectoryOnly
                   ).Length + 1;
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            AlbumArtFixerJob job = e.Argument as AlbumArtFixerJob;

            AlbumArtFixer.UpdateAlbumArt(job.FolderName, job.IncludeSubfolders, job.MaxSize, worker, e);
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Operation cancelled.");
            }
            else
            {
               lblProgress.Text += ". Job completed.";
            }

            SetWizardButtons(wizardButtons);
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            string completedFolder;
            byte[] imageBytes;

            if (e.UserState is byte[])
            {
                imageBytes = e.UserState as byte[];
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    pbProgress.Visible = true;
                    using (MemoryStream ms = new MemoryStream(e.UserState as byte[]))
                    {
                        pbProgress.Image = Image.FromStream(ms);
                    }
                }
            }

            if (e.UserState is string)
            {
                completedFolder = e.UserState as string;
                completedCount++;
                lblProgress.Text = completedCount.ToString() + " of " + folderCount.ToString();
                progressBar1.Value = (int)(((double)completedCount / (double)folderCount) * 100d);
                textBox1.Text += completedFolder + System.Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private void CompletePage_WizardCancel(object sender, CancelEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
                e.Cancel = true;
            }
        }
    }
}

