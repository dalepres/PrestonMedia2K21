namespace Preston.Media
{
    partial class ID3TagRawViewerUC
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
            this.cbFrames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteFrame = new System.Windows.Forms.Button();
            this.pnlHexView = new System.Windows.Forms.Panel();
            this.lblFrameDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbFrames
            // 
            this.cbFrames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrames.FormattingEnabled = true;
            this.cbFrames.Location = new System.Drawing.Point(47, 2);
            this.cbFrames.Name = "cbFrames";
            this.cbFrames.Size = new System.Drawing.Size(74, 21);
            this.cbFrames.TabIndex = 0;
            this.cbFrames.SelectedIndexChanged += new System.EventHandler(this.cbFrames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frames:";
            // 
            // btnDeleteFrame
            // 
            this.btnDeleteFrame.AutoSize = true;
            this.btnDeleteFrame.Location = new System.Drawing.Point(562, 0);
            this.btnDeleteFrame.Name = "btnDeleteFrame";
            this.btnDeleteFrame.Size = new System.Drawing.Size(80, 23);
            this.btnDeleteFrame.TabIndex = 2;
            this.btnDeleteFrame.Text = "Delete Frame";
            this.btnDeleteFrame.UseVisualStyleBackColor = true;
            this.btnDeleteFrame.Click += new System.EventHandler(this.btnDeleteFrame_Click);
            // 
            // pnlHexView
            // 
            this.pnlHexView.Location = new System.Drawing.Point(0, 27);
            this.pnlHexView.Name = "pnlHexView";
            this.pnlHexView.Size = new System.Drawing.Size(643, 127);
            this.pnlHexView.TabIndex = 3;
            // 
            // lblFrameDescription
            // 
            this.lblFrameDescription.Location = new System.Drawing.Point(127, 5);
            this.lblFrameDescription.Name = "lblFrameDescription";
            this.lblFrameDescription.Size = new System.Drawing.Size(429, 13);
            this.lblFrameDescription.TabIndex = 4;
            this.lblFrameDescription.Text = "lblFrameDescription";
            //this.lblFrameDescription.Resize += new System.EventHandler(this.lblFrameDescription_TextChanged);
            //this.lblFrameDescription.TextChanged += new System.EventHandler(this.lblFrameDescription_TextChanged);
            // 
            // ID3TagRawViewerUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFrameDescription);
            this.Controls.Add(this.pnlHexView);
            this.Controls.Add(this.btnDeleteFrame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFrames);
            this.Name = "ID3TagRawViewerUC";
            this.Size = new System.Drawing.Size(643, 154);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFrames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteFrame;
        private System.Windows.Forms.Panel pnlHexView;
        private System.Windows.Forms.Label lblFrameDescription;
    }
}
