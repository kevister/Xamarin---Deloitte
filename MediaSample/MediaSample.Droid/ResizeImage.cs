using System;
using System.IO;
using Android.Graphics;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaSample.Droid.ResizeImage))]


namespace MediaSample.Droid
{
	public class ResizeImage : IImageService
	{
		public ResizeImage()
		{
		}

		public byte[] ResizeTheImage(byte[] imageData, float width, float height)
		{
			Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
			Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

			using (MemoryStream ms = new MemoryStream())
			{
				resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
				return ms.ToArray();
			}
		}
	}
}

