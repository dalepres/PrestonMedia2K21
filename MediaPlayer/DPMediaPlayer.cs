namespace DPMediaPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Timers;
    using WMPLib;

    public class DPMediaPlayer
    {
        private TraceSwitch infoSwitch = new TraceSwitch("InfoSwitch", "General use switch");
        private bool isMuted;
        private Timer mediaEndedTimer = new Timer(3000.0);
        private bool mediaIsEnded = false;
        private byte muteVolume;
        private string oldMediaName = string.Empty;
        private WindowsMediaPlayer player = new WindowsMediaPlayer();
        private IWMPPlaylist playList = null;
        private bool randomize = false;

        public event PlayStateChangedEventHandler MediaEnded;

        public event PlayerPlayCommandEventHandler PlayCommand;

        public event PlayerPlayingEventHandler PlayerPlaying;

        public event PlayStateChangedEventHandler PlayStateChanged;

        public DPMediaPlayer()
        {
            this.mediaEndedTimer.Enabled = false;
            this.mediaEndedTimer.Elapsed += new ElapsedEventHandler(this.mediaEndedTimer_Elapsed);
            this.player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(this.Player_PlayStateChange);
            this.player.MediaError += new _WMPOCXEvents_MediaErrorEventHandler(this.Player_MediaError);
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (File.Exists(appDataPath + @"\PrestonMedia\DPMediaPlayer\DPlayerLog.txt"))
            {
                File.Delete(appDataPath + @"\PrestonMedia\DPMediaPlayer\DPlayerLog.txt");
            }
        }

        public void FastForward()
        {
            this.player.controls.fastForward();
        }

        public double GetCurrentPosition()
        {
            return this.player.controls.currentPosition;
        }

        public string GetCurrentPositionString()
        {
            return this.player.controls.currentPositionString;
        }

        public int GetPlayerVolume()
        {
            return this.player.settings.volume;
        }

        public IWMPPlaylist GetPlayList(string playListName)
        {
            IWMPPlaylistArray matching = this.player.playlistCollection.getByName(playListName);
            if (matching.count == 1)
            {
                return matching.Item(0);
            }
            return null;
        }

        public IWMPPlaylistArray GetPlayLists()
        {
            return this.player.playlistCollection.getAll();
        }

        public PlayListInfo[] GetPlayListsInfo()
        {
            List<PlayListInfo> infos = new List<PlayListInfo>();
            IWMPPlaylistArray lists = this.GetPlayLists();
            for (int count = 0; count < lists.count; count++)
            {
                if (count == 0x13)
                {
                    count = 0x13;
                }
                string playListType = string.Empty;
                try
                {
                    IWMPPlaylist playList = lists.Item(count);
                    for (int attribCount = 0; attribCount < playList.attributeCount; attribCount++)
                    {
                        if (playList.get_attributeName(attribCount) == "PlaylistType")
                        {
                            playListType = playList.getItemInfo("PlaylistType");
                            break;
                        }
                    }
                    PlayListInfo info = new PlayListInfo(playList.name, playListType);
                    if ((playList.count > 0) && (playList.get_Item(0).getItemInfo("MediaType") == "audio"))
                    {
                        infos.Add(info);
                    }
                }
                catch
                {
                    this.LogMessage("Error loading playlists", string.Format("Could not load all playlists because playlist index {0} was not found.", count.ToString()));
                }
            }
            return infos.ToArray();
        }

        private void LogMessage(string title, string details)
        {
            string pmDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\PrestonMedia";
            string dpDataPath = pmDataPath + @"\DPMediaPlayer";
            if (!Directory.Exists(pmDataPath))
            {
                Directory.CreateDirectory(pmDataPath);
            }
            if (!Directory.Exists(dpDataPath))
            {
                Directory.CreateDirectory(dpDataPath);
            }
            using (FileStream fs = File.Open(dpDataPath + @"\DPlayerLog.txt", FileMode.Append))
            {
                if (fs != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(DateTime.Now.ToString() + "\t" + title + "\t" + details + "\r\n");
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
        }

        private void mediaEndedTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.mediaIsEnded = false;
            this.mediaEndedTimer.Enabled = false;
            this.OnMediaEnded(null);
        }

        public void Mute()
        {
            if (this.isMuted)
            {
                this.isMuted = false;
                this.player.settings.volume = this.muteVolume;
            }
            else
            {
                this.isMuted = true;
                this.muteVolume = (byte) this.player.settings.volume;
                this.player.settings.volume = 0;
            }
        }

        protected void OnMediaEnded(object args)
        {
            if (this.MediaEnded != null)
            {
                this.MediaEnded(null);
            }
        }

        protected void OnPlayCommand(string args)
        {
            if (this.PlayCommand != null)
            {
                this.PlayCommand(args);
            }
        }

        protected void OnPlayerPlaying()
        {
            if (this.PlayerPlaying != null)
            {
                this.PlayerPlaying(new PlayerPlayingEventArgs(this.player.currentMedia));
            }
        }

        protected void OnPlayStateChanged(object args)
        {
            if (this.PlayStateChanged != null)
            {
                this.PlayStateChanged(args);
            }
        }

        public void Pause()
        {
            this.player.controls.pause();
        }

        public void Play()
        {
            this.player.controls.play();
        }

        public void Play(string filePath)
        {
            this.player.controls.stop();
            this.player.URL = filePath;
            this.player.controls.play();
        }

        private void Player_MediaError(object pMediaObject)
        {
            throw new Exception(pMediaObject.ToString());
        }

        private void Player_PlayStateChange(int NewState)
        {
            WMPPlayState playState = (WMPPlayState) NewState;
            if (this.infoSwitch.TraceInfo)
            {
                this.LogMessage("Player_PlayStateChange", "WMPPlayState." + playState.ToString());
            }
            this.OnPlayStateChanged(NewState);
            switch (playState)
            {
                case WMPPlayState.wmppsPlaying:
                    this.OnPlayerPlaying();
                    break;

                case WMPPlayState.wmppsMediaEnded:
                    if (!this.mediaIsEnded)
                    {
                        this.mediaIsEnded = true;
                        this.mediaEndedTimer.Enabled = true;
                    }
                    break;
            }
        }

        public void PlayNext()
        {
            this.player.controls.next();
            this.OnPlayCommand(this.oldMediaName);
            this.oldMediaName = this.player.currentMedia.name;
        }

        public void PlayPlayList(int listIndex)
        {
            if (this.playList == null)
            {
                throw new Exception("Playlist is null.");
            }
            if (this.playList.count <= listIndex)
            {
                throw new IndexOutOfRangeException();
            }
            this.player.currentMedia = this.playList.get_Item(listIndex);
            this.player.controls.play();
        }

        public void PlayPlayList(PlayListInfo playListInfo, bool randomize)
        {
            IWMPPlaylistArray list = this.player.playlistCollection.getByName(playListInfo.Name);
            if (list.count == 1)
            {
                IWMPPlaylist playList = list.Item(0);
                this.PlayPlayList(playList, randomize);
            }
        }

        private void PlayPlayList(IWMPPlaylist playList, bool randomize)
        {
            if ((playList != null) && (playList.count != 0))
            {
                this.player.settings.setMode("shuffle", randomize);
                this.playList = playList;
                this.player.currentPlaylist = playList;
                this.LogMessage("Set currentMedia", this.player.currentMedia.sourceURL);
                this.OnPlayCommand(this.oldMediaName);
                this.oldMediaName = this.player.currentMedia.name;
            }
        }

        public void PlayPrevious()
        {
            this.player.controls.previous();
            this.OnPlayCommand(this.oldMediaName);
            this.oldMediaName = this.player.currentMedia.name;
        }

        public void Rewind()
        {
            this.player.controls.fastReverse();
        }

        public void SetPlayerVolume(int volume)
        {
            if (volume < 0)
            {
                this.player.settings.volume = 0;
            }
            else if (volume > 100)
            {
                this.player.settings.volume = 100;
            }
            else
            {
                this.player.settings.volume = volume;
            }
        }

        public void Stop()
        {
            this.player.controls.stop();
        }

        public void VolumeDown()
        {
            this.VolumeDown(3);
        }

        public void VolumeDown(int step)
        {
            if (this.isMuted)
            {
                this.Mute();
            }
            else
            {
                this.SetPlayerVolume(this.GetPlayerVolume() - step);
            }
        }

        public void VolumeUp()
        {
            this.VolumeUp(3);
        }

        public void VolumeUp(int step)
        {
            if (this.isMuted)
            {
                this.Mute();
            }
            else
            {
                this.SetPlayerVolume(this.GetPlayerVolume() + step);
            }
        }

        public WindowsMediaPlayer Player
        {
            get
            {
                return this.player;
            }
        }

        public bool Randomize
        {
            get
            {
                return this.randomize;
            }
            set
            {
                this.randomize = value;
            }
        }
    }
}

