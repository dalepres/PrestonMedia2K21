namespace DPMediaPlayer
{
    using System;
    using WMPLib;

    public class PlayerPlayingEventArgs : EventArgs
    {
        private string album = string.Empty;
        private string albumArtist = string.Empty;
        private string artist = string.Empty;
        private double duration = 0.0;
        private string durationString = string.Empty;
        private string title = string.Empty;
        private string url = string.Empty;

        public PlayerPlayingEventArgs(IWMPMedia currentMedia)
        {
            if (currentMedia != null)
            {
                this.durationString = currentMedia.durationString;
                this.duration = currentMedia.duration;
                this.url = currentMedia.sourceURL;
            }
        }

        public double Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        public string DurationString
        {
            get
            {
                return this.durationString;
            }
            set
            {
                this.durationString = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }
    }
}

