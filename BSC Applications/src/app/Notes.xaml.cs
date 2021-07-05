using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using muxc = Microsoft.UI.Xaml.Controls;

namespace BSC_Applications.src.app
{

    public sealed partial class Notes
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        private bool temp;

        public Notes()
        {
            temp = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());

            this.InitializeComponent();

            if (lib.Var.notesContent != null && temp)
                Text.Document.SetText(TextSetOptions.FormatRtf, lib.Var.notesContent);

            MainPage.nav.ItemInvoked += Nav_ItemInvoked;

            new lib.Event("Notes loaded", lib.Event.load);
        }

        private void ColorPickerFlyout_Pick_Click(object sender, RoutedEventArgs e)
        {
            Text.Document.Selection.CharacterFormat.ForegroundColor = ColorFlyout_Picker.Color;

            ColorFlyout.Hide();
            Text.Focus(FocusState.Keyboard);
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            if (senderButton.Name == "NewFlyout_New") Text.Document.SetText(0, "");
            NewFlyout.Hide();
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

        private async void SaveRTF(StorageFile file)
        {
            CachedFileManager.DeferUpdates(file);
            Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite);
            Text.Document.SaveToStream(TextGetOptions.FormatRtf, randAccStream);

            await CachedFileManager.CompleteUpdatesAsync(file);
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

        private void Nav_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (temp)
                Text.Document.GetText(TextGetOptions.FormatRtf, out lib.Var.notesContent);
        }
    }
}
