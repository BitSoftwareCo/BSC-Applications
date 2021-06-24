using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BSC_Applications.src
{
    public sealed partial class Settings
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Settings()
        {
            this.InitializeComponent();

            new lib.Events("Settings Loaded", 0);
        }

        private void General_Loaded(object sender, RoutedEventArgs e)
        {
            SoundToggle.IsOn = Boolean.Parse(roamingSettings.Values["sound"].ToString());

            if (roamingSettings.Values["backgroundColor"].ToString() == "default")
            {
                SolidColorBrush bgColor = Application.Current.Resources.ThemeDictionaries["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush;
                DefaultBackground.IsChecked = true;
                BGColorpicker.Color = bgColor.Color;
                BGColorVisual.Fill = new SolidColorBrush(BGColorpicker.Color);
            }
            else
            {
                string[] colors = roamingSettings.Values["backgroundColor"].ToString().Split(" , ");
                byte r = byte.Parse(colors[0]);
                byte g = byte.Parse(colors[1]);
                byte b = byte.Parse(colors[2]);
                byte a = byte.Parse(colors[3]);
                BGColorpicker.Color = Color.FromArgb(a, r, g, b);
                BGColorVisual.Fill = new SolidColorBrush(BGColorpicker.Color);
            }

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

        private void BGColorpicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            BGColorVisual.Fill = new SolidColorBrush(BGColorpicker.Color);
            roamingSettings.Values["backgroundColor"] = $"{BGColorpicker.Color.R} , {BGColorpicker.Color.G} , {BGColorpicker.Color.B} , {BGColorpicker.Color.A}";
        }

        private void DefaultBackground_Checked(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["backgroundColor"] = "default";
            BGColor.IsEnabled = false;
        }
        private void DefaultBackground_Unchecked(object sender, RoutedEventArgs e)
        {
            BGColor.IsEnabled = true;
        }

        private void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            if (senderButton.Name == "ResetFlyout_Rest")
            {
                lib.AppSettings.Reset();
                CoreApplication.Exit();
            }
            ResetFlyout.Hide();
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
                string log = lib.Events.events;
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, log);
                await CachedFileManager.CompleteUpdatesAsync(file);

                await Launcher.LaunchUriAsync(new Uri(file.Path));
            }
        }
    }
}