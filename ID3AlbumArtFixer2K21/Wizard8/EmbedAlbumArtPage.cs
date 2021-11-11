using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using ID3Lib;
using ID3AlbumArtFixer.Properties;

namespace ID3AlbumArtFixer.Wizard
{
    internal class EmbedAlbumArtPage : InternalWizardPage, IAlbumArtFixerJob, IAlbumArtFixerSettings
    {
        private Panel pnlMain;
        private Panel pnlEmbedAlbumArtOptions;
        private GroupBox gbEmbedAlbumArt;
        private Label label5;
        private NumericUpDown nudHeight;
        private Label label4;
        private NumericUpDown nudWidth;
        private CheckBox chkEnforceMaximumSize;
        private RadioButton rbSameTypeImages;
        private RadioButton rbAllImages;
        private CheckBox chkDeleteExistingImages;
        private TextBox txtImageName;
        private Label label3;
        private Label label2;
        private ComboBox cbPictureTypes;
        private TextBox txtDescription;
        private Label label1;
        private CheckBox chkEmbedAlbumArt;
		private System.ComponentModel.IContainer components = null;

        private int maxWidthPreIconSelection = 0;
        private int maxHeightPreIconSelection = 0;
        private bool enforceMaxPreIconSelection = false;
        private bool iconIsSelected = false;

        internal EmbedAlbumArtPage()
		{
			InitializeComponent();

            LoadPictureTypes();
		}

        private void LoadPictureTypes()
        {
            cbPictureTypes.DataSource = ID3PictureTypes.PictureTypes;
            cbPictureTypes.DisplayMember = "PictureType";
            cbPictureTypes.ValueMember = "PictureTypeId";
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
            this.pnlEmbedAlbumArtOptions = new System.Windows.Forms.Panel();
            this.gbEmbedAlbumArt = new System.Windows.Forms.GroupBox();
            this.chkEmbedAlbumArt = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.chkEnforceMaximumSize = new System.Windows.Forms.CheckBox();
            this.rbSameTypeImages = new System.Windows.Forms.RadioButton();
            this.rbAllImages = new System.Windows.Forms.RadioButton();
            this.chkDeleteExistingImages = new System.Windows.Forms.CheckBox();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPictureTypes = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlEmbedAlbumArtOptions.SuspendLayout();
            this.gbEmbedAlbumArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(409, 64);
            this.Banner.Subtitle = "Choose options for embedding album art into your MP3 files.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlEmbedAlbumArtOptions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(409, 202);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlEmbedAlbumArtOptions
            // 
            this.pnlEmbedAlbumArtOptions.Controls.Add(this.gbEmbedAlbumArt);
            this.pnlEmbedAlbumArtOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEmbedAlbumArtOptions.Location = new System.Drawing.Point(5, 5);
            this.pnlEmbedAlbumArtOptions.Name = "pnlEmbedAlbumArtOptions";
            this.pnlEmbedAlbumArtOptions.Padding = new System.Windows.Forms.Padding(5);
            this.pnlEmbedAlbumArtOptions.Size = new System.Drawing.Size(399, 189);
            this.pnlEmbedAlbumArtOptions.TabIndex = 8;
            // 
            // gbEmbedAlbumArt
            // 
            this.gbEmbedAlbumArt.Controls.Add(this.chkEmbedAlbumArt);
            this.gbEmbedAlbumArt.Controls.Add(this.label5);
            this.gbEmbedAlbumArt.Controls.Add(this.nudHeight);
            this.gbEmbedAlbumArt.Controls.Add(this.label4);
            this.gbEmbedAlbumArt.Controls.Add(this.nudWidth);
            this.gbEmbedAlbumArt.Controls.Add(this.chkEnforceMaximumSize);
            this.gbEmbedAlbumArt.Controls.Add(this.rbSameTypeImages);
            this.gbEmbedAlbumArt.Controls.Add(this.rbAllImages);
            this.gbEmbedAlbumArt.Controls.Add(this.chkDeleteExistingImages);
            this.gbEmbedAlbumArt.Controls.Add(this.txtImageName);
            this.gbEmbedAlbumArt.Controls.Add(this.label3);
            this.gbEmbedAlbumArt.Controls.Add(this.label2);
            this.gbEmbedAlbumArt.Controls.Add(this.cbPictureTypes);
            this.gbEmbedAlbumArt.Controls.Add(this.txtDescription);
            this.gbEmbedAlbumArt.Controls.Add(this.label1);
            this.gbEmbedAlbumArt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEmbedAlbumArt.Location = new System.Drawing.Point(5, 5);
            this.gbEmbedAlbumArt.Name = "gbEmbedAlbumArt";
            this.gbEmbedAlbumArt.Size = new System.Drawing.Size(389, 179);
            this.gbEmbedAlbumArt.TabIndex = 22;
            this.gbEmbedAlbumArt.TabStop = false;
            this.gbEmbedAlbumArt.Text = "Embed Album Art Options";
            // 
            // chkEmbedAlbumArt
            // 
            this.chkEmbedAlbumArt.AutoSize = true;
            this.chkEmbedAlbumArt.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedAlbumArt;
            this.chkEmbedAlbumArt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmbedAlbumArt.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedAlbumArt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkEmbedAlbumArt.Location = new System.Drawing.Point(10, 19);
            this.chkEmbedAlbumArt.Name = "chkEmbedAlbumArt";
            this.chkEmbedAlbumArt.Size = new System.Drawing.Size(105, 17);
            this.chkEmbedAlbumArt.TabIndex = 31;
            this.chkEmbedAlbumArt.Text = "Embed album art";
            this.chkEmbedAlbumArt.UseVisualStyleBackColor = true;
            this.chkEmbedAlbumArt.CheckedChanged += new System.EventHandler(this.chkEmbedAlbumArt_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Height:";
            // 
            // nudHeight
            // 
            this.nudHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedImageMaxHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudHeight.Enabled = false;
            this.nudHeight.Location = new System.Drawing.Point(312, 145);
            this.nudHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(63, 20);
            this.nudHeight.TabIndex = 29;
            this.nudHeight.Value = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedImageMaxHeight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Width:";
            // 
            // nudWidth
            // 
            this.nudWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedImageMaxWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudWidth.Enabled = false;
            this.nudWidth.Location = new System.Drawing.Point(195, 145);
            this.nudWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(63, 20);
            this.nudWidth.TabIndex = 27;
            this.nudWidth.Value = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedImageMaxWidth;
            // 
            // chkEnforceMaximumSize
            // 
            this.chkEnforceMaximumSize.AutoSize = true;
            this.chkEnforceMaximumSize.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedEnforceMaximumSize;
            this.chkEnforceMaximumSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnforceMaximumSize.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedEnforceMaximumSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkEnforceMaximumSize.Location = new System.Drawing.Point(9, 146);
            this.chkEnforceMaximumSize.Name = "chkEnforceMaximumSize";
            this.chkEnforceMaximumSize.Size = new System.Drawing.Size(136, 17);
            this.chkEnforceMaximumSize.TabIndex = 26;
            this.chkEnforceMaximumSize.Text = "Enforce maximum size?";
            this.chkEnforceMaximumSize.UseVisualStyleBackColor = true;
            this.chkEnforceMaximumSize.CheckedChanged += new System.EventHandler(this.chkEnforceMaximumSize_CheckedChanged);
            // 
            // rbSameTypeImages
            // 
            this.rbSameTypeImages.AutoSize = true;
            this.rbSameTypeImages.Enabled = false;
            this.rbSameTypeImages.Location = new System.Drawing.Point(242, 121);
            this.rbSameTypeImages.Name = "rbSameTypeImages";
            this.rbSameTypeImages.Size = new System.Drawing.Size(111, 17);
            this.rbSameTypeImages.TabIndex = 25;
            this.rbSameTypeImages.TabStop = true;
            this.rbSameTypeImages.Text = "Same type images";
            this.rbSameTypeImages.UseVisualStyleBackColor = true;
            // 
            // rbAllImages
            // 
            this.rbAllImages.AutoSize = true;
            this.rbAllImages.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedDeleteAllImages;
            this.rbAllImages.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedDeleteAllImages", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rbAllImages.Enabled = false;
            this.rbAllImages.Location = new System.Drawing.Point(153, 121);
            this.rbAllImages.Name = "rbAllImages";
            this.rbAllImages.Size = new System.Drawing.Size(72, 17);
            this.rbAllImages.TabIndex = 24;
            this.rbAllImages.TabStop = true;
            this.rbAllImages.Text = "All images";
            this.rbAllImages.UseVisualStyleBackColor = true;
            // 
            // chkDeleteExistingImages
            // 
            this.chkDeleteExistingImages.AutoSize = true;
            this.chkDeleteExistingImages.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedDeleteImages;
            this.chkDeleteExistingImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteExistingImages.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedDeleteImages", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDeleteExistingImages.Location = new System.Drawing.Point(9, 122);
            this.chkDeleteExistingImages.Name = "chkDeleteExistingImages";
            this.chkDeleteExistingImages.Size = new System.Drawing.Size(137, 17);
            this.chkDeleteExistingImages.TabIndex = 23;
            this.chkDeleteExistingImages.Text = "Delete existing images?";
            this.chkDeleteExistingImages.UseVisualStyleBackColor = true;
            this.chkDeleteExistingImages.CheckedChanged += new System.EventHandler(this.chkDeleteExistingImages_CheckedChanged);
            // 
            // txtImageName
            // 
            this.txtImageName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtImageName.Location = new System.Drawing.Point(153, 40);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.Size = new System.Drawing.Size(205, 20);
            this.txtImageName.TabIndex = 22;
            this.txtImageName.Text = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedFileName;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Image file name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Select image type:";
            // 
            // cbPictureTypes
            // 
            this.cbPictureTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPictureTypes.FormattingEnabled = true;
            this.cbPictureTypes.Location = new System.Drawing.Point(153, 94);
            this.cbPictureTypes.Name = "cbPictureTypes";
            this.cbPictureTypes.Size = new System.Drawing.Size(205, 21);
            this.cbPictureTypes.TabIndex = 19;
            this.cbPictureTypes.SelectedIndexChanged += new System.EventHandler(this.cbPictureTypes_SelectedIndexChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ID3AlbumArtFixer.Properties.Settings.Default, "EmbedImageDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDescription.Location = new System.Drawing.Point(153, 67);
            this.txtDescription.MaxLength = 64;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(228, 20);
            this.txtDescription.TabIndex = 18;
            this.txtDescription.Text = global::ID3AlbumArtFixer.Properties.Settings.Default.EmbedImageDescription;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Description (64 char max):";
            // 
            // EmbedAlbumArtPage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "EmbedAlbumArtPage";
            this.Size = new System.Drawing.Size(409, 266);
            this.Load += new System.EventHandler(this.EmbedAlbumArtPage_Load);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlEmbedAlbumArtOptions.ResumeLayout(false);
            this.gbEmbedAlbumArt.ResumeLayout(false);
            this.gbEmbedAlbumArt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
            SetWizardButtons(WizardButtons.Back | WizardButtons.Help | WizardButtons.About | WizardButtons.Finish);
		}

        #region IAlbumArtFixerJob Members

        public AlbumArtFixerJob UpdateJobFromForm(AlbumArtFixerJob job)
        {
            job.EmbedAlbumArt = chkEmbedAlbumArt.Checked;
            if (job.EmbedAlbumArt)
            {
                EmbedPictureJob embedJob = new EmbedPictureJob();
                embedJob.EnforceMaxSize = chkEnforceMaximumSize.Checked;
                if (embedJob.EnforceMaxSize)
                {
                    embedJob.MaxSize = new Size((int)nudWidth.Value, (int)nudHeight.Value);
                }
                if (chkDeleteExistingImages.Checked)
                {
                    embedJob.DeleteExistingImages = rbAllImages.Checked ? DeleteEmbeddedImages.All : DeleteEmbeddedImages.SameType;
                }
                else
                {
                    embedJob.DeleteExistingImages = DeleteEmbeddedImages.None;
                }
                embedJob.Id3ImageType = (byte)cbPictureTypes.SelectedIndex;
                embedJob.ImageDescription = txtDescription.Text.Trim();
                embedJob.ImageFileName = txtImageName.Text.Trim();

                job.EmbedPictureJob = embedJob;
            }

            return job;
        }

        #endregion

        private void EmbedAlbumArtPage_Load(object sender, EventArgs e)
        {
            LoadSettings();
            SetEmbedEnabled(chkEmbedAlbumArt.Checked);
        }

        private void LoadSettings()
        {
            cbPictureTypes.SelectedIndex = Settings.Default.EmbedImageType;
            if (cbPictureTypes.SelectedIndex == -1)
            {
                cbPictureTypes.SelectedIndex = 3;
            }
            rbSameTypeImages.Checked = !rbAllImages.Checked;
        }

        #region IAlbumArtFixerSettings Members

        public void SetSettingsFromForm()
        {
            Settings.Default.EmbedImageType = cbPictureTypes.SelectedIndex;
        }

        #endregion

        private void chkEmbedAlbumArt_CheckedChanged(object sender, EventArgs e)
        {
            SetEmbedEnabled(chkEmbedAlbumArt.Checked);
        }

        private void SetEmbedEnabled(bool enabled)
        {
            txtDescription.Enabled
                = txtImageName.Enabled
                = cbPictureTypes.Enabled
                = chkDeleteExistingImages.Enabled
                = chkEnforceMaximumSize.Enabled
                = enabled;

            SetEmbedSizeEnabled(enabled);
            SetEmbedDeleteEnabled();
        }

        private void SetEmbedSizeEnabled(bool enabled)
        {
            nudHeight.Enabled = nudWidth.Enabled
                = (chkEnforceMaximumSize.Enabled && chkEnforceMaximumSize.Checked);
        }

        private void SetEmbedDeleteEnabled()
        {
            rbAllImages.Enabled 
                = rbSameTypeImages.Enabled 
                = (chkDeleteExistingImages.Enabled && chkDeleteExistingImages.Checked);
        }

        private void chkDeleteExistingImages_CheckedChanged(object sender, EventArgs e)
        {
            SetEmbedEnabled(chkEmbedAlbumArt.Checked);
        }

        private void chkEnforceMaximumSize_CheckedChanged(object sender, EventArgs e)
        {
            SetEmbedEnabled(chkEmbedAlbumArt.Checked);
        }

        private void cbPictureTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iconIsSelected && cbPictureTypes.SelectedIndex != 1)
            {
                iconIsSelected = false;
                nudWidth.Value = maxWidthPreIconSelection;
                nudHeight.Value = maxHeightPreIconSelection;
                chkEnforceMaximumSize.Checked = enforceMaxPreIconSelection;
            }

            if (cbPictureTypes.SelectedIndex == 1)
            {
                iconIsSelected = true;
                maxHeightPreIconSelection = (int)nudHeight.Value;
                maxWidthPreIconSelection = (int)nudWidth.Value;
                enforceMaxPreIconSelection = chkEnforceMaximumSize.Checked;

                chkEnforceMaximumSize.Checked = true;
                nudHeight.Value = nudWidth.Value = 32;
                chkEnforceMaximumSize.Enabled = false;
                EnableWidthHeightNud(false);
            }
            else
            {
                chkEnforceMaximumSize.Enabled = true;
                EnableWidthHeightNud(chkEnforceMaximumSize.Checked);
            }
        }

        private void EnableWidthHeightNud(bool enable)
        {
            if (enable)
            {
                this.nudHeight.Enabled = this.nudWidth.Enabled = true;
            }
            else
            {
                this.nudHeight.Enabled = this.nudWidth.Enabled = false;
            }
        }
    }
}

