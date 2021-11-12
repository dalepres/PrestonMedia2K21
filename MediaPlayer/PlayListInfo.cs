namespace DPMediaPlayer
{
    using System;

    public class PlayListInfo
    {
        private string name;
        private string playListType;

        public PlayListInfo(string name, string playListType)
        {
            this.name = name;
            this.playListType = playListType;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string PlayListType
        {
            get
            {
                return this.playListType;
            }
            set
            {
                this.playListType = value;
            }
        }
    }
}

