namespace ID3AlbumArtFixer
{
    partial class AlbumArtFixerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAlbumArtFixerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMenuAndMain = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pbProgress = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlSecurity = new System.Windows.Forms.Panel();
            this.grpAlbumArtFileSecurity = new System.Windows.Forms.GroupBox();
            this.cbFullControl = new System.Windows.Forms.ComboBox();
            this.chkRestrictAlbumArtAccess = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbReadOnly = new System.Windows.Forms.ComboBox();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.gbAlbumArtOptions = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbDoNotCreate = new System.Windows.Forms.RadioButton();
            this.rbLargestImage = new System.Windows.Forms.RadioButton();
            this.rbFromFileNamed = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMaxWidth = new System.Windows.Forms.NumericUpDown();
            this.pnlExecuteButton = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnCloseCreate = new System.Windows.Forms.Button();
            this.pnlStatusStrip = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderSelector1 = new CommonControls.FolderSelector();
            this.directoryEntry = new System.DirectoryServices.DirectoryEntry();
            this.pnlMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlMenuAndMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).BeginInit();
            this.pnlSecurity.SuspendLayout();
            this.grpAlbumArtFileSecurity.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.gbAlbumArtOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWidth)).BeginInit();
            this.pnlExecuteButton.SuspendLayout();
            this.pnlStatusStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.menuStrip1);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(613, 30);
            this.pnlMenu.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.aboutAlbumArtFixerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.contentsToolStripMenuItem.Text = "Contents";
            // 
            // aboutAlbumArtFixerToolStripMenuItem
            // 
            this.aboutAlbumArtFixerToolStripMenuItem.Name = "aboutAlbumArtFixerToolStripMenuItem";
            this.aboutAlbumArtFixerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.aboutAlbumArtFixerToolStripMenuItem.Text = "About Album Art Fixer";
            this.aboutAlbumArtFixerToolStripMenuItem.Click += new System.EventHandler(this.aboutAlbumArtFixerToolStripMenuItem_Click);
            // 
            // pnlMenuAndMain
            // 
            this.pnlMenuAndMain.Controls.Add(this.pnlMain);
            this.pnlMenuAndMain.Controls.Add(this.pnlMenu);
            this.pnlMenuAndMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenuAndMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuAndMain.Name = "pnlMenuAndMain";
            this.pnlMenuAndMain.Size = new System.Drawing.Size(613, 668);
            this.pnlMenuAndMain.TabIndex = 3;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlProgress);
            this.pnlMain.Controls.Add(this.pnlSecurity);
            this.pnlMain.Controls.Add(this.pnlOptions);
            this.pnlMain.Controls.Add(this.pnlExecuteButton);
            this.pnlMain.Controls.Add(this.pnlStatusStrip);
            this.pnlMain.Controls.Add(this.folderSelector1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 30);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(613, 638);
            this.pnlMain.TabIndex = 4;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.grpProgress);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgress.Location = new System.Drawing.Point(0, 352);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(5);
            this.pnlProgress.Size = new System.Drawing.Size(613, 248);
            this.pnlProgress.TabIndex = 7;
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
            this.grpProgress.Size = new System.Drawing.Size(603, 238);
            this.grpProgress.TabIndex = 0;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(491, 19);
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
            this.textBox1.Size = new System.Drawing.Size(472, 103);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(10, 139);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(13, 155);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(578, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // pnlSecurity
            // 
            this.pnlSecurity.Controls.Add(this.grpAlbumArtFileSecurity);
            this.pnlSecurity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSecurity.Location = new System.Drawing.Point(0, 235);
            this.pnlSecurity.Name = "pnlSecurity";
            this.pnlSecurity.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSecurity.Size = new System.Drawing.Size(613, 117);
            this.pnlSecurity.TabIndex = 6;
            // 
            // grpAlbumArtFileSecurity
            // 
            this.grpAlbumArtFileSecurity.Controls.Add(this.cbFullControl);
            this.grpAlbumArtFileSecurity.Controls.Add(this.chkRestrictAlbumArtAccess);
            this.grpAlbumArtFileSecurity.Controls.Add(this.label4);
            this.grpAlbumArtFileSecurity.Controls.Add(this.label5);
            this.grpAlbumArtFileSecurity.Controls.Add(this.cbReadOnly);
            this.grpAlbumArtFileSecurity.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAlbumArtFileSecurity.Location = new System.Drawing.Point(5, 5);
            this.grpAlbumArtFileSecurity.Name = "grpAlbumArtFileSecurity";
            this.grpAlbumArtFileSecurity.Size = new System.Drawing.Size(603, 107);
            this.grpAlbumArtFileSecurity.TabIndex = 11;
            this.grpAlbumArtFileSecurity.TabStop = false;
            this.grpAlbumArtFileSecurity.Text = "Album Art File Security";
            // 
            // cbFullControl
            // 
            this.cbFullControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFullControl.Enabled = false;
            this.cbFullControl.FormattingEnabled = true;
            this.cbFullControl.Location = new System.Drawing.Point(175, 50);
            this.cbFullControl.Name = "cbFullControl";
            this.cbFullControl.Size = new System.Drawing.Size(229, 21);
            this.cbFullControl.TabIndex = 12;
            // 
            // chkRestrictAlbumArtAccess
            // 
            this.chkRestrictAlbumArtAccess.AutoSize = true;
            this.chkRestrictAlbumArtAccess.Location = new System.Drawing.Point(12, 23);
            this.chkRestrictAlbumArtAccess.Name = "chkRestrictAlbumArtAccess";
            this.chkRestrictAlbumArtAccess.Size = new System.Drawing.Size(148, 17);
            this.chkRestrictAlbumArtAccess.TabIndex = 11;
            this.chkRestrictAlbumArtAccess.Text = "Restrict Album Art Access";
            this.chkRestrictAlbumArtAccess.UseVisualStyleBackColor = true;
            this.chkRestrictAlbumArtAccess.CheckedChanged += new System.EventHandler(this.chkRestrictAlbumArtAccess_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Full Control User or Group:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Read Only User or Group:";
            // 
            // cbReadOnly
            // 
            this.cbReadOnly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReadOnly.Enabled = false;
            this.cbReadOnly.FormattingEnabled = true;
            this.cbReadOnly.Location = new System.Drawing.Point(175, 80);
            this.cbReadOnly.Name = "cbReadOnly";
            this.cbReadOnly.Size = new System.Drawing.Size(229, 21);
            this.cbReadOnly.TabIndex = 7;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.gbAlbumArtOptions);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOptions.Location = new System.Drawing.Point(0, 80);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Padding = new System.Windows.Forms.Padding(5);
            this.pnlOptions.Size = new System.Drawing.Size(613, 155);
            this.pnlOptions.TabIndex = 5;
            // 
            // gbAlbumArtOptions
            // 
            this.gbAlbumArtOptions.Controls.Add(this.textBox4);
            this.gbAlbumArtOptions.Controls.Add(this.label9);
            this.gbAlbumArtOptions.Controls.Add(this.label8);
            this.gbAlbumArtOptions.Controls.Add(this.textBox3);
            this.gbAlbumArtOptions.Controls.Add(this.comboBox1);
            this.gbAlbumArtOptions.Controls.Add(this.checkBox1);
            this.gbAlbumArtOptions.Controls.Add(this.label6);
            this.gbAlbumArtOptions.Controls.Add(this.rbDoNotCreate);
            this.gbAlbumArtOptions.Controls.Add(this.rbLargestImage);
            this.gbAlbumArtOptions.Controls.Add(this.rbFromFileNamed);
            this.gbAlbumArtOptions.Controls.Add(this.label3);
            this.gbAlbumArtOptions.Controls.Add(this.cbQuality);
            this.gbAlbumArtOptions.Controls.Add(this.label2);
            this.gbAlbumArtOptions.Controls.Add(this.nudMaxHeight);
            this.gbAlbumArtOptions.Controls.Add(this.label1);
            this.gbAlbumArtOptions.Controls.Add(this.nudMaxWidth);
            this.gbAlbumArtOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlbumArtOptions.Location = new System.Drawing.Point(5, 5);
            this.gbAlbumArtOptions.Name = "gbAlbumArtOptions";
            this.gbAlbumArtOptions.Size = new System.Drawing.Size(603, 145);
            this.gbAlbumArtOptions.TabIndex = 0;
            this.gbAlbumArtOptions.TabStop = false;
            this.gbAlbumArtOptions.Text = "Album Art Options";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(447, 46);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(144, 20);
            this.textBox4.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Image comments:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Image type:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(117, 118);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(474, 20);
            this.textBox3.TabIndex = 15;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(117, 92);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(187, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(173, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Embed album art in MP3 tracks";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Create album art from:";
            // 
            // rbDoNotCreate
            // 
            this.rbDoNotCreate.AutoSize = true;
            this.rbDoNotCreate.Location = new System.Drawing.Point(350, 93);
            this.rbDoNotCreate.Name = "rbDoNotCreate";
            this.rbDoNotCreate.Size = new System.Drawing.Size(90, 17);
            this.rbDoNotCreate.TabIndex = 9;
            this.rbDoNotCreate.TabStop = true;
            this.rbDoNotCreate.Text = "Do not create";
            this.rbDoNotCreate.UseVisualStyleBackColor = true;
            // 
            // rbLargestImage
            // 
            this.rbLargestImage.AutoSize = true;
            this.rbLargestImage.Location = new System.Drawing.Point(350, 70);
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
            this.rbFromFileNamed.Location = new System.Drawing.Point(350, 47);
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
            this.label3.Location = new System.Drawing.Point(167, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Image Quality:";
            // 
            // cbQuality
            // 
            this.cbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cbQuality.Location = new System.Drawing.Point(251, 46);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(53, 21);
            this.cbQuality.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max Height:";
            // 
            // nudMaxHeight
            // 
            this.nudMaxHeight.Location = new System.Drawing.Point(243, 19);
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
            this.nudMaxHeight.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Width:";
            // 
            // nudMaxWidth
            // 
            this.nudMaxWidth.Location = new System.Drawing.Point(88, 19);
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
            this.nudMaxWidth.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // pnlExecuteButton
            // 
            this.pnlExecuteButton.Controls.Add(this.btnCancel);
            this.pnlExecuteButton.Controls.Add(this.btnExecute);
            this.pnlExecuteButton.Controls.Add(this.btnCloseCreate);
            this.pnlExecuteButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlExecuteButton.Location = new System.Drawing.Point(0, 600);
            this.pnlExecuteButton.Name = "pnlExecuteButton";
            this.pnlExecuteButton.Size = new System.Drawing.Size(613, 16);
            this.pnlExecuteButton.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(450, -3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExecute.Location = new System.Drawing.Point(369, -3);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnCloseCreate
            // 
            this.btnCloseCreate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCloseCreate.Location = new System.Drawing.Point(531, -3);
            this.btnCloseCreate.Name = "btnCloseCreate";
            this.btnCloseCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCloseCreate.TabIndex = 0;
            this.btnCloseCreate.Text = "Close";
            this.btnCloseCreate.UseVisualStyleBackColor = true;
            this.btnCloseCreate.Click += new System.EventHandler(this.btnCloseCreate_Click);
            // 
            // pnlStatusStrip
            // 
            this.pnlStatusStrip.Controls.Add(this.statusStrip1);
            this.pnlStatusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusStrip.Location = new System.Drawing.Point(0, 616);
            this.pnlStatusStrip.Name = "pnlStatusStrip";
            this.pnlStatusStrip.Size = new System.Drawing.Size(613, 22);
            this.pnlStatusStrip.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
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
            this.folderSelector1.Size = new System.Drawing.Size(613, 80);
            this.folderSelector1.TabIndex = 1;
            // 
            // AlbumArtFixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 668);
            this.Controls.Add(this.pnlMenuAndMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AlbumArtFixerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Preston Media Album Art Fixer";
            this.Load += new System.EventHandler(this.AlbumArtFixerForm_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMenuAndMain.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).EndInit();
            this.pnlSecurity.ResumeLayout(false);
            this.grpAlbumArtFileSecurity.ResumeLayout(false);
            this.grpAlbumArtFileSecurity.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.gbAlbumArtOptions.ResumeLayout(false);
            this.gbAlbumArtOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWidth)).EndInit();
            this.pnlExecuteButton.ResumeLayout(false);
            this.pnlStatusStrip.ResumeLayout(false);
            this.pnlStatusStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlMenuAndMain;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.GroupBox gbAlbumArtOptions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMaxWidth;
        private System.Windows.Forms.Panel pnlExecuteButton;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnCloseCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.DirectoryServices.DirectoryEntry directoryEntry;
        private System.Windows.Forms.ComboBox cbReadOnly;
        private System.Windows.Forms.Panel pnlSecurity;
        private System.Windows.Forms.GroupBox grpAlbumArtFileSecurity;
        private System.Windows.Forms.CheckBox chkRestrictAlbumArtAccess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbFullControl;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.PictureBox pbProgress;
        private System.Windows.Forms.Panel pnlStatusStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAlbumArtFixerToolStripMenuItem;
        private System.Windows.Forms.RadioButton rbDoNotCreate;
        private System.Windows.Forms.RadioButton rbLargestImage;
        private System.Windows.Forms.RadioButton rbFromFileNamed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private CommonControls.FolderSelector folderSelector1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox4;



    }
}

