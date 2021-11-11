using System.Drawing;
using System.Windows.Forms;
using System;
namespace DPlayer
{
    partial class AlbumArtForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumArtForm));
            this.ffrTimer = new System.Windows.Forms.Timer(this.components);
            this.HideButtonsTimer = new System.Windows.Forms.Timer(this.components);
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.lblAlbumNameSmall = new System.Windows.Forms.Label();
            this.lblArtistNameSmall = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDurationSmall = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.lblElapsedTimeSmall = new System.Windows.Forms.Label();
            this.lblTrackName = new System.Windows.Forms.Label();
            this.lblTrackNameSmall = new System.Windows.Forms.Label();
            this.lblArtistName = new System.Windows.Forms.Label();
            this.PlaySecondsTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlMinMaxClose = new System.Windows.Forms.Panel();
            this.usbClose = new DPlayer.UnstyledButton();
            this.usbRestore = new DPlayer.UnstyledButton();
            this.usbMinimize = new DPlayer.UnstyledButton();
            this.pnlMPControls = new System.Windows.Forms.Panel();
            this.usbNext = new DPlayer.UnstyledButton();
            this.usbPause = new DPlayer.UnstyledButton();
            this.usbPrevious = new DPlayer.UnstyledButton();
            this.usbRewind = new DPlayer.UnstyledButton();
            this.usbStop = new DPlayer.UnstyledButton();
            this.usbVolumeDown = new DPlayer.UnstyledButton();
            this.usbVolumeUp = new DPlayer.UnstyledButton();
            this.usbFastForward = new DPlayer.UnstyledButton();
            this.usbMute = new DPlayer.UnstyledButton();
            this.alarmTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkThisTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAlbumArtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAlbumNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkArtistNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkArtistNameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutsOffAtTheEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playThisAlbumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAlbumDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTrackDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlideShowTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlMinMaxClose.SuspendLayout();
            this.pnlMPControls.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ffrTimer
            // 
            this.ffrTimer.Interval = 250;
            this.ffrTimer.Tick += new System.EventHandler(this.ffrTimer_Tick);
            // 
            // HideButtonsTimer
            // 
            this.HideButtonsTimer.Interval = 5000;
            this.HideButtonsTimer.Tick += new System.EventHandler(this.HideButtonsTimer_Tick);
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbumName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAlbumName.Location = new System.Drawing.Point(330, 495);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(282, 44);
            this.lblAlbumName.TabIndex = 5;
            this.lblAlbumName.Text = "lblAlbumName";
            // 
            // lblAlbumNameSmall
            // 
            this.lblAlbumNameSmall.AutoSize = true;
            this.lblAlbumNameSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbumNameSmall.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAlbumNameSmall.Location = new System.Drawing.Point(146, 120);
            this.lblAlbumNameSmall.MaximumSize = new System.Drawing.Size(500, 40);
            this.lblAlbumNameSmall.Name = "lblAlbumNameSmall";
            this.lblAlbumNameSmall.Size = new System.Drawing.Size(0, 20);
            this.lblAlbumNameSmall.TabIndex = 9;
            this.lblAlbumNameSmall.Visible = false;
            // 
            // lblArtistNameSmall
            // 
            this.lblArtistNameSmall.AutoSize = true;
            this.lblArtistNameSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtistNameSmall.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblArtistNameSmall.Location = new System.Drawing.Point(5, 126);
            this.lblArtistNameSmall.MaximumSize = new System.Drawing.Size(500, 40);
            this.lblArtistNameSmall.Name = "lblArtistNameSmall";
            this.lblArtistNameSmall.Size = new System.Drawing.Size(0, 20);
            this.lblArtistNameSmall.TabIndex = 10;
            this.lblArtistNameSmall.Visible = false;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCurrentTime.Location = new System.Drawing.Point(812, 42);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(108, 39);
            this.lblCurrentTime.TabIndex = 23;
            this.lblCurrentTime.Text = "00:00";
            this.lblCurrentTime.Visible = false;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDuration.Location = new System.Drawing.Point(146, 109);
            this.lblDuration.MaximumSize = new System.Drawing.Size(950, 120);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(0, 39);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Visible = false;
            // 
            // lblDurationSmall
            // 
            this.lblDurationSmall.AutoSize = true;
            this.lblDurationSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDurationSmall.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDurationSmall.Location = new System.Drawing.Point(146, 124);
            this.lblDurationSmall.MaximumSize = new System.Drawing.Size(500, 40);
            this.lblDurationSmall.Name = "lblDurationSmall";
            this.lblDurationSmall.Size = new System.Drawing.Size(0, 25);
            this.lblDurationSmall.TabIndex = 12;
            this.lblDurationSmall.Visible = false;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblElapsedTime.Location = new System.Drawing.Point(146, 117);
            this.lblElapsedTime.MaximumSize = new System.Drawing.Size(950, 120);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(0, 39);
            this.lblElapsedTime.TabIndex = 21;
            this.lblElapsedTime.Visible = false;
            // 
            // lblElapsedTimeSmall
            // 
            this.lblElapsedTimeSmall.AutoSize = true;
            this.lblElapsedTimeSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTimeSmall.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblElapsedTimeSmall.Location = new System.Drawing.Point(154, 132);
            this.lblElapsedTimeSmall.MaximumSize = new System.Drawing.Size(500, 40);
            this.lblElapsedTimeSmall.Name = "lblElapsedTimeSmall";
            this.lblElapsedTimeSmall.Size = new System.Drawing.Size(0, 25);
            this.lblElapsedTimeSmall.TabIndex = 22;
            this.lblElapsedTimeSmall.Visible = false;
            // 
            // lblTrackName
            // 
            this.lblTrackName.AutoSize = true;
            this.lblTrackName.Font = new System.Drawing.Font("Microsoft Sans Serif", 64F, System.Drawing.FontStyle.Bold);
            this.lblTrackName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTrackName.Location = new System.Drawing.Point(10, 13);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(588, 97);
            this.lblTrackName.TabIndex = 0;
            this.lblTrackName.Text = "lblTrackName";
            // 
            // lblTrackNameSmall
            // 
            this.lblTrackNameSmall.AutoSize = true;
            this.lblTrackNameSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNameSmall.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTrackNameSmall.Location = new System.Drawing.Point(5, 134);
            this.lblTrackNameSmall.MaximumSize = new System.Drawing.Size(500, 80);
            this.lblTrackNameSmall.Name = "lblTrackNameSmall";
            this.lblTrackNameSmall.Size = new System.Drawing.Size(0, 25);
            this.lblTrackNameSmall.TabIndex = 11;
            this.lblTrackNameSmall.Visible = false;
            // 
            // lblArtistName
            // 
            this.lblArtistName.AutoSize = true;
            this.lblArtistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.lblArtistName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblArtistName.Location = new System.Drawing.Point(11, 100);
            this.lblArtistName.MaximumSize = new System.Drawing.Size(950, 120);
            this.lblArtistName.Name = "lblArtistName";
            this.lblArtistName.Size = new System.Drawing.Size(321, 55);
            this.lblArtistName.TabIndex = 3;
            this.lblArtistName.Text = "lblArtistName";
            // 
            // PlaySecondsTimer
            // 
            this.PlaySecondsTimer.Interval = 1000;
            this.PlaySecondsTimer.Tick += new System.EventHandler(this.PlaySecondsTimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(243, 158);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 334);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            // 
            // pnlMinMaxClose
            // 
            this.pnlMinMaxClose.Controls.Add(this.usbClose);
            this.pnlMinMaxClose.Controls.Add(this.usbRestore);
            this.pnlMinMaxClose.Controls.Add(this.usbMinimize);
            this.pnlMinMaxClose.Location = new System.Drawing.Point(799, 12);
            this.pnlMinMaxClose.Name = "pnlMinMaxClose";
            this.pnlMinMaxClose.Size = new System.Drawing.Size(119, 27);
            this.pnlMinMaxClose.TabIndex = 25;
            // 
            // usbClose
            // 
            this.usbClose.BackColor = System.Drawing.Color.Transparent;
            this.usbClose.DPlayerButtonType = DPlayer.DPlayerButtonTypes.Close;
            this.usbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbClose.Location = new System.Drawing.Point(81, 2);
            this.usbClose.Name = "usbClose";
            this.usbClose.Size = new System.Drawing.Size(33, 23);
            this.usbClose.TabIndex = 37;
            this.usbClose.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbClose.Click += new System.EventHandler(this.close_Click);
            this.usbClose.Leave += new System.EventHandler(this.button_Leave);
            this.usbClose.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbRestore
            // 
            this.usbRestore.BackColor = System.Drawing.Color.Transparent;
            this.usbRestore.DPlayerButtonType = DPlayer.DPlayerButtonTypes.Restore;
            this.usbRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbRestore.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbRestore.Location = new System.Drawing.Point(43, 2);
            this.usbRestore.Name = "usbRestore";
            this.usbRestore.Size = new System.Drawing.Size(33, 23);
            this.usbRestore.TabIndex = 36;
            this.usbRestore.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbRestore.Click += new System.EventHandler(this.restore_Click);
            this.usbRestore.Leave += new System.EventHandler(this.button_Leave);
            this.usbRestore.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbMinimize
            // 
            this.usbMinimize.BackColor = System.Drawing.Color.Transparent;
            this.usbMinimize.DPlayerButtonType = DPlayer.DPlayerButtonTypes.Minimize;
            this.usbMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbMinimize.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbMinimize.Location = new System.Drawing.Point(2, 2);
            this.usbMinimize.Name = "usbMinimize";
            this.usbMinimize.Size = new System.Drawing.Size(33, 23);
            this.usbMinimize.TabIndex = 35;
            this.usbMinimize.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbMinimize.Click += new System.EventHandler(this.minimize_Click);
            this.usbMinimize.Leave += new System.EventHandler(this.button_Leave);
            this.usbMinimize.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // pnlMPControls
            // 
            this.pnlMPControls.Controls.Add(this.usbNext);
            this.pnlMPControls.Controls.Add(this.usbPause);
            this.pnlMPControls.Controls.Add(this.usbPrevious);
            this.pnlMPControls.Controls.Add(this.usbRewind);
            this.pnlMPControls.Controls.Add(this.usbStop);
            this.pnlMPControls.Controls.Add(this.usbVolumeDown);
            this.pnlMPControls.Controls.Add(this.usbVolumeUp);
            this.pnlMPControls.Controls.Add(this.usbFastForward);
            this.pnlMPControls.Controls.Add(this.usbMute);
            this.pnlMPControls.Location = new System.Drawing.Point(264, 543);
            this.pnlMPControls.Name = "pnlMPControls";
            this.pnlMPControls.Size = new System.Drawing.Size(402, 27);
            this.pnlMPControls.TabIndex = 24;
            // 
            // usbNext
            // 
            this.usbNext.BackColor = System.Drawing.Color.Transparent;
            this.usbNext.DPlayerButtonType = DPlayer.DPlayerButtonTypes.EndOfTrack;
            this.usbNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbNext.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbNext.Location = new System.Drawing.Point(207, 2);
            this.usbNext.Name = "usbNext";
            this.usbNext.Size = new System.Drawing.Size(33, 23);
            this.usbNext.TabIndex = 34;
            this.usbNext.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbNext.Click += new System.EventHandler(this.next_Click);
            this.usbNext.Leave += new System.EventHandler(this.button_Leave);
            this.usbNext.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbPause
            // 
            this.usbPause.BackColor = System.Drawing.Color.Transparent;
            this.usbPause.DPlayerButtonType = DPlayer.DPlayerButtonTypes.Pause;
            this.usbPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbPause.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbPause.Location = new System.Drawing.Point(125, 2);
            this.usbPause.Name = "usbPause";
            this.usbPause.Size = new System.Drawing.Size(33, 23);
            this.usbPause.TabIndex = 35;
            this.usbPause.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbPause.Click += new System.EventHandler(this.pause_Click);
            this.usbPause.Leave += new System.EventHandler(this.button_Leave);
            this.usbPause.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbPrevious
            // 
            this.usbPrevious.BackColor = System.Drawing.Color.Transparent;
            this.usbPrevious.DPlayerButtonType = DPlayer.DPlayerButtonTypes.BeginningOfTrack;
            this.usbPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbPrevious.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbPrevious.Location = new System.Drawing.Point(2, 2);
            this.usbPrevious.Name = "usbPrevious";
            this.usbPrevious.Size = new System.Drawing.Size(33, 23);
            this.usbPrevious.TabIndex = 29;
            this.usbPrevious.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbPrevious.Click += new System.EventHandler(this.previous_Click);
            this.usbPrevious.Leave += new System.EventHandler(this.button_Leave);
            this.usbPrevious.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbRewind
            // 
            this.usbRewind.BackColor = System.Drawing.Color.Transparent;
            this.usbRewind.DPlayerButtonType = DPlayer.DPlayerButtonTypes.FastRewind;
            this.usbRewind.Enabled = false;
            this.usbRewind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbRewind.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbRewind.Location = new System.Drawing.Point(43, 2);
            this.usbRewind.Name = "usbRewind";
            this.usbRewind.Size = new System.Drawing.Size(33, 23);
            this.usbRewind.TabIndex = 28;
            this.usbRewind.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbRewind.Click += new System.EventHandler(this.rewind_Click);
            this.usbRewind.Leave += new System.EventHandler(this.button_Leave);
            this.usbRewind.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbStop
            // 
            this.usbStop.BackColor = System.Drawing.Color.Transparent;
            this.usbStop.DPlayerButtonType = DPlayer.DPlayerButtonTypes.Stop;
            this.usbStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbStop.Location = new System.Drawing.Point(84, 2);
            this.usbStop.Name = "usbStop";
            this.usbStop.Size = new System.Drawing.Size(33, 23);
            this.usbStop.TabIndex = 33;
            this.usbStop.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbStop.Click += new System.EventHandler(this.stop_Click);
            this.usbStop.Leave += new System.EventHandler(this.button_Leave);
            this.usbStop.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbVolumeDown
            // 
            this.usbVolumeDown.BackColor = System.Drawing.Color.Transparent;
            this.usbVolumeDown.DPlayerButtonType = DPlayer.DPlayerButtonTypes.VolumeDown;
            this.usbVolumeDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbVolumeDown.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbVolumeDown.Location = new System.Drawing.Point(326, 2);
            this.usbVolumeDown.Name = "usbVolumeDown";
            this.usbVolumeDown.Size = new System.Drawing.Size(33, 23);
            this.usbVolumeDown.TabIndex = 37;
            this.usbVolumeDown.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbVolumeDown.Click += new System.EventHandler(this.volumeDown_Click);
            this.usbVolumeDown.Leave += new System.EventHandler(this.button_Leave);
            this.usbVolumeDown.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbVolumeUp
            // 
            this.usbVolumeUp.BackColor = System.Drawing.Color.Transparent;
            this.usbVolumeUp.DPlayerButtonType = DPlayer.DPlayerButtonTypes.VolumeUp;
            this.usbVolumeUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbVolumeUp.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbVolumeUp.Location = new System.Drawing.Point(367, 2);
            this.usbVolumeUp.Name = "usbVolumeUp";
            this.usbVolumeUp.Size = new System.Drawing.Size(33, 23);
            this.usbVolumeUp.TabIndex = 38;
            this.usbVolumeUp.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbVolumeUp.Click += new System.EventHandler(this.volumeUp_Click);
            this.usbVolumeUp.Leave += new System.EventHandler(this.button_Leave);
            this.usbVolumeUp.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbFastForward
            // 
            this.usbFastForward.BackColor = System.Drawing.Color.Transparent;
            this.usbFastForward.DPlayerButtonType = DPlayer.DPlayerButtonTypes.FastForward;
            this.usbFastForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbFastForward.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbFastForward.Location = new System.Drawing.Point(166, 2);
            this.usbFastForward.Name = "usbFastForward";
            this.usbFastForward.Size = new System.Drawing.Size(33, 23);
            this.usbFastForward.TabIndex = 31;
            this.usbFastForward.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbFastForward.Click += new System.EventHandler(this.fastForward_Click);
            this.usbFastForward.Leave += new System.EventHandler(this.button_Leave);
            this.usbFastForward.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // usbMute
            // 
            this.usbMute.BackColor = System.Drawing.Color.Transparent;
            this.usbMute.DPlayerButtonType = DPlayer.DPlayerButtonTypes.SpeakerUnmuted;
            this.usbMute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbMute.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.usbMute.Location = new System.Drawing.Point(285, 2);
            this.usbMute.Name = "usbMute";
            this.usbMute.Size = new System.Drawing.Size(33, 23);
            this.usbMute.TabIndex = 36;
            this.usbMute.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.usbMute.Click += new System.EventHandler(this.mute_Click);
            this.usbMute.Leave += new System.EventHandler(this.button_Leave);
            this.usbMute.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            // 
            // alarmTimer
            // 
            this.alarmTimer.Interval = 60000;
            this.alarmTimer.Tick += new System.EventHandler(this.alarmTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkThisTrackToolStripMenuItem,
            this.playThisAlbumToolStripMenuItem,
            this.viewAlbumDetailsToolStripMenuItem,
            this.viewTrackDetailsToolStripMenuItem,
            this.showHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 114);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            // 
            // checkThisTrackToolStripMenuItem
            // 
            this.checkThisTrackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAlbumArtToolStripMenuItem,
            this.checkAlbumNameToolStripMenuItem,
            this.checkArtistNameToolStripMenuItem,
            this.checkArtistNameToolStripMenuItem1,
            this.checkFileNameToolStripMenuItem,
            this.cutsOffAtTheEndToolStripMenuItem,
            this.skipsToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.checkThisTrackToolStripMenuItem.Name = "checkThisTrackToolStripMenuItem";
            this.checkThisTrackToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.checkThisTrackToolStripMenuItem.Text = "Check This Track";
            // 
            // checkAlbumArtToolStripMenuItem
            // 
            this.checkAlbumArtToolStripMenuItem.Enabled = false;
            this.checkAlbumArtToolStripMenuItem.Name = "checkAlbumArtToolStripMenuItem";
            this.checkAlbumArtToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkAlbumArtToolStripMenuItem.Text = "Check Album Art";
            // 
            // checkAlbumNameToolStripMenuItem
            // 
            this.checkAlbumNameToolStripMenuItem.Enabled = false;
            this.checkAlbumNameToolStripMenuItem.Name = "checkAlbumNameToolStripMenuItem";
            this.checkAlbumNameToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkAlbumNameToolStripMenuItem.Text = "Check Album Name";
            // 
            // checkArtistNameToolStripMenuItem
            // 
            this.checkArtistNameToolStripMenuItem.Enabled = false;
            this.checkArtistNameToolStripMenuItem.Name = "checkArtistNameToolStripMenuItem";
            this.checkArtistNameToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkArtistNameToolStripMenuItem.Text = "Check Artist Name";
            // 
            // checkArtistNameToolStripMenuItem1
            // 
            this.checkArtistNameToolStripMenuItem1.Enabled = false;
            this.checkArtistNameToolStripMenuItem1.Name = "checkArtistNameToolStripMenuItem1";
            this.checkArtistNameToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.checkArtistNameToolStripMenuItem1.Text = "Check Artist Name";
            // 
            // checkFileNameToolStripMenuItem
            // 
            this.checkFileNameToolStripMenuItem.Enabled = false;
            this.checkFileNameToolStripMenuItem.Name = "checkFileNameToolStripMenuItem";
            this.checkFileNameToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkFileNameToolStripMenuItem.Text = "Check File Name";
            // 
            // cutsOffAtTheEndToolStripMenuItem
            // 
            this.cutsOffAtTheEndToolStripMenuItem.Enabled = false;
            this.cutsOffAtTheEndToolStripMenuItem.Name = "cutsOffAtTheEndToolStripMenuItem";
            this.cutsOffAtTheEndToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.cutsOffAtTheEndToolStripMenuItem.Text = "Cuts Off at the End";
            // 
            // skipsToolStripMenuItem
            // 
            this.skipsToolStripMenuItem.Enabled = false;
            this.skipsToolStripMenuItem.Name = "skipsToolStripMenuItem";
            this.skipsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.skipsToolStripMenuItem.Text = "Skips";
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.otherToolStripMenuItem.Text = "Other";
            this.otherToolStripMenuItem.ToolTipText = "Currently the only option.  Choose this to log issues with this track to look up " +
                "later.";
            this.otherToolStripMenuItem.Click += new System.EventHandler(this.otherToolStripMenuItem_Click);
            // 
            // playThisAlbumToolStripMenuItem
            // 
            this.playThisAlbumToolStripMenuItem.Enabled = false;
            this.playThisAlbumToolStripMenuItem.Name = "playThisAlbumToolStripMenuItem";
            this.playThisAlbumToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.playThisAlbumToolStripMenuItem.Text = "Play This Album";
            // 
            // viewAlbumDetailsToolStripMenuItem
            // 
            this.viewAlbumDetailsToolStripMenuItem.Enabled = false;
            this.viewAlbumDetailsToolStripMenuItem.Name = "viewAlbumDetailsToolStripMenuItem";
            this.viewAlbumDetailsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.viewAlbumDetailsToolStripMenuItem.Text = "View Album Details";
            // 
            // viewTrackDetailsToolStripMenuItem
            // 
            this.viewTrackDetailsToolStripMenuItem.Enabled = false;
            this.viewTrackDetailsToolStripMenuItem.Name = "viewTrackDetailsToolStripMenuItem";
            this.viewTrackDetailsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.viewTrackDetailsToolStripMenuItem.Text = "View Track Details";
            // 
            // showHistoryToolStripMenuItem
            // 
            this.showHistoryToolStripMenuItem.Enabled = false;
            this.showHistoryToolStripMenuItem.Name = "showHistoryToolStripMenuItem";
            this.showHistoryToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.showHistoryToolStripMenuItem.Text = "Show History";
            // 
            // SlideShowTimer
            // 
            this.SlideShowTimer.Interval = 5000;
            this.SlideShowTimer.Tick += new System.EventHandler(this.SlideShowTimer_Tick);
            // 
            // AlbumArtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(935, 622);
            this.Controls.Add(this.lblTrackNameSmall);
            this.Controls.Add(this.lblArtistName);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.pnlMinMaxClose);
            this.Controls.Add(this.lblElapsedTimeSmall);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.pnlMPControls);
            this.Controls.Add(this.lblDurationSmall);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblArtistNameSmall);
            this.Controls.Add(this.lblAlbumNameSmall);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.lblAlbumName);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlbumArtForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Album Art - DPLayer";
            this.Deactivate += new System.EventHandler(this.AlbumArtForm_Deactivate);
            this.Load += new System.EventHandler(this.AlbumArtForm_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AlbumArtForm_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.MouseLeave += new System.EventHandler(this.AlbumArtForm_MouseLeave);
            this.Resize += new System.EventHandler(this.AlbumArtForm_Resize);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlbumArtForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlMinMaxClose.ResumeLayout(false);
            this.pnlMPControls.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer ffrTimer;
        private System.Windows.Forms.Timer HideButtonsTimer;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.Label lblAlbumNameSmall;
        private System.Windows.Forms.Label lblArtistNameSmall;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDurationSmall;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label lblElapsedTimeSmall;
        private System.Windows.Forms.Label lblTrackName;
        private System.Windows.Forms.Label lblTrackNameSmall;
        private System.Windows.Forms.Label lblArtistName;
        private System.Windows.Forms.Timer PlaySecondsTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlMinMaxClose;
        private System.Windows.Forms.Panel pnlMPControls;
        private UnstyledButton usbClose;
        private UnstyledButton usbRestore;
        private UnstyledButton usbMinimize;
        private UnstyledButton usbPause;
        private UnstyledButton usbPrevious;
        private UnstyledButton usbRewind;
        private UnstyledButton usbStop;
        private UnstyledButton usbVolumeDown;
        private UnstyledButton usbVolumeUp;
        private UnstyledButton usbFastForward;
        private UnstyledButton usbMute;
        private UnstyledButton usbNext;
        private System.Windows.Forms.Timer alarmTimer;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem checkThisTrackToolStripMenuItem;
        private ToolStripMenuItem playThisAlbumToolStripMenuItem;
        private ToolStripMenuItem viewAlbumDetailsToolStripMenuItem;
        private ToolStripMenuItem viewTrackDetailsToolStripMenuItem;
        private ToolStripMenuItem showHistoryToolStripMenuItem;
        private ToolStripMenuItem checkAlbumArtToolStripMenuItem;
        private ToolStripMenuItem checkAlbumNameToolStripMenuItem;
        private ToolStripMenuItem checkArtistNameToolStripMenuItem;
        private ToolStripMenuItem checkArtistNameToolStripMenuItem1;
        private ToolStripMenuItem checkFileNameToolStripMenuItem;
        private ToolStripMenuItem cutsOffAtTheEndToolStripMenuItem;
        private ToolStripMenuItem skipsToolStripMenuItem;
        private ToolStripMenuItem otherToolStripMenuItem;
        private Timer SlideShowTimer;
    }
}