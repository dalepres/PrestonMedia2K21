namespace Preston.Media
{
    partial class TrackTagEditor : System.IDisposable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackTagEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.iD3V2Editor = new Preston.Media.ID3V2Editor();
            this.cbTags = new System.Windows.Forms.ComboBox();
            this.lblTteChooseTag = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblTteTrackFileName = new System.Windows.Forms.Label();
            this.btnChangeFileName = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTrackName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iD3V2Editor);
            this.panel1.Location = new System.Drawing.Point(7, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 220);
            this.panel1.TabIndex = 0;
            // 
            // iD3V2Editor
            // 
            this.iD3V2Editor.Id3Tag = null;
            this.iD3V2Editor.Location = new System.Drawing.Point(0, 0);
            this.iD3V2Editor.Margin = new System.Windows.Forms.Padding(0);
            this.iD3V2Editor.Name = "iD3V2Editor";
            this.iD3V2Editor.Size = new System.Drawing.Size(651, 220);
            this.iD3V2Editor.TabIndex = 2;
            this.iD3V2Editor.Visible = false;
            this.iD3V2Editor.FrameDeleted += new Preston.Media.FrameDeletedEventHandler(this.iD3V2Editor_FrameDeleted);
            // 
            // cbTags
            // 
            this.cbTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTags.Enabled = false;
            this.cbTags.FormattingEnabled = true;
            this.cbTags.Location = new System.Drawing.Point(113, 34);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(150, 21);
            this.cbTags.TabIndex = 1;
            this.cbTags.SelectedIndexChanged += new System.EventHandler(this.cbTags_SelectedIndexChanged);
            // 
            // lblTteChooseTag
            // 
            this.lblTteChooseTag.AutoSize = true;
            this.lblTteChooseTag.Location = new System.Drawing.Point(9, 37);
            this.lblTteChooseTag.Name = "lblTteChooseTag";
            this.lblTteChooseTag.Size = new System.Drawing.Size(101, 13);
            this.lblTteChooseTag.TabIndex = 2;
            this.lblTteChooseTag.Text = "Choose tag to view:";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(269, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = global::Preston.Media.Properties.Resources.tteBtnDeleteTagText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTteTrackFileName
            // 
            this.lblTteTrackFileName.AutoSize = true;
            this.lblTteTrackFileName.Location = new System.Drawing.Point(9, 8);
            this.lblTteTrackFileName.Name = "lblTteTrackFileName";
            this.lblTteTrackFileName.Size = new System.Drawing.Size(83, 13);
            this.lblTteTrackFileName.TabIndex = 4;
            this.lblTteTrackFileName.Text = "Track file name:";
            // 
            // btnChangeFileName
            // 
            this.btnChangeFileName.Enabled = false;
            this.btnChangeFileName.Location = new System.Drawing.Point(565, 32);
            this.btnChangeFileName.Name = "btnChangeFileName";
            this.btnChangeFileName.Size = new System.Drawing.Size(93, 23);
            this.btnChangeFileName.TabIndex = 7;
            this.btnChangeFileName.Text = global::Preston.Media.Properties.Resources.tteBtnChangeFileNameText;
            this.btnChangeFileName.UseVisualStyleBackColor = true;
            this.btnChangeFileName.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(581, 293);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(502, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(423, 293);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Close";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(565, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(93, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblTrackName
            // 
            this.lblTrackName.AllowDrop = true;
            this.lblTrackName.Location = new System.Drawing.Point(110, 8);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(449, 23);
            this.lblTrackName.TabIndex = 14;
            this.lblTrackName.Text = "Drop MP3 file here or browse to select file to view.";
            this.lblTrackName.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFileName_DragDrop);
            this.lblTrackName.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFileName_DragEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "MP3 files (*.mp3)|*.mp3|ID3 files (*.id3)|*.id3|All files (*.*)|*.*";
            // 
            // TrackTagEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 322);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnChangeFileName);
            this.Controls.Add(this.lblTteTrackFileName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblTteChooseTag);
            this.Controls.Add(this.cbTags);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackTagEditor";
            this.Text = "ID3 Raw Tag Viewer";
            this.Load += new System.EventHandler(this.TrackTagEditor_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbTags;
        private System.Windows.Forms.Label lblTteChooseTag;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTteTrackFileName;
        private System.Windows.Forms.Button btnChangeFileName;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private ID3V2Editor iD3V2Editor;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblTrackName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}