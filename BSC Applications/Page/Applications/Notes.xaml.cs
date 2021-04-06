using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BSC_Applications.Page.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class File_View
    {
        public File_View()
        {
            this.InitializeComponent();
        }

        private async void New_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            { 
                Title = "Are you sure you want to create a new Note?",
                Content = "Clicking \"Yes\" will delete any unsaved text. Do you wish to continue?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No",
                CloseButtonText = "Cancel"
            };
            if(await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                Text.Document.SetText(0, "");
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("BSC Note File", new List<string>() { ".note" });
            savePicker.FileTypeChoices.Add("Rich Text File", new List<string>() { ".rtf" });
            savePicker.SuggestedFileName = "Untitled Note";
            
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Text.Foreground = new SolidColorBrush(Colors.Black);

                CachedFileManager.DeferUpdates(file);
                Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                Text.Document.SaveToStream((TextGetOptions)TextGetOptions.FormatRtf, randAccStream);
                
                await CachedFileManager.CompleteUpdatesAsync(file);

                if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    Text.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".note");
            open.FileTypeFilter.Add(".rtf");

            StorageFile file = await open.PickSingleFileAsync();

            if (file != null)
            {
                Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read);
                Text.Document.LoadFromStream((TextSetOptions)TextSetOptions.FormatRtf, randAccStream);
            }
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#text-edit");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
