using System;
namespace MediaSample
{
	public interface IImageService
	{
		byte[] ResizeTheImage(byte[] imageData, float width, float height);
	}
}

