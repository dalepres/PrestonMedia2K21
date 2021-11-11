using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using System.IO;
using ID3Lib;

namespace ID3AlbumArtFixer.Wizard
{
    internal class ConfirmationPage : InternalWizardPage
    {
        private Panel pnlMain;
        private System.ComponentModel.IContainer components = null;
        private RichTextBox rtbAlbumArtJob;
        private AlbumArtFixerJob albumArtFixerJob;
        private WizardButtons wizardButtons
            = WizardButtons.Back
            | WizardButtons.Help
            | WizardButtons.About
            | WizardButtons.Finish;

        private string nl = System.Environment.NewLine;
        private string tab = "\t";
        
        public AlbumArtFixerJob AlbumArtFixerJob
        {
            get { return albumArtFixerJob; }
            set
            {
                albumArtFixerJob = value;
                DisplayJob();
            }
        }

        private void DisplayJob()
        {
            if (!ValidateJob())
            {
                return;
            }

            rtbAlbumArtJob.Text = "Album Art Fixer Job" + nl;
            rtbAlbumArtJob.Text += "Selected folder: " + albumArtFixerJob.FolderName + nl;

            DisplayJobOptions();
            DisplaySecurity();
            DisplayEmbedOptions();
        }

        private void DisplayEmbedOptions()
        {
            rtbAlbumArtJob.Text += nl;

            if (!albumArtFixerJob.EmbedAlbumArt)
            {
                rtbAlbumArtJob.Text += "Album art not embedded into MP3 files." + nl;
            }
            else
            {
                EmbedPictureJob picJob = albumArtFixerJob.EmbedPictureJob;
                DeletePictures deleteJob = picJob.DeleteExistingImages;

                rtbAlbumArtJob.Text += "Embed album art into MP3 file options:" + nl;
                rtbAlbumArtJob.Text += tab + "Filename of image to embed:" + "\"" + picJob.ImageFileName + "\"" + nl;
                rtbAlbumArtJob.Text += tab + "Image description: " + picJob.ImageDescription + nl;
                rtbAlbumArtJob.Text += tab + "Image type: " 
                    + ID3Lib.ID3PictureTypes.GetPictureType(picJob.Id3ImageType).PictureType + nl;

                if (deleteJob == DeletePictures.None)
                {
                    rtbAlbumArtJob.Text += tab + "Do not delete existing embedded images.  Picture Type 1 or Picture Type 2\n\t\tor matching picture type and description will be replacedwith new picture." + nl;
                }
                else if(deleteJob == DeletePictures.AllPictures)
                {
                    rtbAlbumArtJob.Text += tab + "Delete all existing embedded images." + nl;
                }
                else if (deleteJob == DeletePictures.SameTypePictures)
                {
                    rtbAlbumArtJob.Text += tab + "Delete existing embedded images of the same type." + nl;
                }

                if (picJob.EnforceMaxSize)
                {
                    rtbAlbumArtJob.Text += tab + "Embedded album art size restricted to max: " 
                        + picJob.MaxSize.Value.Width.ToString() + "W x " 
                        + picJob.MaxSize.Value.Height.ToString() + "H" + nl;
                }
            }
        }

        private void DisplaySecurity()
        {
            rtbAlbumArtJob.Text += nl;

            if (!albumArtFixerJob.SetAlbumArtSecurity)
            {
                rtbAlbumArtJob.Text += "Album art file security not set." + nl;
            }
            else
            {
                rtbAlbumArtJob.Text += "Set security to restrict album art to:" + nl;
                rtbAlbumArtJob.Text += tab + "Read only access: " + albumArtFixerJob.ReadOnlyAccount + nl;
                rtbAlbumArtJob.Text += tab + "Full control access: " + albumArtFixerJob.FullControlAccount + nl;
            }
        }

        private void DisplayJobOptions()
        {
            rtbAlbumArtJob.Text += nl;

            if (!albumArtFixerJob.CreateAlbumArt)
            {
                rtbAlbumArtJob.Text += "Album art not created or resized." + nl;
            }
            else
            {
                rtbAlbumArtJob.Text += "Album art options" + nl;
                rtbAlbumArtJob.Text += tab + "Album art size: " 
                    + albumArtFixerJob.MaxSize.Width.ToString() 
                    + "W x " + albumArtFixerJob.MaxSize.Height.ToString() 
                    + "H" + nl;

                rtbAlbumArtJob.Text += tab + "Image quality: " 
                    + albumArtFixerJob.ImageQuality.ToString() 
                    + "%" + nl;

                if (albumArtFixerJob.AlbumArtSource == AlbumArtSource.LargestImage)
                {
                    rtbAlbumArtJob.Text += tab + "Album art created from largest image in folder." + nl;
                }
                else
                {
                    rtbAlbumArtJob.Text += tab + "Album art created from file \"" + albumArtFixerJob.AlbumArtSourceFileName + "\"" + nl;
                }
            }
        }

        private bool ValidateJob()
        {
            if (!Directory.Exists(albumArtFixerJob.FolderName))
            {
                string errorMessage = "Folder "
                    + albumArtFixerJob.FolderName
                    + " could not be found.  Click Back to the Source page to select a new folder to work with.";
                rtbAlbumArtJob.ForeColor = Color.Red;
                rtbAlbumArtJob.Text = errorMessage;

                SetWizardButtons(wizardButtons & ~WizardButtons.Finish);
                return false;
            }
            else
            {
                rtbAlbumArtJob.ForeColor = System.Drawing.SystemColors.WindowText;
                SetWizardButtons(wizardButtons | WizardButtons.Finish);
                return true;
            }
        }

        public ConfirmationPage()
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
            this.rtbAlbumArtJob = new System.Windows.Forms.RichTextBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(304, 64);
            this.Banner.Subtitle = "Confirm your choices shown below and then click Finish to execute your album art " +
                "job.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.rtbAlbumArtJob);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(304, 176);
            this.pnlMain.TabIndex = 1;
            // 
            // rtbAlbumArtJob
            // 
            this.rtbAlbumArtJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAlbumArtJob.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtbAlbumArtJob.Location = new System.Drawing.Point(5, 5);
            this.rtbAlbumArtJob.Margin = new System.Windows.Forms.Padding(10);
            this.rtbAlbumArtJob.Name = "rtbAlbumArtJob";
            this.rtbAlbumArtJob.ReadOnly = true;
            this.rtbAlbumArtJob.ShortcutsEnabled = false;
            this.rtbAlbumArtJob.Size = new System.Drawing.Size(294, 166);
            this.rtbAlbumArtJob.TabIndex = 0;
            this.rtbAlbumArtJob.Text = "";
            this.rtbAlbumArtJob.WordWrap = false;
            // 
            // ConfirmationPage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "ConfirmationPage";
            this.Size = new System.Drawing.Size(304, 240);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
            SetWizardButtons(wizardButtons);
		}
    }
}

