using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.src
{
    public sealed partial class Settings
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Settings()
        {
            this.InitializeComponent();

            new lib.Event("Settings loaded", lib.Event.load);
        }

        private void General_Loaded(object sender, RoutedEventArgs e)
        {
            SoundToggle.IsOn = Boolean.Parse(roamingSettings.Values["sound"].ToString());

            TemporaryContent.IsOn = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());
        }
        private void About_Loaded(object sender, RoutedEventArgs e)
        {
            AppName.Text = lib.Data.Name;
            AppVersion.Text = lib.Data.Version;
            AppType.Text = lib.Data.Type;
            CopyAppInfo.Content = "Copy";
        }
        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = roamingSettings.Values["displayName"].ToString();
        }

        // General
        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["sound"] = SoundToggle.IsOn;
            ElementSoundPlayer.State = Boolean.Parse(roamingSettings.Values["sound"].ToString()) ? ElementSoundPlayerState.On 
                                                                                                 : ElementSoundPlayerState.Off;
        }
        private void TemporaryContent_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["temporaryContent"] = TemporaryContent.IsOn;
            ClearTemporaryContent.IsEnabled = Boolean.Parse(roamingSettings.Values["sound"].ToString());
        }
        private void ClearTemporaryContent_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            if (senderButton.Name == "ClearTempContentFlyout_Clear")
            {
                lib.Var.notesContent = null;
                lib.Var.todoContent = new List<string>();
            }
            ClearTempContentFlyout.Hide();
        }

        // About
        private void CopyAppInfo_Click(object sender, RoutedEventArgs e)
        {
            DataPackage package = new DataPackage();
            package.SetText($"{AppName.Text}\n{AppVersion.Text}\n{AppType.Text}");
            Clipboard.SetContent(package);
            CopyAppInfo.Content = "Copied";
        }

        // User
        private void Name_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            roamingSettings.Values["displayName"] = Name.Text;
        }

        // Resources & Feedback
        private async void SaveEventLog_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.FileTypeChoices.Add("Excel Document", new List<string>() { ".csv" });
            savePicker.SuggestedFileName = "BSC Applications Event Log";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string log = lib.Event.events;
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, log);
                await CachedFileManager.CompleteUpdatesAsync(file);

                await Launcher.LaunchUriAsync(new Uri(file.Path));
            }
        }
    }
}