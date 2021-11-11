namespace Preston.Media
{
    partial class RawV2TagEditor
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
            this.lblMp3File = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMp3File
            // 
            this.lblMp3File.AutoSize = true;
            this.lblMp3File.Location = new System.Drawing.Point(7, 7);
            this.lblMp3File.Name = "lblMp3File";
            this.lblMp3File.Size = new System.Drawing.Size(51, 13);
            this.lblMp3File.TabIndex = 0;
            this.lblMp3File.Text = "MP3 File:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(7, 23);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(560, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(573, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // RawV2TagEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 215);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblMp3File);
            this.Name = "RawV2TagEditor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RawV2TagEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMp3File;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
    }
}

