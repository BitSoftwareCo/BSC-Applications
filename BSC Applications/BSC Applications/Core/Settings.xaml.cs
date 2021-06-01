using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.Core
{
    public sealed partial class Settings
    {
        private lib.AppSettings appSettings = new lib.AppSettings();

        public Settings()
        {
            this.InitializeComponent();
        }

        private void General_Loaded(object sender, RoutedEventArgs e)
        {
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                SoundToggle.IsOn = true;
            else
                SoundToggle.IsOn = false;

            Theme.SelectedIndex = appSettings.Theme;

            TemporaryContent.IsOn = appSettings.TemporaryContent;
            ClearTemporaryContent.IsEnabled = appSettings.TemporaryContent;
        }
        private void About_Loaded(object sender, RoutedEventArgs e)
        {
            AppName.Text = lib.Data.Name;
            AppVersion.Text = lib.Data.Version;
            AppType.Text = lib.Data.Type;
            CopyAppInfo.Content = "Copy";

            AutoUpdates.IsOn = appSettings.CheckForUpdates;

            Status.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
        }
        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = appSettings.DisplayName;
        }

        // General
        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            appSettings.Sound = SoundToggle.IsOn;
            ElementSoundPlayer.State = appSettings.Sound ? ElementSoundPlayerState.On : ElementSoundPlayerState.Off;
        }
        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appSettings.Theme = Theme.SelectedIndex;

            switch (Theme.SelectedIndex)
            {
                case 0: MainPage.nav.RequestedTheme = ElementTheme.Light; break;
                case 1: MainPage.nav.RequestedTheme = ElementTheme.Dark; break;
                case 2: MainPage.nav.RequestedTheme = ElementTheme.Default; break;
            }
        }
        private async void ImportSettings_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.Desktop;
            open.FileTypeFilter.Add(".json");

            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                string json = await FileIO.ReadTextAsync(file);
                lib.AppSettings jsonAppSettings = JsonConvert.DeserializeObject<lib.AppSettings>(json);

                appSettings.DisplayName = jsonAppSettings.DisplayName;
                appSettings.TemporaryContent = jsonAppSettings.TemporaryContent;
                appSettings.Sound = jsonAppSettings.Sound;
                appSettings.Theme = jsonAppSettings.Theme;
                appSettings.NavbarLocation = jsonAppSettings.NavbarLocation;
                appSettings.BackgroundAcrylic = jsonAppSettings.BackgroundAcrylic;
                appSettings.CheckForUpdates = jsonAppSettings.CheckForUpdates;

                ContentDialog dialog = new ContentDialog
                {
                    Title = "BSC Applications needs to restart to apply new Settings",
                    Content = "BSC Applications will restart and apply your new settings.",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
                CoreApplication.Exit();
            }
        }
        private void TemporaryContent_Toggled(object sender, RoutedEventArgs e)
        {
            appSettings.TemporaryContent = TemporaryContent.IsOn;
            ClearTemporaryContent.IsEnabled = appSettings.TemporaryContent;
        }
        private async void ClearTemporaryContent_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Are you sure you want to Clear your Temporary Content?",
                Content = "Clearing your Temporary Content will delete anything that is unsaved.",
                PrimaryButtonText = "Clear",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                lib.Var.notesContent = null;
                lib.Var.todoContent = new List<string>();
            }
        }

        // About
        private void CopyAppInfo_Click(object sender, RoutedEventArgs e)
        {
            DataPackage package = new DataPackage();
            package.SetText($"{AppName.Text}\n{AppVersion.Text}\n{AppType.Text}");
            Clipboard.SetContent(package);
            CopyAppInfo.Content = "Copied";
        }
        private void AutoUpdates_Toggled(object sender, RoutedEventArgs e)
        {
            appSettings.CheckForUpdates = AutoUpdates.IsOn;
        }
        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            Update.Visibility = Visibility.Collapsed;
            Status.Visibility = Visibility.Collapsed;

            if (lib.Web.Set() == 0)
            {
                if (lib.Data.Version != lib.Web.package[0])
                {
                    Update.Visibility = Visibility.Visible;

                    Version.Text = lib.Web.package[0];
                    Publisher.Text = $"Publisher: {lib.Web.package[1].Split("\n")[0]}";
                }
                else
                    Status.Visibility = Visibility.Visible;
            }
            else
            {
                Status.Text = "Unable to check for updates.";
                Status.Visibility = Visibility.Visible;
            }
        }
        private async void Update_Click(object sender, RoutedEventArgs e)
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
            await Launcher.LaunchUriAsync(new Uri($"https://github.com/BitSoftwareCo/BSC-Applications/releases/download/{Version.Text}/BSC.Applications.zip"));

            CoreApplication.Exit();
        }

        // User
        private void Name_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            appSettings.DisplayName = Name.Text;
        }
    }
}
