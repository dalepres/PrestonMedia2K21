using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;

namespace ID3AlbumArtFixer.Wizard
{
    internal class SourcePage : InternalWizardPage, IAlbumArtFixerJob
    {
        private Panel pnlMain;
        private Panel pnlSourcePage;
        private CommonControls.FolderSelector folderSelector1;
		private System.ComponentModel.IContainer components = null;

        internal SourcePage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSourcePage = new System.Windows.Forms.Panel();
            this.folderSelector1 = new CommonControls.FolderSelector();
            this.pnlMain.SuspendLayout();
            this.pnlSourcePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(325, 64);
            this.Banner.Subtitle = "Select a folder to work with.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlSourcePage);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(325, 99);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlSourcePage
            // 
            this.pnlSourcePage.Controls.Add(this.folderSelector1);
            this.pnlSourcePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSourcePage.Location = new System.Drawing.Point(5, 5);
            this.pnlSourcePage.Name = "pnlSourcePage";
            this.pnlSourcePage.Size = new System.Drawing.Size(315, 86);
            this.pnlSourcePage.TabIndex = 0;
            // 
            // folderSelector1
            // 
            this.folderSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.folderSelector1.Location = new System.Drawing.Point(0, 0);
            this.folderSelector1.MaximumSize = new System.Drawing.Size(0, 80);
            this.folderSelector1.MinimumSize = new System.Drawing.Size(150, 80);
            this.folderSelector1.Name = "folderSelector1";
            this.folderSelector1.SaveFolderHistoryOnSelection = true;
            this.folderSelector1.SelectedFolder = "";
            this.folderSelector1.Size = new System.Drawing.Size(315, 80);
            this.folderSelector1.TabIndex = 4;
            // 
            // SourcePage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "SourcePage";
            this.Size = new System.Drawing.Size(325, 163);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlSourcePage.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next | WizardButtons.Help | WizardButtons.About);
		}

        public void SaveSelectedFolderSettings()
        {
            folderSelector1.SaveSettings();
        }

        #region IAlbumArtFixerJob Members

        public AlbumArtFixerJob UpdateJobFromForm(AlbumArtFixerJob job)
        {
            job.IncludeSubfolders = folderSelector1.IncludeSubfolders;
            job.FolderName = folderSelector1.SelectedFolder;

            return job;
        }

        #endregion
    }
}

