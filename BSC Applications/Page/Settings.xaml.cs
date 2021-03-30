using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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

            if(ElementSoundPlayer.State == ElementSoundPlayerState.On)
                SoundToggle.IsOn = true;
            else
                SoundToggle.IsOn = false;

            website.Content = $"{lib.Data.Publisher}'s Website";
            website.NavigateUri = new Uri(lib.Data.Website);

            docs.Content = "Docs";
            docs.NavigateUri = new Uri(lib.Data.Docs);
        }

        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            roamingSettings.Values["sound"] = SoundToggle.IsOn.ToString();

            if (Boolean.Parse(roamingSettings.Values["sound"] as string))
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            else
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
        }

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
    }
}
