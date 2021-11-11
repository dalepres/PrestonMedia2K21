namespace DPlayer
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class DPlayerForm : Form
    {
        private ToolStripMenuItem albumArtToolStripMenuItem;
        private Button btnPlay;
        private ComboBox cbPlayLists;
        private CheckBox chkRandomize;
        private IContainer components = null;
        private ToolStripMenuItem displayToolStripMenuItem;
        private Label label5;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;

        public DPlayerForm()
        {
            this.InitializeComponent();
        }

        private void albumArtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MediaPlayerAccessLayer.ShowArt();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            MediaPlayerAccessLayer.PlayPlayList((PlayListInfo) this.cbPlayLists.SelectedValue, this.chkRandomize.Checked);
        }

        private void chkRandomize_CheckedChanged(object sender, EventArgs e)
        {
            MediaPlayerAccessLayer.SetRandomize(this.chkRandomize.Checked);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DPlayer_Load(object sender, EventArgs e)
        {

            //try
            //{
            //    MessageBox.Show(MediaPlayerAccessLayer.HelloWorld());
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            TraceSwitch infoSwitch = new TraceSwitch("InfoSwitch", "General use switch");
            this.cbPlayLists.DataSource = MediaPlayerAccessLayer.Player.GetPlayListsInfo();
            this.cbPlayLists.DisplayMember = "Name";
        }

        private void DPlayerForm_Activated(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DPlayerForm));
            this.chkRandomize = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPlayLists = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.albumArtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkRandomize
            // 
            this.chkRandomize.AutoSize = true;
            this.chkRandomize.Location = new System.Drawing.Point(270, 58);
            this.chkRandomize.Name = "chkRandomize";
            this.chkRandomize.Size = new System.Drawing.Size(79, 17);
            this.chkRandomize.TabIndex = 7;
            this.chkRandomize.Text = "Randomize";
            this.chkRandomize.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(373, 52);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Existing Playlists";
            // 
            // cbPlayLists
            // 
            this.cbPlayLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayLists.FormattingEnabled = true;
            this.cbPlayLists.Location = new System.Drawing.Point(21, 54);
            this.cbPlayLists.Name = "cbPlayLists";
            this.cbPlayLists.Size = new System.Drawing.Size(218, 21);
            this.cbPlayLists.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(457, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.albumArtToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.displayToolStripMenuItem.Text = "View";
            // 
            // albumArtToolStripMenuItem
            // 
            this.albumArtToolStripMenuItem.Name = "albumArtToolStripMenuItem";
            this.albumArtToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.albumArtToolStripMenuItem.Text = "Album Art";
            this.albumArtToolStripMenuItem.Click += new System.EventHandler(this.albumArtToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // DPlayerForm
            // 
            this.ClientSize = new System.Drawing.Size(457, 85);
            this.Controls.Add(this.chkRandomize);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbPlayLists);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DPlayerForm";
            this.Text = "DPlayer";
            this.Load += new System.EventHandler(this.DPlayer_Load);
            this.Activated += new System.EventHandler(this.DPlayerForm_Activated);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new OptionsForm().ShowDialog();
        }
    }
}

