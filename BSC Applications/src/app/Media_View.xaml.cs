using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BSC_Applications.src.app
{
    public sealed partial class Media_View
    {
        IReadOnlyList<IStorageItem> itemsList;

        public Media_View()
        {
            this.InitializeComponent();

            new lib.Event("Media View loaded", lib.Event.load);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            itemsList = await picturesFolder.GetFilesAsync();

            if (itemsList.Count == 0)
            {
                Message.Visibility = Visibility.Visible;
                Command.IsEnabled = false;
            }

            foreach (IStorageFile item in itemsList)
            {
                if (item.Name.Contains(".bmp") ||
                    item.Name.Contains(".png") ||
                    item.Name.Contains(".jpeg") ||
                    item.Name.Contains(".jpg"))
                {
                    using (IRandomAccessStream fileStream = await item.OpenAsync(FileAccessMode.Read))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(fileStream);

                        Image image = new Image
                        {
                            Source = bitmapImage,
                            MaxWidth = 590,
                            MaxHeight = 530
                        };
                        View.Items.Add(image);
                    }
                }
            }
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += Share_DataRequested;
            DataTransferManager.ShowShareUI();
        }
        private void Share_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            RandomAccessStreamReference bitmap = RandomAccessStreamReference.CreateFromFile((IStorageFile)itemsList[View.SelectedIndex]);

            args.Request.Data.SetBitmap(bitmap);
            args.Request.Data.Properties.Title = itemsList[View.SelectedIndex].Name.ToString();
            args.Request.Data.Properties.Description = $"This file was shared from {lib.Data.Name} - {lib.Data.Version}.";
        }
    }
}
