using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using ID3AlbumArtFixer.Wizard;
using ID3AlbumArtFixer.Properties;

namespace ID3AlbumArtFixer.Wizard
{
    internal class AlbumArtOptionsPage : InternalWizardPage, IAlbumArtFixerJob, IAlbumArtFixerSettings
    {
        private Panel pnlMain;
        private Panel pnlOptions;
        private GroupBox gbAlbumArtOptions;
        private CheckBox chkCreateResizeAlbumArt;
        private TextBox txtArtSourceFileName;
        private Label label6;
        private RadioButton rbLargestImage;
        private RadioButton rbFromFileNamed;
        private Label label3;
        private ComboBox cbQuality;
        private Label label2;
        private NumericUpDown nudMaxHeight;
        private Label label4;
        private NumericUpDown nudMaxWidth;
		private System.ComponentModel.IContainer components = null;

        internal AlbumArtOptionsPage()
		{
			InitializeComponent();

            LoadSettings();
            SetOptionsEnabled(chkCreateResizeAlbumArt.Checked);
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
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.gbAlbumArtOptions = new System.Windows.Forms.GroupBox();
            this.chkCreateResizeAlbumArt = new System.Windows.Forms.CheckBox();
            this.txtArtSourceFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbLargestImage = new System.Windows.Forms.RadioButton();
            this.rbFromFileNamed = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMaxWidth = new System.Windows.Forms.NumericUpDown();
            this.pnlMain.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.gbAlbumArtOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(508, 64);
            this.Banner.Subtitle = "Select album art options.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlOptions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(508, 155);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.gbAlbumArtOptions);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOptions.Location = new System.Drawing.Point(5, 5);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Padding = new System.Windows.Forms.Padding(5);
            this.pnlOptions.Size = new System.Drawing.Size(498, 142);
            this.pnlOptions.TabIndex = 7;
            // 
            // gbAlbumArtOptions
            // 
            this.gbAlbumArtOptions.Controls.Add(this.chkCreateResizeAlbumArt);
            this.gbAlbumArtOptions.Controls.Add(this.txtArtSourceFileName);
            this.gbAlbumArtOptions.Controls.Add(this.label6);
            this.gbAlbumArtOptions.Controls.Add(this.rbLargestImage);
            this.gbAlbumArtOptions.Controls.Add(this.rbFromFileNamed);
            this.gbAlbumArtOptions.Controls.Add(this.label3);
            this.gbAlbumArtOptions.Controls.Add(this.cbQuality);
            this.gbAlbumArtOptions.Controls.Add(this.label2);
            this.gbAlbumArtOptions.Controls.Add(this.nudMaxHeight);
            this.gbAlbumArtOptions.Controls.Add(this.label4);
            this.gbAlbumArtOptions.Controls.Add(this.nudMaxWidth);
            this.gbAlbumArtOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlbumArtOptions.Location = new System.Drawing.Point(5, 5);
            this.gbAlbumArtOptions.Name = "gbAlbumArtOptions";
            this.gbAlbumArtOptions.Size = new System.Drawing.Size(488, 132);
            this.gbAlbumArtOptions.TabIndex = 0;
            this.gbAlbumArtOptions.TabStop = false;
            this.gbAlbumArtOptions.Text = "Album Art Options";
            // 
            // chkCreateResizeAlbumArt
            // 
            this.chkCreateResizeAlbumArt.AutoSize = true;
            this.chkCreateResizeAlbumArt.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.CreateResizeAlbumArt;
            this.chkCreateResizeAlbumArt.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "CreateResizeAlbumArt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCreateResizeAlbumArt.Location = new System.Drawing.Point(25, 25);
            this.chkCreateResizeAlbumArt.Name = "chkCreateResizeAlbumArt";
            this.chkCreateResizeAlbumArt.Size = new System.Drawing.Size(145, 17);
            this.chkCreateResizeAlbumArt.TabIndex = 19;
            this.chkCreateResizeAlbumArt.Text = "Create and size album art";
            this.chkCreateResizeAlbumArt.UseVisualStyleBackColor = true;
            this.chkCreateResizeAlbumArt.CheckedChanged += new System.EventHandler(this.chkCreateResizeAlbumArt_CheckedChanged);
            // 
            // txtArtSourceFileName
            // 
            this.txtArtSourceFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArtSourceFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ID3AlbumArtFixer.Properties.Settings.Default, "AlbumArtFromFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtArtSourceFileName.Enabled = false;
            this.txtArtSourceFileName.Location = new System.Drawing.Point(311, 47);
            this.txtArtSourceFileName.Name = "txtArtSourceFileName";
            this.txtArtSourceFileName.Size = new System.Drawing.Size(171, 20);
            this.txtArtSourceFileName.TabIndex = 18;
            this.txtArtSourceFileName.Text = global::ID3AlbumArtFixer.Properties.Settings.Default.AlbumArtFromFile;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Create album art from:";
            // 
            // rbLargestImage
            // 
            this.rbLargestImage.AutoSize = true;
            this.rbLargestImage.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.AlbumArtFromLargestImage;
            this.rbLargestImage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "AlbumArtFromLargestImage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rbLargestImage.Enabled = false;
            this.rbLargestImage.Location = new System.Drawing.Point(217, 74);
            this.rbLargestImage.Name = "rbLargestImage";
            this.rbLargestImage.Size = new System.Drawing.Size(91, 17);
            this.rbLargestImage.TabIndex = 8;
            this.rbLargestImage.TabStop = true;
            this.rbLargestImage.Text = "Largest image";
            this.rbLargestImage.UseVisualStyleBackColor = true;
            // 
            // rbFromFileNamed
            // 
            this.rbFromFileNamed.AutoSize = true;
            this.rbFromFileNamed.Enabled = false;
            this.rbFromFileNamed.Location = new System.Drawing.Point(217, 48);
            this.rbFromFileNamed.Name = "rbFromFileNamed";
            this.rbFromFileNamed.Size = new System.Drawing.Size(79, 17);
            this.rbFromFileNamed.TabIndex = 7;
            this.rbFromFileNamed.TabStop = true;
            this.rbFromFileNamed.Text = "File named:";
            this.rbFromFileNamed.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Image Quality:";
            // 
            // cbQuality
            // 
            this.cbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuality.Enabled = false;
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100"});
            this.cbQuality.Location = new System.Drawing.Point(101, 100);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(53, 21);
            this.cbQuality.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max Height:";
            // 
            // nudMaxHeight
            // 
            this.nudMaxHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ID3AlbumArtFixer.Properties.Settings.Default, "AlbumArtMaxHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudMaxHeight.Enabled = false;
            this.nudMaxHeight.Location = new System.Drawing.Point(101, 74);
            this.nudMaxHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxHeight.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudMaxHeight.Name = "nudMaxHeight";
            this.nudMaxHeight.Size = new System.Drawing.Size(61, 20);
            this.nudMaxHeight.TabIndex = 2;
            this.nudMaxHeight.Value = global::ID3AlbumArtFixer.Properties.Settings.Default.AlbumArtMaxHeight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Max Width:";
            // 
            // nudMaxWidth
            // 
            this.nudMaxWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ID3AlbumArtFixer.Properties.Settings.Default, "AlbumArtMaxWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudMaxWidth.Enabled = false;
            this.nudMaxWidth.Location = new System.Drawing.Point(101, 48);
            this.nudMaxWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxWidth.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudMaxWidth.Name = "nudMaxWidth";
            this.nudMaxWidth.Size = new System.Drawing.Size(61, 20);
            this.nudMaxWidth.TabIndex = 0;
            this.nudMaxWidth.Value = global::ID3AlbumArtFixer.Properties.Settings.Default.AlbumArtMaxWidth;
            // 
            // AlbumArtOptionsPage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "AlbumArtOptionsPage";
            this.Size = new System.Drawing.Size(508, 219);
            this.Load += new System.EventHandler(this.AlbumArtOptionsPage_Load);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.gbAlbumArtOptions.ResumeLayout(false);
            this.gbAlbumArtOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWidth)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void WizardPage_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetWizardButtons(WizardButtons.Back | WizardButtons.Next | WizardButtons.Help | WizardButtons.About);
		}

        #region IAlbumArtFixerJob Members

        public AlbumArtFixerJob UpdateJobFromForm(AlbumArtFixerJob job)
        {
            job.AlbumArtSource = rbFromFileNamed.Checked ? AlbumArtSource.FileName : AlbumArtSource.LargestImage;
            job.AlbumArtSourceFileName = job.AlbumArtSource == AlbumArtSource.FileName ? txtArtSourceFileName.Text.Trim() : string.Empty;
            job.CreateAlbumArt = chkCreateResizeAlbumArt.Checked;
            job.MaxSize = new Size((int)nudMaxWidth.Value, (int)nudMaxHeight.Value);
            job.ImageQuality = int.Parse(cbQuality.Text);

            return job;
        }

        #endregion

        private void chkCreateResizeAlbumArt_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsEnabled(chkCreateResizeAlbumArt.Checked);
        }

        private void SetOptionsEnabled(bool enabled)
        {
            this.nudMaxHeight.Enabled
                = nudMaxWidth.Enabled
                = cbQuality.Enabled
                = rbFromFileNamed.Enabled
                = rbLargestImage.Enabled
                = txtArtSourceFileName.Enabled
                = enabled;
        }

        private void AlbumArtOptionsPage_Load(object sender, EventArgs e)
        {
        }

        private void LoadSettings()
        {
            cbQuality.SelectedItem = Settings.Default.AlbumArtImageQualityPerCent > 0 ? Settings.Default.AlbumArtImageQualityPerCent.ToString() : "70";

            if (cbQuality.SelectedIndex == -1)
            {
                cbQuality.SelectedItem = "70";
            }

            rbFromFileNamed.Checked = !rbLargestImage.Checked;
       }

        #region IAlbumArtFixerSettings Members

        public void SetSettingsFromForm()
        {
            Settings.Default.AlbumArtImageQualityPerCent = Convert.ToInt32(cbQuality.Text);
        }

        #endregion
    }
}

