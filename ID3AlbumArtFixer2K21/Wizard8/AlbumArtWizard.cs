using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ID3AlbumArtFixer.Properties;
using Wizard.UI;
using System.IO;

namespace ID3AlbumArtFixer.Wizard
{
    public class AlbumArtWizard : WizardSheet
	{
		private System.ComponentModel.IContainer components = null;

        public AlbumArtWizard()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.Pages.Add(new WelcomePage());
            this.Pages.Add(new SourcePage());
            this.Pages.Add(new AlbumArtOptionsPage());
            this.Pages.Add(new SecurityPage());
            this.Pages.Add(new EmbedAlbumArtPage());
            this.Pages.Add(new ConfirmationPage());
            this.Pages.Add(new ResultsPage());
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumArtWizard));
            this.SuspendLayout();
            // 
            // AlbumArtWizard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(515, 145);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlbumArtWizard";
            this.Text = "ID3 Album Art Fixer";
            this.WizardHelp += new WizardSheetEventHandler(this.WizardHelp_Click);
            this.WizardFinish += new WizardSheetEventHandler(this.AlbumArtWizard_WizardFinish);
            this.Load += new System.EventHandler(this.AlbumArtFixerSheet_Load);
            this.WizardAbout += new System.EventHandler(this.WizardAbout_Click);
            this.ResumeLayout(false);

		}

        void AlbumArtWizard_WizardFinish(object sender, WizardSheetEventArgs e)
        {
            if (e.ActivePage is ResultsPage)
            {
                e.Cancel = false;
                return;
            }

            if (!e.Cancel && !(e.ActivePage is ConfirmationPage))
            {
                AlbumArtFixerJob confirmJob = new AlbumArtFixerJob();
                foreach (WizardPage page in this.Pages)
                {
                    if (page is IAlbumArtFixerJob)
                    {
                        confirmJob = ((IAlbumArtFixerJob)page).UpdateJobFromForm(confirmJob);
                    }
                }

                e.Cancel = true;
                SetActivePage("ConfirmationPage");
                ((ConfirmationPage)ActivePage).AlbumArtFixerJob = confirmJob;
            }

            if (!e.Cancel)
            {
                AlbumArtFixerJob executeJob = ((ConfirmationPage)ActivePage).AlbumArtFixerJob;
                SaveSettings();
                SetActivePage("CompletePage");
                ((ResultsPage)ActivePage).ExecuteAlbumArtJob(executeJob);
            }
        }
		#endregion

		private void AlbumArtFixerSheet_Load(object sender, System.EventArgs e)
		{
            if (Settings.Default.SkipWelcomePage)
            {
                SetActivePage(1);
            }
		}

        public void SaveSettings()
        {
            foreach (WizardPage page in this.Pages)
            {
                if (page is IAlbumArtFixerSettings)
                {
                    ((IAlbumArtFixerSettings)page).SetSettingsFromForm();
                }
            }

            Settings.Default.Save();
            ((SourcePage)Pages["SourcePage"]).SaveSelectedFolderSettings();
        }

        void WizardHelp_Click(object sender, WizardSheetEventArgs e)
        {
            if (File.Exists("ID3AlbumArtFixerHelp.chm"))
            {
                Help.ShowHelp(this, "ID3AlbumArtFixerHelp.chm", HelpNavigator.Topic, e.ActivePage.Name + ".html");
            }
            else
            {
                MessageBox.Show("Help file 'ID3AlbumArtFixerHelp.chm' was not found in the application folder.", "ID3AlbumArtFixer Help", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void WizardAbout_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
        }
    }
}

