using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BSC_Applications.Core.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Notes
    {
        public Notes()
        {
            this.InitializeComponent();

            if (lib.Var.notesContent != null)
                Text.Document.SetText(TextSetOptions.FormatRtf, lib.Var.notesContent);

            MainPage.nav.ItemInvoked += Nav_ItemInvoked;
        }
        
        private async void New_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Are you sure you want to create a new Note?",
                Content = "Creating a new Note will delete any unsaved text.",
                PrimaryButtonText = "New",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                Text.Document.SetText(0, "");
            }
        }
        private async void SaveLocal_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf", ".note" });
            savePicker.FileTypeChoices.Add("Text File", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "Untitled Note";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                if (file.Path.Contains(".note") || file.Path.Contains(".rtf"))
                    SaveRTF(file);
                else
                    SaveTXT(file);

            }
        }
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".note");
            open.FileTypeFilter.Add(".rtf");
            open.FileTypeFilter.Add(".txt");

            StorageFile file = await open.PickSingleFileAsync();

            if (file != null)
            {
                if (file.Path.Contains(".note") || file.Path.Contains(".rtf"))
                    OpenRTF(file);
                else
                    OpenTXT(file);
            }
        }
        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#notes"));
        }

        private async void SaveRTF(StorageFile file)
        {
            Text.Foreground = new SolidColorBrush(Colors.Black);

            CachedFileManager.DeferUpdates(file);
            Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite);
            Text.Document.SaveToStream(TextGetOptions.FormatRtf, randAccStream);

            await CachedFileManager.CompleteUpdatesAsync(file);

            if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                Text.Foreground = new SolidColorBrush(Colors.White);
        }
        private async void SaveTXT(StorageFile file)
        {
            CachedFileManager.DeferUpdates(file);
            Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite);
            Text.Document.SaveToStream(TextGetOptions.None, randAccStream);

            await CachedFileManager.CompleteUpdatesAsync(file);
        }

        private async void OpenRTF(StorageFile file)
        {
            Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read);
            Text.Document.LoadFromStream(TextSetOptions.FormatRtf, randAccStream);
        }
        private async void OpenTXT(StorageFile file)
        {
            Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read);
            Text.Document.LoadFromStream(TextSetOptions.None, randAccStream);
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            Text.Document.GetText(TextGetOptions.FormatRtf, out lib.Var.notesContent);
        }
    }
}
