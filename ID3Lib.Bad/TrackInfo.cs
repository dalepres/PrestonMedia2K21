using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
	public class TrackInfo
	{
		string filePath;

		private string trackName = string.Empty;
		private string albumName = string.Empty;
		private string artistName = string.Empty;
		private int trackNumber = 0;
		private string albumArtist = string.Empty;

		private Mp3File mp3File = null;
		private List<V2Tag> v2Tags = null;
		private V1Tag v1Tag = null;
		private V2Tag v2Tag = null;

		public TrackInfo(string filePath)
		{
			this.filePath = filePath;
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("File " + filePath + " could not be found.");
			}

			mp3File = new Mp3File(filePath);
			GetTrackInfo();
			mp3File.Close();
			mp3File.Dispose();
		}

		private void GetTrackInfo()
		{
			GetAlbumName();
			GetArtistName();
			GetTrackName();
			GetAlbumArtist();
			GetTrackNumber();
		}


		private void GetV2Tag()
		{
			v2Tags = mp3File.GetV2Tags();
			if (v2Tags.Count > 0)
			{
				v2Tag = v2Tags[0];
			}
		}


		private void GetV1Tag()
		{
			v1Tag = mp3File.GetV1Tag();
		}


		#region Album

		private void GetAlbumName()
		{
			albumName = GetAlbumFromV2Tag();
			if (albumName.Trim().Length == 0)
			{
				albumName = GetAlbumFromV1Tag();
			}
		}


		private string GetAlbumFromV1Tag()
		{
			if (this.V1Tag != null)
			{
				return this.V1Tag.Album;
			}
			else
			{
				return string.Empty;
			}
		}


		private string GetAlbumFromV2Tag()
		{
			if (this.V2Tag != null)
			{
				IEnumerator<V2Frame> ie = null;
				ie = v2Tag.Frames["TALB"];
				while (ie.MoveNext())
				{
					TALBTextFrame tf = (TALBTextFrame)ie.Current;
					return tf.Text;
				}
			}

			return string.Empty;
		}


		#endregion Album

		#region TrackNumber

		private void GetTrackNumber()
		{
			trackNumber = GetTrackNumberFromV2Tag();
		}


		private int GetTrackNumberFromV2Tag()
		{
			string text = "0";
			int num;
			if (this.V2Tag != null)
			{
				IEnumerator<V2Frame> ie = null;
				ie = v2Tag.Frames["TRCK"];
				while (ie.MoveNext())
				{
					TRCKTextFrame tf = (TRCKTextFrame)ie.Current;
					text = tf.Text;
					break;
				}
			}

			if (text.IndexOf("/") > 0)
			{
				text = text.Split('/')[0];
			}

			int.TryParse(text, out num);

			return num;
		}


		#endregion TrackNumber

		#region AlbumArtist

		private void GetAlbumArtist()
		{
			albumArtist = GetAlbumArtistFromV2Tag();
		}


		private string GetAlbumArtistFromV2Tag()
		{
			if (this.V2Tag != null)
			{
				IEnumerator<V2Frame> ie = null;
				ie = v2Tag.Frames["TPE2"];
				while (ie.MoveNext())
				{
					TPE2TextFrame tf = (TPE2TextFrame)ie.Current;
					return tf.Text;
				}
			}

			return string.Empty;
		}


		#endregion AlbumArtist

		#region Track

		private void GetTrackName()
		{
			trackName = GetTrackFromV2Tag();
			if (trackName.Trim().Length == 0)
			{
				trackName = GetTrackFromV1Tag();
			}
		}


		private string GetTrackFromV1Tag()
		{
			if (this.V1Tag != null)
			{
				return this.V1Tag.Title;
			}
			else
			{
				return string.Empty;
			}
		}


		private string GetTrackFromV2Tag()
		{
			if (this.V2Tag != null)
			{
				IEnumerator<V2Frame> ie = null;
				ie = v2Tag.Frames["TIT2"];
				while (ie.MoveNext())
				{
					TIT2TextFrame tf = (TIT2TextFrame)ie.Current;
					return tf.Text;
				}
			}

			return string.Empty;
		}


		#endregion Track

		#region Artist

		private void GetArtistName()
		{
			artistName = GetArtistFromV2Tag();
			if (artistName.Trim().Length == 0)
			{
				artistName = GetArtistFromV1Tag();
			}
		}


		private string GetArtistFromV1Tag()
		{
			if (this.V1Tag != null)
			{
				return this.V1Tag.Artist;
			}
			else
			{
				return string.Empty;
			}
		}


		private string GetArtistFromV2Tag()
		{
			if (this.V2Tag != null)
			{
				IEnumerator<V2Frame> ie = null;
				ie = v2Tag.Frames["TPE1"];
				while (ie.MoveNext())
				{
					TPE1TextFrame tf = (TPE1TextFrame)ie.Current;
					return tf.Text;
				}
			}

			return string.Empty;
		}


		#endregion Artist

		#region Properties


		private V2Tag V2Tag
		{
			get
			{
				if (this.v2Tag == null)
				{
					GetV2Tag();
				}

				return v2Tag;
			}
		}


		private V1Tag V1Tag
		{
			get
			{
				if (this.v1Tag == null)
				{
					GetV1Tag();
				}

				return v1Tag;
			}
		}


		public string FilePath
		{
			get { return filePath; }
		}


		public string TrackName
		{
			get { return trackName; }
		}


		public string AlbumName
		{
			get { return albumName; }
		}


		public string ArtistName
		{
			get { return artistName; }
		}


		public int TrackNumber
		{
			get { return trackNumber; }
		}



		public string AlbumArtist
		{
			get { return albumArtist; }
			set { albumArtist = value; }
		}


		#endregion Properties
	}
}
