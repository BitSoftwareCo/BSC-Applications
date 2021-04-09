using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BSC_Applications.Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings
    {
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Settings()
        {
            this.InitializeComponent();

            website.Content = $"{lib.Data.Publisher}'s Website";
            website.NavigateUri = new Uri(lib.Data.Website);
        }

        private void General_Loaded(object sender, RoutedEventArgs e)
        {
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                SoundToggle.IsOn = true;
            else
                SoundToggle.IsOn = false;

            Name.Text = roamingSettings.Values["displayName"] as string;

            Status.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
        }

        // General
        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["sound"] = SoundToggle.IsOn.ToString();

            if (Boolean.Parse(roamingSettings.Values["sound"] as string))
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            else
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
        }
        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            Page.lib.Web.Set();
            if (Page.lib.Data.Version != Page.lib.Web.package[0])
            {
                Update.Visibility = Visibility.Visible;

                Version.Text = Page.lib.Web.package[0];
                Publisher.Text = $"Publisher: {Page.lib.Web.package[1].Split("\n")[0]}";
            }
            else
                Status.Visibility = Visibility.Visible;
        }
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#update");
            Uri uri2 = new Uri($"https://github.com/BitSoftwareCo/BSC-Applications/releases/download/{Version.Text}/BSC.Applications.zip");
            await Launcher.LaunchUriAsync(uri);
            await Launcher.LaunchUriAsync(uri2);
        }

        // Feeback
        private async void About_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "About",
                Content = $"Name: {lib.Data.Name} \n" +
                          $"Version: {lib.Data.Version} \n" +
                          $"Publisher: {lib.Data.Publisher}",
                PrimaryButtonText = "Copy",
                CloseButtonText = "OK"
            };

            if(await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                DataPackage package = new DataPackage();
                package.SetText(dialog.Content.ToString());
                Clipboard.SetContent(package);
            }
        }
        private async void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Text Document", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "Untitled Report";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string text = $"Name: {lib.Data.Name} \n" +
                              $"Version: {lib.Data.Version} \n" +
                              $"Publisher: {lib.Data.Publisher} \n" +
                              $"Date: {DateTime.Today}";

                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, text);
                await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["displayName"] = Name.Text;
        }
    }
}
