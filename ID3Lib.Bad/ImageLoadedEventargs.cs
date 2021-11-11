using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ID3Lib
{
	public class ImageLoadedEventArgs : EventArgs
	{
		private string imageName;
		private Image image;

		public ImageLoadedEventArgs(string imageName, Image image)
		{
			this.imageName = imageName;
			this.image = image;
		}


		public ImageLoadedEventArgs(string imageName, Stream stream)
		{
			this.ImageName = imageName;
			this.image = Image.FromStream(stream);
		}

		public string ImageName
		{
			get { return imageName; }
			set { imageName = value; }
		}


		public Image Image
		{
			get { return image; }
			set { image = value; }
		}
	}
}
