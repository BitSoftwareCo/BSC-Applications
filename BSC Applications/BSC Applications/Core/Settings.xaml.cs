using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.Core
{
    public sealed partial class Settings
    {
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

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

            Theme.SelectedIndex = (int)roamingSettings.Values["theme"];

            Name.Text = (string)roamingSettings.Values["displayName"];

            TemporaryContent.IsOn = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());
            ClearTemporaryContent.IsEnabled = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());
        }
        private void About_Loaded(object sender, RoutedEventArgs e)
        {
            AppName.Text = lib.Data.Name;
            AppVersion.Text = lib.Data.Version;
            AppType.Text = lib.Data.Type;
            CopyAppInfo.Content = "Copy";

            AutoUpdates.IsOn = (bool)roamingSettings.Values["checkForUpdates"];

            Status.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
        }

        // General
        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["sound"] = SoundToggle.IsOn.ToString();
            ElementSoundPlayer.State = Boolean.Parse(roamingSettings.Values["sound"].ToString()) ? ElementSoundPlayerState.On : ElementSoundPlayerState.Off;
        }
        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roamingSettings.Values["theme"] = Theme.SelectedIndex;

            switch (Theme.SelectedIndex)
            {
                case 0: MainPage.nav.RequestedTheme = ElementTheme.Light; break;
                case 1: MainPage.nav.RequestedTheme = ElementTheme.Dark; break;
                case 2: MainPage.nav.RequestedTheme = ElementTheme.Default; break;
            }
        }
        private void Name_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            roamingSettings.Values["displayName"] = Name.Text;
        }
        private void TemporaryContent_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["temporaryContent"] = TemporaryContent.IsOn;
            ClearTemporaryContent.IsEnabled = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());
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
            roamingSettings.Values["checkForUpdates"] = AutoUpdates.IsOn;
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
            await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#update"));
            await Launcher.LaunchUriAsync(new Uri($"https://github.com/BitSoftwareCo/BSC-Applications/releases/download/{Version.Text}/BSC.Applications.zip"));

            CoreApplication.Exit();
        }
    }
}
