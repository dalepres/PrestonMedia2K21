using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Wizard.UI;

namespace AlbumArtFixer
{
	public class BlankInternalPage: Wizard.UI.InternalWizardPage
    {
        private Panel pnlMain;
		private System.ComponentModel.IContainer components = null;

        public BlankInternalPage()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(539, 64);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(539, 176);
            this.pnlMain.TabIndex = 1;
            // 
            // BlankInternalNoBannerPage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "BlankInternalNoBannerPage";
            this.Size = new System.Drawing.Size(539, 240);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.SourcePage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.ResumeLayout(false);

		}
		#endregion

		private void SourcePage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next | WizardButtons.Help | WizardButtons.About);
		}

        void AlbumArtOptionsPage_WizardHelp(object sender, EventArgs e)
        {
            HelpViewer.ShowPageHelp(this);
        }

        void AlbumArtOptionsPage_WizardAbout(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
        }
	}
}

