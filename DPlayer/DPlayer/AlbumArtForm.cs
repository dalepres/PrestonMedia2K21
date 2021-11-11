using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMPLib;
using Microsoft.Win32;
using ID3Lib;
using System.IO;
using DPlayer.Properties;
using System.Diagnostics;

namespace DPlayer
{
    public partial class AlbumArtForm : Form
    {
        public delegate void Display(TrackInfo trackInfo);
        public Display DisplayNextTrack;
        public delegate void LastPlayedDelegate(string title);
        public delegate void UpdateDurationDelegate(double duration, string durationString, string url);

        private bool alarming;
        private DateTime alarmTime;
        private bool ButtonsAreVisible;
        private DateTime initialAlarmTime;
        private bool isButtonDown;
        private bool isMaximized;
        private bool isMinimized;
        private UnstyledProgressBar progress;
        private string[] slides;
        private bool isSlideShowPicture = false;
        private bool useSlideShow = false;
        private string currentTrackImagePath;
        private Point smallFormStartLocation;
        private Point MouseXY;
        private object origScreenSaveSetting;
        private IWMPMedia preAlarmMediaItem;
        private IWMPPlaylist preAlarmPlayList;
        private double preAlarmPosition;
        private int preAlarmVolume;
        private RegistryKey regkeyScreenSaver;
        public LastPlayedDelegate SetLastPlayed;
        private bool snoozing;
        public UpdateDurationDelegate StartDurationCounter;
        private Point startPoint;
        private TrackDirection trackDirection;
        private ID3Lib.TrackInfo trackInfo;
        public event MediaControlEventHandler MediaControlClicked;

        public AlbumArtForm()
        {
            smallFormStartLocation = GetSmallFormStartLocation();
            this.isButtonDown = false;
            this.ButtonsAreVisible = false;
            this.isMaximized = false;
            this.isMinimized = false;
            this.trackDirection = TrackDirection.Forward;
            this.InitializeComponent();
            this.DisplayNextTrack += new Display(this.SetTrackInfo);
            this.StartDurationCounter += new UpdateDurationDelegate(this.SetDuration);
            this.SetLastPlayed += new LastPlayedDelegate(this.DisplayLastPlayed);
            MediaPlayerAccessLayer.Player.PlayStateChanged += new PlayStateChangedEventHandler(this.Player_PlayStateChanged);
        }


        private void Alarm()
        {
            if (!this.alarming)
            {
                this.alarming = true;
                this.alarmTimer.Enabled = true;
                this.preAlarmVolume = MediaPlayerAccessLayer.GetPlayerVolume();
                this.preAlarmMediaItem = MediaPlayerAccessLayer.Player.Player.currentMedia;
                this.preAlarmPlayList = MediaPlayerAccessLayer.Player.Player.currentPlaylist;
                this.preAlarmPosition = MediaPlayerAccessLayer.Player.GetCurrentPosition();
                MediaPlayerAccessLayer.Player.Play(Settings.Default.AlarmTrack);
                this.alarmTimer_Tick(null, null);
                this.snoozing = false;
            }
        }

        private void InitializedUnstyledControls()
        {
            this.progress = new UnstyledProgressBar();
            this.SuspendLayout();
            this.progress.Location = new Point(10, 200);
            this.progress.BackColor = System.Drawing.Color.Black;
            this.progress.ForeColor = SystemColors.ButtonFace;
            this.progress.BorderColor = SystemColors.ButtonFace;
            this.progress.Height = 12;
            this.Controls.Add(this.progress);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void OnMouseEvent(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
            {
                if (!this.isButtonDown)
                {
                    this.startPoint = new Point(e.X, e.Y);
                }
                this.isButtonDown = true;
            }
            if (this.isButtonDown && (this.startPoint != e.Location))
            {
                this.isButtonDown = false;
                this.DragForm(ref this.startPoint, e.Location);
            }
            if (!(this.MouseXY.IsEmpty || ((this.MouseXY.Equals(new Point(e.X, e.Y)) && (e.Clicks <= 0)) && (e.Delta <= 0))))
            {
                if (!this.ButtonsAreVisible)
                {
                    this.ShowButtons();
                }
                this.HideButtonsTimer.Stop();
                this.HideButtonsTimer.Start();
            }
            this.MouseXY = new Point(e.X, e.Y);
        }

        private void HideButtons()
        {
            this.HideCursor();
            this.pnlMPControls.Visible = false;
            this.pnlMinMaxClose.Visible = false;
            this.ButtonsAreVisible = false;
        }

        private void HideButtonsTimer_Tick(object sender, EventArgs e)
        {
            this.HideButtons();
        }

        private void HideCursor()
        {
        }

        private void DragForm(ref Point startPoint, Point newPoint)
        {
            if (!this.isMaximized)
            {
                int diffX = startPoint.X - newPoint.X;
                int diffY = startPoint.Y - newPoint.Y;
                int newLeft = this.Left - diffX;
                int newTop = this.Top - diffY;
                this.Left = newLeft;
                this.Top = newTop;
                startPoint.X -= diffX;
                startPoint.Y -= diffY;
                this.isButtonDown = false;
                smallFormStartLocation = new Point(this.Left, this.Top);
            }
        }

        private Point GetSmallFormStartLocation()
        {
            Point center = GetScreenCenterPoint();
            return new Point(center.X - 300, center.Y - 200);
        }

        private Point GetScreenCenterPoint()
        {
            Screen screen = Screen.FromControl(this);
            return new Point(screen.Bounds.Width / 2, screen.Bounds.Height / 2);
        }

        private void fastForward_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("FastForward");
            this.usbPause.DPlayerButtonType = DPlayerButtonTypes.Play;
        }

        private void ffrTimer_Tick(object sender, EventArgs e)
        {
            if (this.trackDirection == TrackDirection.Forward)
            {
                this.OnMediaControlClicked("FastForward");
            }
            else
            {
                this.OnMediaControlClicked("Rewind");
            }
        }


        private void AlarmOff()
        {
            this.StopAlarm();
        }

        private void alarmTimer_Tick(object sender, EventArgs e)
        {
            MediaPlayerAccessLayer.VolumeUp(Settings.Default.AlarmVolumeStepUp);
        }

        private void AlbumArtForm_Deactivate(object sender, EventArgs e)
        {
            this.ShowCursor();
        }

        private void AlbumArtForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.alarming)
            {
                this.Snooze();
            }
            else
            {
                Button button = null;
                UnstyledButton usb = null;
                this.HideButtonsTimer.Stop();
                this.ShowButtons();
                switch (e.KeyCode)
                {
                    case Keys.F8:
                        usb = this.usbMute;
                        break;

                    case Keys.F9:
                        usb = this.usbVolumeDown;
                        break;

                    case Keys.F10:
                        usb = this.usbVolumeUp;
                        break;

                    case Keys.F11:
                        usb = this.usbRestore;
                        break;

                    case Keys.S:
                        usb = this.usbStop;
                        break;

                    case Keys.P:
                        usb = this.usbPause;
                        break;

                    case Keys.B:
                        usb = this.usbPrevious;
                        break;

                    case Keys.F:
                        usb = this.usbNext;
                        break;
                }
                if (button != null)
                {
                    button.Focus();
                    button.PerformClick();
                }
                if (usb != null)
                {
                    usb.Focus();
                    usb.PerformClick();
                }
                this.HideButtonsTimer.Start();
                e.Handled = true;
            }
        }

        private void AlbumArtForm_MouseLeave(object sender, EventArgs e)
        {
            this.ShowCursor();
        }

        private void AlbumArtForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.isButtonDown = false;
            if (this.alarming || this.snoozing)
            {
                if (this.alarming)
                {
                    this.RestoreFromAlarm();
                }
                this.AlarmOff();
            }
        }

        private void AlbumArtForm_Resize(object sender, EventArgs e)
        {
            if (this.isMinimized)
            {
                if (this.isMaximized)
                {
                    this.MaximizeForm();
                }
                else
                {
                    this.RestoreForm();
                }
            }
            this.isMinimized = this.WindowState == FormWindowState.Minimized;
        }

        private void button_Leave(object sender, EventArgs e)
        {
            this.HideButtonsTimer.Stop();
            this.HideButtonsTimer.Start();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            this.HideButtonsTimer.Stop();
            ((Control)sender).Focus();
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            this.HideButtonsTimer.Start();
        }

        private void CheckAlarm()
        {
            if ((this.alarmTime != DateTime.MinValue) && this.IsAlarmTime())
            {
                this.Alarm();
            }
        }

        private void PlaySecondsTimer_Tick(object sender, EventArgs e)
        {
            int currentPosition = (int)MediaPlayerAccessLayer.GetCurrentPosition();
            if (currentPosition < this.progress.Maximum)
            {
                this.progress.Value = currentPosition;
                string currentPositionString = MediaPlayerAccessLayer.GetCurrentPositionString();
                if (currentPositionString.StartsWith("00"))
                {
                    currentPositionString = currentPositionString.Substring(1);
                }
                if (currentPositionString.StartsWith(":"))
                {
                    currentPositionString = "0" + currentPositionString;
                }
                this.lblElapsedTime.Text = this.lblElapsedTimeSmall.Text = currentPositionString;
                this.lblElapsedTimeSmall.Left = this.progress.Left - (this.lblElapsedTimeSmall.Width + 10);
                this.lblElapsedTime.Left = this.progress.Left - (this.lblElapsedTime.Width + 10);
            }
            else
            {
                this.progress.Value = this.progress.Maximum;
            }
            this.lblCurrentTime.Text = DateTime.Now.ToString("h:mm tt");
            this.CheckAlarm();
            this.lblCurrentTime.Left = this.Width - (this.lblCurrentTime.Width + 10);
        }

        private void previous_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("Previous");
        }

        private void restore_Click(object sender, EventArgs e)
        {
            if (this.isMaximized)
            {
                this.RestoreForm();
            }
            else
            {
                this.MaximizeForm();
            }
        }

        private void ShowCursor()
        {
        }

        private void Snooze()
        {
            this.alarmTimer.Enabled = false;
            this.alarming = false;
            this.snoozing = true;
            this.alarmTime = DateTime.Now.AddMinutes(Settings.Default.SnoozeTime);
            this.RestoreFromAlarm();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("Stop");
        }

        private void StopAlarm()
        {
            DateTime now = DateTime.Now;
            this.alarmTimer.Enabled = false;
            if (now.TimeOfDay < this.initialAlarmTime.TimeOfDay)
            {
                this.alarmTime = new DateTime(now.Year, now.Month, now.Day, this.initialAlarmTime.Hour, this.initialAlarmTime.Minute, this.initialAlarmTime.Second);
            }
            else
            {
                this.alarmTime = new DateTime(now.Year, now.Month, now.Day, this.initialAlarmTime.Hour, this.initialAlarmTime.Minute, this.initialAlarmTime.Second);
                this.alarmTime.AddDays(1.0);
            }
            this.alarming = false;
            this.snoozing = false;
        }

        private void volumeDown_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("VolumeDown");
            this.usbMute.DPlayerButtonType = DPlayerButtonTypes.SpeakerUnmuted;
        }

        private void volumeUp_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("VolumeUp");
            this.usbMute.DPlayerButtonType = DPlayerButtonTypes.SpeakerUnmuted;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }

        private void CloseForm()
        {
            this.ShowCursor();
            this.Hide();
        }

        private void MaximizeForm()
        {
            this.isMaximized = true;
            this.Bounds = Screen.FromControl(this).Bounds;
            this.TopMost = true;
            this.Focus();
            this.usbRestore.DPlayerButtonType = DPlayerButtonTypes.Restore;
            this.lblTrackName.Visible = true;
            this.lblDuration.Visible = true;
            this.lblAlbumName.Visible = true;
            this.lblArtistName.Visible = true;
            this.lblCurrentTime.Visible = true;
            this.lblTrackNameSmall.Visible = false;
            this.lblDurationSmall.Visible = false;
            this.lblArtistNameSmall.Visible = false;
            this.lblAlbumNameSmall.Visible = false;
            this.lblElapsedTimeSmall.Visible = false;
            this.pnlMinMaxClose.Top = 0;
            this.lblTrackName.Top = this.pnlMinMaxClose.Top;
            this.lblCurrentTime.Top = this.pnlMinMaxClose.Bottom;
            this.pnlMinMaxClose.Left = this.Width - this.pnlMinMaxClose.Width;
            this.lblCurrentTime.Left = this.Width - (this.lblCurrentTime.Width + 10);
            this.lblTrackName.MaximumSize = new Size(this.lblCurrentTime.Left - 20, 0);
            this.lblArtistName.Top = this.lblTrackName.Bottom;
            this.lblAlbumName.Left = (this.Width / 2) - (this.lblAlbumName.Width / 2);
            this.progress.Value = 0;
            this.lblDuration.Top = this.Height - this.lblDuration.Height;
            this.progress.Height = 0x10;
            this.progress.Width = (int)(this.Width * 0.7);
            this.progress.Left = (this.Width / 2) - (this.progress.Width / 2);
            this.lblDuration.Left = (this.progress.Left + this.progress.Width) + 10;
            this.lblElapsedTime.Visible = true;
            this.lblElapsedTime.Left = this.progress.Left - (this.lblElapsedTime.Width + 10);
            this.lblElapsedTime.Top = this.lblDuration.Top;
            this.progress.Top = (this.lblDuration.Top + (this.lblDuration.Height / 2)) - (this.progress.Height / 2);
            this.pnlMPControls.Top = this.progress.Top - (this.pnlMPControls.Height + 5);
            this.pnlMPControls.Left = (this.progress.Left + (this.progress.Width / 2)) - (this.pnlMPControls.Width / 2);
            this.lblAlbumName.Top = pnlMPControls.Top - lblAlbumName.Height; // this.pictureBox1.Top + this.pictureBox1.Height;
            this.lblAlbumName.Left = (this.Width / 2) - (this.lblAlbumName.Width / 2);
            this.SetPictureLocation();
        }

        private void RestoreForm()
        {
            this.Height = 400;
            this.Width = 600;
            this.Location = smallFormStartLocation;
            this.ShowCursor();
            this.isMaximized = false;
            this.TopMost = false;
            this.lblTrackName.Visible = false;
            this.lblDuration.Visible = false;
            this.lblAlbumName.Visible = false;
            this.lblArtistName.Visible = false;
            this.lblCurrentTime.Visible = false;
            this.lblTrackNameSmall.Visible = true;
            this.lblDurationSmall.Visible = true;
            this.lblArtistNameSmall.Visible = true;
            this.lblAlbumNameSmall.Visible = true;
            this.lblElapsedTime.Visible = false;
            this.usbRestore.DPlayerButtonType = DPlayerButtonTypes.Maximize;
            this.pictureBox1.Size = new Size(200, 200);
            this.SetPictureLocation();
            this.pnlMinMaxClose.Top = 5;
            this.pnlMinMaxClose.Left = this.Width - (this.pnlMinMaxClose.Width + 5);
            this.progress.Value = 0;
            this.progress.Height = 12;
            this.progress.Width = (int)(this.Width * 0.7);
            this.progress.Left = (this.Width / 2) - (this.progress.Width / 2);
            this.lblDurationSmall.Top = this.Height - this.lblDurationSmall.Height;
            this.lblDurationSmall.Left = this.progress.Right + 10;
            this.lblElapsedTimeSmall.Visible = true;
            this.lblElapsedTimeSmall.Left = this.progress.Left - (this.lblElapsedTimeSmall.Width + 10);
            this.lblElapsedTimeSmall.Top = this.lblDurationSmall.Top;
            this.progress.Top = (this.lblDurationSmall.Top + (this.lblDurationSmall.Height / 2)) - (this.progress.Height / 2);
            this.pnlMPControls.Left = (this.progress.Left + (this.progress.Width / 2)) - (this.pnlMPControls.Width / 2);
            this.pnlMPControls.Top = this.progress.Top - (this.pnlMPControls.Height + 5);
        }


        private void SetPictureLocation()
        {
            if (this.pictureBox1.Image != null)
            {
                if (this.isMaximized)
                {
                    int availableHeight = lblAlbumName.Top - lblArtistName.Bottom;  //(this.pnlMPControls.Top - this.lblArtistName.Bottom) - this.lblAlbumName.Height;
                    int pictureSize = this.GetPictureSize(availableHeight);
                    this.pictureBox1.Size = new Size(pictureSize, pictureSize);
                    this.pictureBox1.Left = (this.Width / 2) - (pictureSize / 2);
                    this.pictureBox1.Top = this.lblArtistName.Bottom + ((availableHeight - this.pictureBox1.Height) / 2);
                    //this.lblAlbumName.Top = pnlMPControls.Top - lblAlbumName.Height; // this.pictureBox1.Top + this.pictureBox1.Height;
                    //this.lblAlbumName.Left = (this.Width / 2) - (this.lblAlbumName.Width / 2);
                }
                else
                {
                    this.pictureBox1.Left = (this.Width / 2) - 100;
                    this.pictureBox1.Top = (this.lblArtistNameSmall.Top + this.lblArtistNameSmall.Height) + 5;
                    this.lblAlbumNameSmall.Top = this.pictureBox1.Top + this.pictureBox1.Height;
                    this.lblAlbumNameSmall.Left = (this.Width / 2) - (this.lblAlbumNameSmall.Width / 2);
                }
            }

            pictureBox1.Visible = true;
        }

        private int GetPictureSize(int availableHeight)
        {
            return Math.Min(Math.Max(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height), availableHeight);
        }

        private void DisplayTrackInfo(ID3Lib.TrackInfo trackInfo)
        {
            try
            {
                this.PlaySecondsTimer.Enabled = false;
                if (trackInfo != null)
                {
                    this.lblDuration.Text = this.lblDurationSmall.Text = "00:00";
                    this.lblTrackName.Text = this.lblTrackNameSmall.Text = trackInfo.TrackName.Replace("&", "&&");
                    this.lblArtistName.Text = this.lblArtistNameSmall.Text = trackInfo.ArtistName.Replace("&", "&&");
                    this.lblAlbumName.Text = this.lblAlbumNameSmall.Text = trackInfo.AlbumName.Replace("&", "&&");
                    this.lblArtistName.Top = this.lblTrackName.Bottom;
                    this.lblTrackNameSmall.Top = 5;
                    this.lblArtistNameSmall.Top = (this.lblTrackNameSmall.Top + this.lblTrackNameSmall.Height) + 5;
                    if (!string.IsNullOrEmpty(Path.Combine(Path.GetDirectoryName(trackInfo.FilePath), "Folder.jpg")))
                    {
                        this.pictureBox1.Load(Path.Combine(Path.GetDirectoryName(trackInfo.FilePath), "Folder.jpg"));
                        currentTrackImagePath = pictureBox1.ImageLocation;
                        isSlideShowPicture = false;
                    }
                    if (this.isMaximized)
                    {
                        this.MaximizeForm();
                    }
                    else
                    {
                        this.RestoreForm();
                    }
                }

                if (useSlideShow)
                {
                    SlideShowTimer.Enabled = true;
                }
            }
            catch
            {
                this.CloseForm();
            }

        }

        public void SetTrackInfo(ID3Lib.TrackInfo trackInfo)
        {
            this.TrackInfo = trackInfo;
            if (!this.Visible)
            {
                this.Show();
            }
        }

        private void DisplayLastPlayed(string title)
        {
        }

        private enum TrackDirection
        {
            Forward,
            Rewind
        }


        public ID3Lib.TrackInfo TrackInfo
        {
            get
            {
                return this.trackInfo;
            }
            set
            {
                try
                {
                    this.trackInfo = value;
                    this.DisplayTrackInfo(this.trackInfo);
                }
                catch
                {
                    this.CloseForm();
                }
            }
        }

        private void RestoreFromAlarm()
        {
            MediaPlayerAccessLayer.Player.Player.currentPlaylist = this.preAlarmPlayList;
            MediaPlayerAccessLayer.Player.Player.controls.playItem(this.preAlarmMediaItem);
            MediaPlayerAccessLayer.SetPlayerVolume(this.preAlarmVolume);
        }

        private void rewind_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("Rewind");
            this.usbPause.DPlayerButtonType = DPlayerButtonTypes.Play;
        }

        public void SetDuration(double duration, string durationString, string url)
        {
            if (durationString.StartsWith("00"))
            {
                durationString = durationString.Substring(1);
            }
            this.lblDuration.Text = this.lblDurationSmall.Text = durationString;
            this.lblDuration.Left = (this.progress.Left + this.progress.Width) + 10;
            this.lblDurationSmall.Left = (this.progress.Left + this.progress.Width) + 10;
            this.progress.Maximum = (int)duration;
            this.PlaySecondsTimer.Enabled = true;
        }

        private void ShowButtons()
        {
            this.ShowCursor();
            this.pnlMPControls.Visible = true;
            this.pnlMinMaxClose.Visible = true;
            this.ButtonsAreVisible = true;
        }

        private void pause_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked(((UnstyledButton)sender).DPlayerButtonType.ToString());
        }

        private void Player_PlayStateChanged(object args)
        {
            if (useSlideShow)
            {
                SlideShowTimer.Enabled = false;
            }

            switch (((WMPPlayState)args))
            {
                case WMPPlayState.wmppsStopped:
                case WMPPlayState.wmppsPaused:
                    this.usbPause.DPlayerButtonType = DPlayerButtonTypes.Play;
                    break;

                case WMPPlayState.wmppsPlaying:
                    this.usbPause.DPlayerButtonType = DPlayerButtonTypes.Pause;
                    break;
            }
            this.usbPause.Invalidate();
        }

        private bool IsAlarmTime()
        {
            TimeSpan ts = DateTime.Now.TimeOfDay.Subtract(this.alarmTime.TimeOfDay);
            return ((DateTime.Now.TimeOfDay > this.alarmTime.TimeOfDay) && (DateTime.Now.TimeOfDay.Subtract(this.alarmTime.TimeOfDay) < TimeSpan.FromSeconds(2.0)));
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mute_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("Mute");
            this.usbMute.DPlayerButtonType = (this.usbMute.DPlayerButtonType == DPlayerButtonTypes.SpeakerUnmuted) ? DPlayerButtonTypes.SpeakerMuted : DPlayerButtonTypes.SpeakerUnmuted;
        }

        private void next_Click(object sender, EventArgs e)
        {
            this.OnMediaControlClicked("Next");
        }

        public void OnMediaControlClicked(string action)
        {
            if (this.MediaControlClicked != null)
            {
                this.MediaControlClicked(action);
            }
        }


        public AlbumArtForm(ID3Lib.TrackInfo trackInfo) : this()
        {
            this.trackInfo = trackInfo;
        }

        //private AlbumArtForm(int scrn) : this()
        //{
        //    this.screenNumber = scrn;
        //}

        private void AlbumArtForm_Load(object sender, EventArgs e)
        {
            TraceSwitch infoSwitch = new TraceSwitch("InfoSwitch", "General use switch");
            ClearLabelTexts();
            this.InitializedUnstyledControls();
            try
            {
                if (Debugger.IsAttached)
                {
                    this.RestoreForm();
                }
                else
                {
                    this.MaximizeForm();
                }
                this.HideButtons();
                this.DisplayTrackInfo(this.trackInfo);
            }
            catch
            {
                this.CloseForm();
            }

            this.regkeyScreenSaver = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            this.origScreenSaveSetting = this.regkeyScreenSaver.GetValue("ScreenSaveActive");
            this.regkeyScreenSaver.SetValue("ScreenSaveActive", "0");
            this.initialAlarmTime = Settings.Default.AlarmTime;
            DateTime now = DateTime.Now;
            if (this.initialAlarmTime.TimeOfDay < now.TimeOfDay)
            {
                this.initialAlarmTime = new DateTime(now.Year, now.Month, now.Day, this.initialAlarmTime.Hour, this.initialAlarmTime.Minute, this.initialAlarmTime.Second);
                this.initialAlarmTime.AddDays(1.0);
            }
            this.alarmTime = this.initialAlarmTime;

            SetSlideShow();
        }

        private void ClearLabelTexts()
        {
            lblCurrentTime.Text
                = lblAlbumName.Text
                = lblArtistName.Text
                = lblTrackName.Text
                = "";
        }

        private void SetSlideShow()
        {
            if (Directory.Exists(Settings.Default.SlideShowPath))
            {
                slides = Directory.GetFiles(Settings.Default.SlideShowPath, "*.jpg", SearchOption.AllDirectories);
                if (slides.Length > 0)
                {
                    useSlideShow = true;
                    SlideShowTimer.Interval = (int.Parse(Settings.Default.SlideShowIntervalSeconds)) * 1000;
                    SlideShowTimer.Enabled = true;
                }
            }
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.HideButtonsTimer.Start();
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            this.HideButtonsTimer.Stop();
            this.ShowCursor();
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileStream fs = File.Open(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DPlayerTracksToCheck.txt"), FileMode.Append))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(this.trackInfo.FilePath + Environment.NewLine);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        ~AlbumArtForm()
        {
            if (this.origScreenSaveSetting != null)
            {
                this.regkeyScreenSaver.SetValue("ScreenSaveActive", this.origScreenSaveSetting);
            }
        }

        private void SlideShowTimer_Tick(object sender, EventArgs e)
        {
            if (!useSlideShow)
            {
                return;
            }

            pictureBox1.Visible = false;
            if (isSlideShowPicture)
            {
                pictureBox1.Load(currentTrackImagePath);
                isSlideShowPicture = false;
            }
            else
            {
                pictureBox1.Load(slides[ReallyRandom.Next(slides.Length - 1)]);
                isSlideShowPicture = true;
            }
            SetPictureLocation();
        }
    }
}
