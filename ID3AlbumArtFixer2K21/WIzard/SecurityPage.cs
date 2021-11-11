using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wizard.UI;
using ID3AlbumArtFixer.Properties;
using System.DirectoryServices;

namespace ID3AlbumArtFixer.Wizard
{
    internal class SecurityPage : InternalWizardPage, IAlbumArtFixerJob, IAlbumArtFixerSettings
    {
        private Panel pnlMain;
        private Panel pnlSecurity;
        private GroupBox grpAlbumArtFileSecurity;
        private ComboBox cbFullControl;
        private CheckBox chkRestrictAlbumArtAccess;
        private Label label4;
        private Label label5;
        private ComboBox cbReadOnly;
        private System.DirectoryServices.DirectoryEntry directoryEntry;
		private System.ComponentModel.IContainer components = null;

        internal SecurityPage()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

            LoadLocalUsersAndGroups();
		}

        private void LoadLocalUsersAndGroups()
        {
            directoryEntry.Path = @"WinNT://" + System.Net.Dns.GetHostName();
            BindUserAccountComboBoxes();
        }

        private void BindUserAccountComboBoxes()
        {
            string readOnlyAccount = Settings.Default.ReadOnlyAccount.ToUpper();
            string fullControlAccount = Settings.Default.FullControlAccount.ToUpper();


            foreach (DirectoryEntry child in directoryEntry.Children)
            {
                switch (child.SchemaClassName)
                {
                    case "User":
                        cbFullControl.Items.Add(new User(child.Name));
                        cbReadOnly.Items.Add(new User(child.Name));
                        break;
                    case "Group":
                        cbFullControl.Items.Add(new Group(child.Name));
                        cbReadOnly.Items.Add(new Group(child.Name));
                        break;
                }

                if (child.Name.ToUpper() == fullControlAccount)
                {
                    cbFullControl.SelectedItem = cbFullControl.Items[cbFullControl.Items.Count - 1];
                }

                if (child.Name.ToUpper() == readOnlyAccount)
                {
                    cbReadOnly.SelectedItem = cbReadOnly.Items[cbReadOnly.Items.Count - 1];
                }
            }
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
            this.pnlSecurity = new System.Windows.Forms.Panel();
            this.grpAlbumArtFileSecurity = new System.Windows.Forms.GroupBox();
            this.cbFullControl = new System.Windows.Forms.ComboBox();
            this.chkRestrictAlbumArtAccess = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbReadOnly = new System.Windows.Forms.ComboBox();
            this.directoryEntry = new System.DirectoryServices.DirectoryEntry();
            this.pnlMain.SuspendLayout();
            this.pnlSecurity.SuspendLayout();
            this.grpAlbumArtFileSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(433, 64);
            this.Banner.Subtitle = "Select security options for your album art.";
            this.Banner.Title = "Preston Media Album Art Fixer";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlSecurity);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMain.Size = new System.Drawing.Size(433, 130);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlSecurity
            // 
            this.pnlSecurity.Controls.Add(this.grpAlbumArtFileSecurity);
            this.pnlSecurity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSecurity.Location = new System.Drawing.Point(5, 5);
            this.pnlSecurity.Name = "pnlSecurity";
            this.pnlSecurity.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSecurity.Size = new System.Drawing.Size(423, 117);
            this.pnlSecurity.TabIndex = 8;
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
            this.grpAlbumArtFileSecurity.Size = new System.Drawing.Size(413, 107);
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
            this.chkRestrictAlbumArtAccess.Checked = global::ID3AlbumArtFixer.Properties.Settings.Default.SetAlbumArtSecurity;
            this.chkRestrictAlbumArtAccess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestrictAlbumArtAccess.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ID3AlbumArtFixer.Properties.Settings.Default, "SetAlbumArtSecurity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkRestrictAlbumArtAccess.Location = new System.Drawing.Point(12, 23);
            this.chkRestrictAlbumArtAccess.Name = "chkRestrictAlbumArtAccess";
            this.chkRestrictAlbumArtAccess.Size = new System.Drawing.Size(145, 17);
            this.chkRestrictAlbumArtAccess.TabIndex = 11;
            this.chkRestrictAlbumArtAccess.Text = "Restrict album art access";
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
            // SecurityPage
            // 
            this.Controls.Add(this.pnlMain);
            this.Name = "SecurityPage";
            this.Size = new System.Drawing.Size(433, 194);
            this.Load += new System.EventHandler(this.SecurityPage_Load);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.WizardPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlSecurity.ResumeLayout(false);
            this.grpAlbumArtFileSecurity.ResumeLayout(false);
            this.grpAlbumArtFileSecurity.PerformLayout();
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
            job.SetAlbumArtSecurity = chkRestrictAlbumArtAccess.Checked;
            job.FullControlAccount = ((UserAccount)cbFullControl.SelectedItem).AccountName;
            job.ReadOnlyAccount = ((UserAccount)cbReadOnly.SelectedItem).AccountName;
            return job;
        }

        #endregion

        private void chkRestrictAlbumArtAccess_CheckedChanged(object sender, EventArgs e)
        {
            SetSecurityEnabled(chkRestrictAlbumArtAccess.Checked);
        }

        private void SetSecurityEnabled(bool enabled)
        {
            cbReadOnly.Enabled = cbFullControl.Enabled = enabled;
        }

        private void SecurityPage_Load(object sender, EventArgs e)
        {
            SetSecurityEnabled(chkRestrictAlbumArtAccess.Checked);
        }

        #region IAlbumArtFixerSettings Members

        public void SetSettingsFromForm()
        {
            Settings.Default.ReadOnlyAccount = ((UserAccount)cbReadOnly.SelectedItem).AccountName;
            Settings.Default.FullControlAccount = ((UserAccount)cbFullControl.SelectedItem).AccountName;
        }

        #endregion
    }
}

