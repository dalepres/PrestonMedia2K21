namespace Preston.Media.ID3TagEditor
{
    partial class ImageFrameEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.cbImageTypes = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.picturePanel = new System.Windows.Forms.Panel();
            this.lblImageType = new System.Windows.Forms.Label();
            this.lblImageDescription = new System.Windows.Forms.Label();
            this.txtImageType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.picturePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Image = global::Preston.Media.Properties.Resources.DropFileHere;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(72, 72);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // cbImageTypes
            // 
            this.cbImageTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageTypes.FormattingEnabled = true;
            this.cbImageTypes.Location = new System.Drawing.Point(97, 20);
            this.cbImageTypes.Name = "cbImageTypes";
            this.cbImageTypes.Size = new System.Drawing.Size(180, 21);
            this.cbImageTypes.TabIndex = 1;
            this.cbImageTypes.SelectedIndexChanged += new System.EventHandler(this.cbImageTypes_SelectedIndexChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(97, 66);
            this.txtDescription.MaxLength = 1000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(180, 42);
            this.txtDescription.TabIndex = 2;
            // 
            // picturePanel
            // 
            this.picturePanel.Controls.Add(this.pictureBox);
            this.picturePanel.Location = new System.Drawing.Point(12, 19);
            this.picturePanel.Margin = new System.Windows.Forms.Padding(0);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(72, 72);
            this.picturePanel.TabIndex = 3;
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Location = new System.Drawing.Point(99, 2);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(66, 13);
            this.lblImageType.TabIndex = 4;
            this.lblImageType.Text = "Image Type:";
            // 
            // lblImageDescription
            // 
            this.lblImageDescription.AutoSize = true;
            this.lblImageDescription.Location = new System.Drawing.Point(97, 49);
            this.lblImageDescription.Name = "lblImageDescription";
            this.lblImageDescription.Size = new System.Drawing.Size(63, 13);
            this.lblImageDescription.TabIndex = 5;
            this.lblImageDescription.Text = "Description:";
            // 
            // txtImageType
            // 
            this.txtImageType.Location = new System.Drawing.Point(97, 20);
            this.txtImageType.Name = "txtImageType";
            this.txtImageType.Size = new System.Drawing.Size(180, 20);
            this.txtImageType.TabIndex = 6;
            // 
            // ImageFrameEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtImageType);
            this.Controls.Add(this.lblImageDescription);
            this.Controls.Add(this.lblImageType);
            this.Controls.Add(this.picturePanel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cbImageTypes);
            this.Name = "ImageFrameEditor";
            this.Size = new System.Drawing.Size(282, 113);
            this.Enter += new System.EventHandler(this.ImageFrameEditor_Enter);
            this.Load += new System.EventHandler(this.ImageFrameEditor_Load);
            this.Leave += new System.EventHandler(this.ImageFrameEditor_Leave);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageFrameEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageFrameEditor_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.picturePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ComboBox cbImageTypes;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel picturePanel;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.Label lblImageDescription;
        private System.Windows.Forms.TextBox txtImageType;
    }
}
