using System;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BSC_Applications.Core
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
        }

        private void General_Loaded(object sender, RoutedEventArgs e)
        {
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                SoundToggle.IsOn = true;
            else
                SoundToggle.IsOn = false;

            Theme.SelectedIndex = (int)roamingSettings.Values["theme"];

            Name.Text = (string)roamingSettings.Values["displayName"];
        }
        private void About_Loaded(object sender, RoutedEventArgs e)
        {
            AppInfo.Text = $"Name: [{lib.Data.Name}] \n" +
                           $"Full Name: [{lib.Data.Name}@{lib.Data.Version}] \n" +
                           $"Version: [{lib.Data.Version}] \n" +
                           $"Publisher: [{lib.Data.Publisher}]";

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

        // About
        private void CopyAppInfo_Click(object sender, RoutedEventArgs e)
        {
            DataPackage package = new DataPackage();
            package.SetText(AppInfo.Text);
            Clipboard.SetContent(package);
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
