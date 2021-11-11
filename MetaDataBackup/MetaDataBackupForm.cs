using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace Preston.Media
{
    public partial class MetaDataBackupForm : Form
    {
        private string backupFileName;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int filesWorked = 0;
        private TagBackupJob currentJob = null;
        private MediaAttributeCollection attributeList;

        public MetaDataBackupForm()
        {
            InitializeComponent();
            InitializeBackgoundWorker();
            attributeList = Properties.Settings.Default.LastAttributeList;
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

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
            Properties.Settings.Default.BackupFilePath = openFileDialog1.FileName;
            Properties.Settings.Default.IncludeSubfolders = chkRecursive.Checked;
            Properties.Settings.Default.LibrarySourcePath = folderBrowserDialog1.SelectedPath;
            Properties.Settings.Default.Save();
            filesWorked = 0;
            listBox1.Items.Clear();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            string lastFolder = Properties.Settings.Default.LibrarySourcePath;

            if (!System.IO.Directory.Exists(lastFolder))
            {
                lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Application.ExecutablePath;
                }
            }

            folderBrowserDialog1.SelectedPath = lastFolder;

            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK || (!Directory.Exists(folderBrowserDialog1.SelectedPath)))
            {
                return;
            }

            this.lblSourceFolder.Text = folderBrowserDialog1.SelectedPath;
            ValidateBackupJob();
        }


        private bool ValidateSourceDirectory(string command)
        {
            if (!Directory.Exists(Path.GetDirectoryName(lblSourceFolder.Text)))
            {
                MessageBox.Show("Directory "
        + lblSourceFolder.Text
        + " could not be located.  Click Browse to select a valid directory to " + command + ".",
        "Preston Media Metadata Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!ValidateSourceDirectory("backup")
                || !ValidateBackupFile("backup")
                || !ValidateBackupPath())
            {
                ValidateBackupJob();
                return;
            }

            int fileCount = (Directory.GetFiles(lblSourceFolder.Text, "*.*", this.chkRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)).Length;

            currentJob = new TagBackupJob(lblSourceFolder.Text, chkRecursive.Checked, false, fileCount, JobTypes.Backup, backupFileName);
            currentJob.BackupDatabaseAttributes = true;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "     ";
            settings.CloseOutput = true;

            currentJob.XmlWriter = XmlWriter.Create(backupFileName, settings);
            currentJob.XmlWriter.WriteStartDocument();
            currentJob.XmlWriter.WriteStartElement("MediaLibraryMetadataBackup");
            currentJob.XmlWriter.WriteAttributeString("SourcePathRoot", currentJob.StartingFolder);
            
            currentJob.MediaPlayer = new MediaPlayer();


            startAsync(currentJob);
        }

        private bool ValidateBackupPath()
        {
            if (!Directory.Exists(Path.GetDirectoryName(lblDatabaseFileLocation.Text)))
            {
                MessageBox.Show("Directory "
        + lblDatabaseFileLocation.Text
        + " could not be located.  Click Browse to select a valid directory to locate the backup file.",
        "Preston Media Metadata Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateBackupFile(string command)
        {
            if (command == "restore" && !File.Exists(lblDatabaseFileLocation.Text))
            {
                MessageBox.Show("The selected backup file, " + lblDatabaseFileLocation.Text
                + " could not be found.  Browse for the backup file.", "Metadata Backup - Backup file not found", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ValidateBackupJob();
                return false;
            }
            else if (command == "backup" && File.Exists(lblDatabaseFileLocation.Text))
            {
                DialogResult fileCheckResult = MessageBox.Show("File "
                    + lblDatabaseFileLocation.Text
                    + " already exists.  Do you want to overwrite it?",
                    "Preston Media Metadata Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (fileCheckResult != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        #region BackgroundWorker Thread

        private void startAsync(TagBackupJob job)
        {
            // Reset the text in the result label.
            resultLabel.Text = String.Empty;

            // Disable the Browse Source button until 
            // the asynchronous operation is done.
            this.btnBrowseSource.Enabled = false;
            this.btnDatabaseBrowse.Enabled = false;
            this.btnClose.Enabled = false;

            // Disable the Start button until 
            // the asynchronous operation is done.
            this.btnBackup.Enabled = false;
            this.btnRestore.Enabled = false;

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
                + " media items " 
                + " out of " + currentJob.FileCount.ToString() + " files "
                + ((currentJob.JobType == JobTypes.Backup) ? "backed up." : "restored.");
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

            if (job.JobType == JobTypes.Backup)
            {
                BackupMetadata(job, worker, e, ref filesWorked);
            }
            else
            {
                RestoreMetadata(job, worker, e, ref filesWorked);
            }
        }

        private void RestoreMetadata(TagBackupJob job, BackgroundWorker worker, DoWorkEventArgs e, ref int filesWorked)
        {
            // Start of what to move to MP3File or MediaPlayer.cs
            WMPLib.WindowsMediaPlayer player = job.MediaPlayer.Player;
            WMPLib.IWMPPlaylist playlist;
            WMPLib.IWMPMedia currentMedia;

            string nullString = "\0";

            for (int xCount = 0; xCount < job.Iterator.Count; xCount++)
            {

                string attributeName;
                string attributeValue;
                string readOnly;
                bool isReadOnly = false;

                job.Iterator.MoveNext();

                job.Iterator.Current.MoveToAttribute("SourceUrl", "");
                string sourceUrl = MergeOriginalAndNewPath(job.StartingFolder, job.OriginalStartingFolder, job.Iterator.Current.Value);

                playlist = job.MediaPlayer.Player.mediaCollection.getByAttribute(
                "SourceURL",
                sourceUrl);

                if (playlist.count == 1)
                {
                    currentMedia = playlist.get_Item(0);
                }
                else
                {
                    continue;
                }

                job.Iterator.Current.MoveToParent();
                bool isChild = job.Iterator.Current.MoveToFirstChild();
                while (isChild)
                {
                    attributeName = job.Iterator.Current.Name;
                    if (attributeName.StartsWith("WM_"))
                    {
                        attributeName = "WM/" + attributeName.Substring(3);
                    }

                    attributeValue = job.Iterator.Current.Value
                        .Replace("<NULL>", nullString)
                        .Replace("<null>", nullString)
                        .Replace("<Null>", nullString);

                    job.Iterator.Current.MoveToAttribute("IsReadOnly", "");
                    readOnly = job.Iterator.Current.Value;

                    if (bool.TryParse(readOnly, out isReadOnly) && !isReadOnly)
                    {
                        currentMedia.setItemInfo(attributeName, attributeValue);
                    }

                    job.Iterator.Current.MoveToParent();
                    isChild = job.Iterator.Current.MoveToNext(XPathNodeType.Element);
                }

                filesWorked++;
                int percentComplete = CalculatePercentComplete(job.FileCount, filesWorked);
                worker.ReportProgress(percentComplete, currentMedia.sourceURL);
            }
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

            //SaveBackupFile();

            if (currentJob.JobType == JobTypes.Backup)
            {
                currentJob.XmlWriter.WriteEndElement();
                currentJob.XmlWriter.WriteEndDocument();
                currentJob.XmlWriter.Flush();
                currentJob.XmlWriter.Close();
            }

            // Enable the Browse Source button.
            btnDatabaseBrowse.Enabled = btnBrowseSource.Enabled = true;

            // Enable the Start button.
            btnRestore.Enabled = btnBackup.Enabled = btnClose.Enabled = true;

            // Disable the Cancel button.
            btnCancel.Enabled = false;
        }

        private void BackupMetadata(TagBackupJob job, BackgroundWorker worker, DoWorkEventArgs e, ref int filesWorked)
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
                string[] files = Directory.GetFiles(job.StartingFolder);
                if (files.Length > 0)
                {
                    for (int count = 0; count < files.Length; count++)
                    {
                        if (job.JobType == JobTypes.Backup)
                        {
                            if (job.BackupDatabaseAttributes)
                            {
                                Mp3File.BackupLibraryData(job.MediaPlayer, job.Playlist, files[count], job.XmlWriter); // job.DatabaseXmlDocument);
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
                            TagBackupJob nextJob = new TagBackupJob(folders[count], job.Recursive, job.RemoveTags, job.FileCount, job.JobType, backupFileName);
                            //nextJob.DatabaseXmlDocument = job.DatabaseXmlDocument;

                            nextJob.XmlWriter = job.XmlWriter;
                            nextJob.BackupDatabaseAttributes = true;
                            nextJob.MediaPlayer = job.MediaPlayer;
                            nextJob.Playlist = job.Playlist;
                            BackupMetadata(nextJob, worker, e, ref filesWorked);
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


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!(ValidateSourceDirectory("restore")
                || ValidateBackupFile("restore")))
            {
                return;
            }

            //int fileCount = (Directory.GetFiles(lblSourceFolder.Text, "*.*", this.chkRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)).Length;

            currentJob = new TagBackupJob(lblSourceFolder.Text, chkRecursive.Checked, false, 0, JobTypes.Restore, backupFileName);
            currentJob.BackupDatabaseAttributes = true;
            currentJob.MediaPlayer = new MediaPlayer();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(backupFileName);
            XmlElement el = xDoc.DocumentElement;
            currentJob.OriginalStartingFolder = el.GetAttribute("SourcePathRoot");
            XPathNavigator xNav = xDoc.CreateNavigator();

            currentJob.Iterator = xNav.Select("//MediaItem");  //    /@SourceUrl=\"D:\\My Music\\Aerosmith\\Permanent Vacation\\02 Magic Touch.mp3\"");
            currentJob.FileCount = currentJob.Iterator.Count;

            //currentJob.XmlReader = XmlReader.Create(backupFileName, settings);

            //currentJob.XmlReader.ReadStartElement("MediaLibraryMetadataBackup");

            //string xml = currentJob.XmlReader.ReadOuterXml();
            //string xml2 = currentJob.XmlReader.ReadOuterXml();
            //currentJob.XmlReader.ReadToNextSibling("MediaItem");

            //currentJob.XmlReader.MoveToAttribute("SourceUrl");
            //currentJob.XmlReader.ReadStartElement("MediaItem");

            //string text = currentJob.XmlReader.Value;
            //string name = currentJob.XmlReader.Name;

            //currentJob.XmlReader.MoveToAttribute("IsReadOnly");
            //string s = currentJob.XmlReader.Value;

            //currentJob.XmlReader.ReadToNextSibling("IsReadOnly");
            //bool isReadOnly = Boolean.Parse(currentJob.XmlReader.Value);

            startAsync(currentJob);
            return;
        }

        private string MergeOriginalAndNewPath(string newSourcePathRoot, string originalSourcePathRoot, string pathToMerge)
        {
            return pathToMerge.Replace(originalSourcePathRoot, newSourcePathRoot);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void btnDatabaseBrowse_Click(object sender, EventArgs e)
        {
            string lastFolder = Properties.Settings.Default.BackupFilePath;

            if (!System.IO.Directory.Exists(lastFolder))
            {
                lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Application.ExecutablePath;
                }
            }

            openFileDialog1.InitialDirectory = lastFolder;

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                backupFileName = lblDatabaseFileLocation.Text = openFileDialog1.FileName;
                ValidateBackupJob();
            }
        }

        private void ValidateBackupJob()
        {
            bool pathsAreValid = 
                (Directory.Exists(System.IO.Path.GetDirectoryName(lblDatabaseFileLocation.Text))
            && Directory.Exists(lblSourceFolder.Text));

            this.btnBackup.Enabled = this.btnRestore.Enabled = pathsAreValid;
            if (pathsAreValid)
            {
                backupFileName = lblDatabaseFileLocation.Text;
            }
        }

        private void MetaDataBackupForm_Load(object sender, EventArgs e)
        {
            this.lblSourceFolder.Text = folderBrowserDialog1.SelectedPath
                = Properties.Settings.Default.LibrarySourcePath;
            this.lblDatabaseFileLocation.Text = openFileDialog1.FileName
                = Properties.Settings.Default.BackupFilePath;
            this.chkRecursive.Checked = Properties.Settings.Default.IncludeSubfolders;
            this.openFileDialog1.InitialDirectory = Path.GetDirectoryName(lblDatabaseFileLocation.Text);
            if (Path.GetFileName(lblDatabaseFileLocation.Text).Contains("Browse for"))
            {
                this.openFileDialog1.FileName = "WMPMetadata";
            }

            ValidateBackupJob();
        }

        private void howToUseMetadataBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToUse howToUse = new HowToUse();
            howToUse.Show();
        }

        
        private void tsmiChooseAttributesToBackup_Click(object sender, EventArgs e)
        {
            if (attributeList == null)
            {
                attributeList = MediaAttributeCollectionFactory.CreateDefaultCollection();
            }
            //MediaAttributeCollection mac = new MediaAttributeCollection();
            AttributeListSelector ec = new AttributeListSelector(attributeList);
            ec.ShowDialog();
        }
    }
}