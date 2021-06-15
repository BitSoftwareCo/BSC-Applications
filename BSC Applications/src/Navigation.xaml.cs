using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.src
{
    public sealed partial class Navigation : Page
    {
        private lib.AppSettings appSettings = new lib.AppSettings();

        public static Frame frame;
        public static NavigationView nav;

        public Navigation()
        {
            this.InitializeComponent();

            ElementSoundPlayer.State = appSettings.Sound ? ElementSoundPlayerState.On
                                                         : ElementSoundPlayerState.Off;

            switch (appSettings.Theme)
            {
                case 0: RequestedTheme = ElementTheme.Light; break;
                case 1: RequestedTheme = ElementTheme.Dark; break;
                case 2: RequestedTheme = ElementTheme.Default; break;
            }

            CheckForUpdates();

            frame = Content;
            nav = Nav;

            Content.Navigate(typeof(Home));
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string selected = args.InvokedItem.ToString();
            Nav.Header = selected;
            switch (selected)
            {
                case "Home": Content.Navigate(typeof(Home)); break;
                case "Notes": Content.Navigate(typeof(app.Notes)); break;
                case "Photo View": Content.Navigate(typeof(app.Photo_View)); break;
                case "Stopwatch": Content.Navigate(typeof(app.Stop_Watch)); break;
                case "Todo": Content.Navigate(typeof(app.Todo)); break;
                default:
                    Content.Navigate(typeof(Settings));
                    Nav.Header = "Settings";
                    break;
            };
        }

        private async void CheckForUpdates()
        {
            if (lib.Package.Set() == 0 && appSettings.CheckForUpdates)
            {
                int webVersion = lib.Package.version;
                string sVersion = lib.Package.sVersion;
                if (lib.Data.iVersion < webVersion)
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = $"Update Available",
                        Content = $"BSC Applications@{sVersion} is out now. Would you like to update to {sVersion}?\n\nChange-Log:\n{lib.Package.changelog}",
                        PrimaryButtonText = "Update",
                        SecondaryButtonText = "Stop Checking",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult dialogResult = await dialog.ShowAsync();
                    if (dialogResult == ContentDialogResult.Primary)
                    {
                        FileSavePicker savePicker = new FileSavePicker();
                        savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                        savePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json" });
                        savePicker.SuggestedFileName = "BSC Applications Settings";

                        StorageFile file = await savePicker.PickSaveFileAsync();
                        if (file != null)
                        {
                            string json = JsonConvert.SerializeObject(lib.AppSettings.All);
                            CachedFileManager.DeferUpdates(file);
                            await FileIO.WriteTextAsync(file, json);
                            await CachedFileManager.CompleteUpdatesAsync(file);
                        }

                        await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#update"));
                        await Launcher.LaunchUriAsync(new Uri($"https://github.com/BitSoftwareCo/BSC-Applications/releases/download/{webVersion}/BSC.Applications.zip"));

                        CoreApplication.Exit();
                    }
                    else if (dialogResult == ContentDialogResult.Secondary)
                    {
                        appSettings.CheckForUpdates = false;
                    }
                }
            }
        }
    }
}
