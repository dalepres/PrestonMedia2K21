using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Wizard.UI;

namespace AlbumArtFixer
{
	public class BlankInternalNoBannerPage: Wizard.UI.InternalWizardNoBannerPage
    {
        private Panel panel1;
		private System.ComponentModel.IContainer components = null;

        public BlankInternalNoBannerPage()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(539, 240);
            this.panel1.TabIndex = 22;
            // 
            // SourcePage
            // 
            this.Controls.Add(this.panel1);
            this.Name = "SourcePage";
            this.Size = new System.Drawing.Size(539, 240);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.SourcePage_SetActive);
            this.ResumeLayout(false);

		}
		#endregion

		private void SourcePage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetWizardButtons(WizardButtons.Back | WizardButtons.Next);
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

