namespace Preston.Media.ID3TagEditor
{
    partial class ImageFramesEditor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageFrameEditor1 = new Preston.Media.ID3TagEditor.ImageFrameEditor();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(330, 128);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(439, 130);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(94, 23);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Visible = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(0, 130);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit Selected";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(101, 130);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(540, 130);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(94, 23);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(540, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageFrameEditor1
            // 
            this.imageFrameEditor1.AllowDrop = true;
            this.imageFrameEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageFrameEditor1.IsSelected = false;
            this.imageFrameEditor1.Location = new System.Drawing.Point(350, 10);
            this.imageFrameEditor1.Name = "imageFrameEditor1";
            this.imageFrameEditor1.PicFrame = null;
            this.imageFrameEditor1.ReadOnly = false;
            this.imageFrameEditor1.Size = new System.Drawing.Size(284, 115);
            this.imageFrameEditor1.TabIndex = 1;
            this.imageFrameEditor1.Visible = false;
            // 
            // ImageFramesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.imageFrameEditor1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Name = "ImageFramesEditor";
            this.Size = new System.Drawing.Size(643, 163);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Preston.Media.ID3TagEditor.ImageFrameEditor imageFrameEditor1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
    }
}
