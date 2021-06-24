using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BSC_Applications.src.app
{
    public sealed partial class Photo_View
    {
        StorageFile path = null;

        public Photo_View()
        {
            this.InitializeComponent();

            new lib.Events("Photo View Loaded", 0);
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
                Copy.IsEnabled = true;

                Message.Visibility = Visibility.Collapsed;
                using (IRandomAccessStream fileStream = await path.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    View.Source = bitmapImage;
                }
                Copy.Icon = new SymbolIcon(Symbol.Copy);
            }
        }
        private async void Capture_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

            path = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (path != null)
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(await path.OpenAsync(FileAccessMode.Read));
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(SoftwareBitmap.Convert(await decoder.GetSoftwareBitmapAsync(),
                                                                         BitmapPixelFormat.Bgra8,
                                                                         BitmapAlphaMode.Premultiplied));
                View.Source = bitmapSource;
                Copy.IsEnabled = true;
                Message.Visibility = Visibility.Collapsed;
            }
        }
        private void CopyImage_Click(object sender, RoutedEventArgs e)
        {
            if (path != null)
            {
                DataPackage imagePackage = new DataPackage();
                imagePackage.SetBitmap(RandomAccessStreamReference.CreateFromFile(path));
                Clipboard.SetContent(imagePackage);

                Copy.Icon = new SymbolIcon(Symbol.Accept);
            }
        }
    }
}
