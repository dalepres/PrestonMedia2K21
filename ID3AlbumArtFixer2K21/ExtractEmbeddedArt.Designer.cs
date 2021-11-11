namespace ID3AlbumArtFixer
{
    partial class ExtractEmbeddedArt
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlByTrack = new System.Windows.Forms.Panel();
            this.pnlByFolder = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.folderSelector2 = new CommonControls.FolderSelector();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExtractEmbedded = new System.Windows.Forms.Button();
            this.btnCloseExtract = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.panel3.SuspendLayout();
            this.pnlByFolder.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlByTrack);
            this.panel3.Controls.Add(this.pnlByFolder);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(738, 526);
            this.panel3.TabIndex = 3;
            // 
            // pnlByTrack
            // 
            this.pnlByTrack.Location = new System.Drawing.Point(242, 215);
            this.pnlByTrack.Name = "pnlByTrack";
            this.pnlByTrack.Size = new System.Drawing.Size(200, 100);
            this.pnlByTrack.TabIndex = 6;
            // 
            // pnlByFolder
            // 
            this.pnlByFolder.Controls.Add(this.groupBox1);
            this.pnlByFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlByFolder.Location = new System.Drawing.Point(0, 87);
            this.pnlByFolder.Name = "pnlByFolder";
            this.pnlByFolder.Padding = new System.Windows.Forms.Padding(5);
            this.pnlByFolder.Size = new System.Drawing.Size(738, 117);
            this.pnlByFolder.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.folderSelector2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "By Folder";
            // 
            // folderSelector2
            // 
            this.folderSelector2.Dock = System.Windows.Forms.DockStyle.Top;
            this.folderSelector2.Location = new System.Drawing.Point(3, 16);
            this.folderSelector2.MaximumSize = new System.Drawing.Size(0, 60);
            this.folderSelector2.MinimumSize = new System.Drawing.Size(150, 60);
            this.folderSelector2.Name = "folderSelector2";
            this.folderSelector2.SaveFolderHistoryOnSelection = true;
            this.folderSelector2.SelectedFolder = "";
            this.folderSelector2.Size = new System.Drawing.Size(722, 60);
            this.folderSelector2.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(5);
            this.panel7.Size = new System.Drawing.Size(738, 87);
            this.panel7.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 77);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(20, 413);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(633, 43);
            this.textBox2.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExtractEmbedded);
            this.panel5.Controls.Add(this.btnCloseExtract);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 471);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(738, 33);
            this.panel5.TabIndex = 3;
            // 
            // btnExtractEmbedded
            // 
            this.btnExtractEmbedded.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExtractEmbedded.Location = new System.Drawing.Point(575, 5);
            this.btnExtractEmbedded.Name = "btnExtractEmbedded";
            this.btnExtractEmbedded.Size = new System.Drawing.Size(75, 23);
            this.btnExtractEmbedded.TabIndex = 1;
            this.btnExtractEmbedded.Text = "Extract";
            this.btnExtractEmbedded.UseVisualStyleBackColor = true;
            // 
            // btnCloseExtract
            // 
            this.btnCloseExtract.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCloseExtract.Location = new System.Drawing.Point(656, 5);
            this.btnCloseExtract.Name = "btnCloseExtract";
            this.btnCloseExtract.Size = new System.Drawing.Size(75, 23);
            this.btnCloseExtract.TabIndex = 0;
            this.btnCloseExtract.Text = "Close";
            this.btnCloseExtract.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.statusStrip2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 504);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(738, 22);
            this.panel6.TabIndex = 2;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Location = new System.Drawing.Point(0, 0);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(738, 22);
            this.statusStrip2.TabIndex = 0;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // ExtractEmbeddedArt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 526);
            this.Controls.Add(this.panel3);
            this.Name = "ExtractEmbeddedArt";
            this.Text = "ExtractEmbeddedArt";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlByFolder.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlByTrack;
        private System.Windows.Forms.Panel pnlByFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private CommonControls.FolderSelector folderSelector2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExtractEmbedded;
        private System.Windows.Forms.Button btnCloseExtract;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.StatusStrip statusStrip2;
    }
}