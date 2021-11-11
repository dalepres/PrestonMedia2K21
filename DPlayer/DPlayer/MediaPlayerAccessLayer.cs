namespace DPlayer
{
    using ID3Lib;
    using System;

    public sealed class MediaPlayerAccessLayer
    {
        private static AlbumArtForm form = new AlbumArtForm();
        private static DPMediaPlayer player;

        static MediaPlayerAccessLayer()
        {
            
            form.MediaControlClicked += new MediaControlEventHandler(MediaPlayerAccessLayer.form_MediaControlClicked);
        }

        private MediaPlayerAccessLayer()
        {
        }

        public static string HelloWorld()
        {
            return "Hello World!";
        }

        private static void form_MediaControlClicked(string action)
        {
            switch (action)
            {
                case "Next":
                    player.PlayNext();
                    break;

                case "Previous":
                    player.PlayPrevious();
                    break;

                case "Pause":
                    Player.Pause();
                    break;

                case "Play":
                    Player.Play();
                    break;

                case "VolumeUp":
                    player.VolumeUp();
                    break;

                case "VolumeDown":
                    player.VolumeDown();
                    break;

                case "Mute":
                    player.Mute();
                    break;

                case "Stop":
                    player.Stop();
                    break;

                case "FastForward":
                    player.FastForward();
                    break;

                case "Rewind":
                    player.Rewind();
                    break;
            }
        }

        internal static double GetCurrentPosition()
        {
            return player.GetCurrentPosition();
        }

        internal static string GetCurrentPositionString()
        {
            return player.GetCurrentPositionString();
        }

        public static int GetPlayerVolume()
        {
            return player.GetPlayerVolume();
        }

        private static PlayListInfo[] GetPlayListsInfo()
        {
            return player.GetPlayListsInfo();
        }

        private static void player_MediaEnded(object args)
        {
        }

        private static void player_PlayCommand(string args)
        {
            if (!form.Visible)
            {
                form.Show();
            }
            form.Invoke(form.SetLastPlayed, new object[] { args });
        }

        private static void player_PlayerPlaying(PlayerPlayingEventArgs args)
        {
            form.DisplayNextTrack(new TrackInfo(args.Url));
            form.Invoke(form.StartDurationCounter, new object[] { args.Duration, args.DurationString, args.Url });
        }

        internal static void PlayPlayList(PlayListInfo playListInfo, bool randomize)
        {
            player.PlayPlayList(playListInfo, randomize);
        }

        public static void SetPlayerVolume(int value)
        {
            player.SetPlayerVolume(value);
        }

        public static void SetRandomize(bool randomize)
        {
            player.Randomize = randomize;
        }

        public static void ShowArt()
        {
            form.Show();
        }

        public static void VolumeDown(int step)
        {
            player.VolumeDown(step);
        }

        public static void VolumeUp(int step)
        {
            player.VolumeUp(step);
        }

        public static DPMediaPlayer Player
        {
            get
            {
                if (player == null)
                {
                    player = new DPMediaPlayer();
                    player.MediaEnded += new PlayStateChangedEventHandler(MediaPlayerAccessLayer.player_MediaEnded);
                    player.PlayerPlaying += new PlayerPlayingEventHandler(MediaPlayerAccessLayer.player_PlayerPlaying);
                    player.PlayCommand += new PlayerPlayCommandEventHandler(MediaPlayerAccessLayer.player_PlayCommand);
                }
                return player;
            }
        }
    }
}

