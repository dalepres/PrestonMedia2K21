using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using ID3AlbumArtFixer.Properties;

namespace ID3AlbumArtFixer.Wizard
{
    internal class WelcomePage : InternalWizardPage
    {
        private Panel pnlMain;
        private Label label1;
        private CheckBox chkDoNotShowWelcome;
		private System.ComponentModel.IContainer components = null;

        internal WelcomePage()
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkDoNotShowWelcome = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(408, 64);
            this.Banner.Subtitle = "Welcome to the album art fixer wizard.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.chkDoNotShowWelcome);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(408, 138);
            this.pnlMain.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 54);
            this.label1.TabIndex = 3;
            this.label1.Text = "This wizard will collect information required to manage album art in your MP3 lib" +
                "rary and to protect that album art from corruption by Windows Media Player.\r\n";
            // 
            // chkDoNotShowWelcome
            // 
            this.chkDoNotShowWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDoNotShowWelcome.AutoSize = true;
            this.chkDoNotShowWelcome.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.SkipWelcomePage;
            this.chkDoNotShowWelcome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoNotShowWelcome.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "SkipWelcomePage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDoNotShowWelcome.Location = new System.Drawing.Point(23, 113);
            this.chkDoNotShowWelcome.Name = "chkDoNotShowWelcome";
            this.chkDoNotShowWelcome.Size = new System.Drawing.Size(250, 17);
            this.chkDoNotShowWelcome.TabIndex = 4;
            this.chkDoNotShowWelcome.Text = "Do not start with this welcome screen next time.";
            this.chkDoNotShowWelcome.UseVisualStyleBackColor = true;
            // 
            // WelcomePage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(408, 202);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetWizardButtons(WizardButtons.Next | WizardButtons.Help | WizardButtons.About);
		}
    }
}

