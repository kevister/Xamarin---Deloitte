using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace MediaSample
{
  public partial class MediaPage : ContentPage
  {
    public MediaPage()
    {
      InitializeComponent();

      takePhoto.Clicked += async (sender, args) =>
      {
		  //await CrossMedia.Current.Initialize();

        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        {
          DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
          return;
        }

		  var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
		  {
			  SaveToAlbum = true,
          	 Directory = "9GAG",
          Name = "test.jpg",
        });

        if (file == null)
          return;

		  //DisplayAlert("File Location", file.Path, "OK");

		  //image.Source = ImageSource.FromStream(() =>
		  //{
		  //  var stream = file.GetStream();
		  //  file.Dispose();
		  //  return stream;
		  //});
				var mysfile = new byte[0];

		  using (var memoryStream = new MemoryStream())
		  {
			  file.GetStream().CopyTo(memoryStream);
			  var myfile = memoryStream.ToArray();
			  mysfile = myfile;
			  file.Dispose();
		  }

		  byte[] resizedImage = DependencyService.Get<IImageService>().ResizeTheImage(mysfile, 100, 100);

		  mysfile = resizedImage;

		  image.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
      };

      pickPhoto.Clicked += async (sender, args) =>
      {
		  //await CrossMedia.Current.Initialize();

        if (!CrossMedia.Current.IsPickPhotoSupported)
        {
          DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
          return;
        }
        var file = await CrossMedia.Current.PickPhotoAsync();


        if (file == null)
          return;

        //image.Source = ImageSource.FromStream(() =>
        //{
        //  var stream = file.GetStream();
        //  file.Dispose();
        //  return stream;
        //});
				var mysfile = new byte[0];

		  using (var memoryStream = new MemoryStream())
		  {
			  file.GetStream().CopyTo(memoryStream);
			  var myfile = memoryStream.ToArray();
			  mysfile = myfile;
			  file.Dispose();
		  }

		  byte[] resizedImage = DependencyService.Get<IImageService>().ResizeTheImage(mysfile, 100, 100);

		  mysfile = resizedImage;
		  image.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
      };

      takeVideo.Clicked += async (sender, args) =>
      {
        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
        {
          DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
          return;
        }

		  var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
		  {
			  SaveToAlbum = true,
          	 Directory = "DefaultVideos",
					Name = "video.mp4"
        });

        if (file == null)
          return;

        DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

        file.Dispose();
      };

      pickVideo.Clicked += async (sender, args) =>
      {
        if (!CrossMedia.Current.IsPickVideoSupported)
        {
          DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
          return;
        }
        var file = await CrossMedia.Current.PickVideoAsync();

        if (file == null)
          return;

        DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
        file.Dispose();
      };
    }
  }
}
