using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BSC_Applications.Page.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Photo_View
    {
        StorageFile path = null;

        public Photo_View()
        {
            this.InitializeComponent();
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            path = await openPicker.PickSingleFileAsync();
            if (path != null)
            {
                using (IRandomAccessStream fileStream = await path.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    View.Source = bitmapImage;
                }
            }
        }

        private void CopyImage_Click(object sender, RoutedEventArgs e)
        {
            if(path != null)
            {
                DataPackage imagePackage = new DataPackage();
                imagePackage.SetBitmap(RandomAccessStreamReference.CreateFromFile(path));
                Clipboard.SetContent(imagePackage);
            }
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#photo-view");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
