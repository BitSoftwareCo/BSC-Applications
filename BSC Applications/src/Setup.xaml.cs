using Newtonsoft.Json;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace BSC_Applications.src
{
    public sealed partial class Setup : Page
    {
        private lib.AppSettings appSettings = new lib.AppSettings();

        public Setup()
        {
            this.InitializeComponent();

            new lib.Events("Setup Loaded", 0);


        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            appSettings.DisplayName = Name.Text;
        }

        private void Sound_Toggled(object sender, RoutedEventArgs e)
        {
            appSettings.Sound = Sound.IsOn;
            ElementSoundPlayer.State = appSettings.Sound ? ElementSoundPlayerState.On : ElementSoundPlayerState.Off;
        }
        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appSettings.Theme = Theme.SelectedIndex;
            Theme.Header = "Theme - The new theme will apply when you click \"Finish\"";
        }
        
        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.frame.Navigate(typeof(Navigation), null, new DrillInNavigationTransitionInfo());
            appSettings.New = false;
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
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
                appSettings.New = jsonAppSettings.New;

                ContentDialog dialog = new ContentDialog
                {
                    Title = "BSC Applications needs to restart to apply new Settings",
                    Content = "BSC Applications will restart and apply your new settings.",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
                MainPage.frame.Navigate(typeof(Navigation), null, new DrillInNavigationTransitionInfo());
            }
        }
    }
}
