using System;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BSC_Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public static Frame frame;
        public static NavigationView nav;

        public MainPage()
        {
            new Core.lib.Setup();

            ElementSoundPlayer.State = Boolean.Parse(roamingSettings.Values["sound"].ToString()) ? ElementSoundPlayerState.On
                                                                                                 : ElementSoundPlayerState.Off;

            switch (roamingSettings.Values["theme"])
            {
                case 0: RequestedTheme = ElementTheme.Light; break;
                case 1: RequestedTheme = ElementTheme.Dark; break;
                case 2: RequestedTheme = ElementTheme.Default; break;
            }

            this.InitializeComponent();

            frame = Content;
            nav = Nav;

            Content.Navigate(typeof(Core.Home));

            CheckForUpdates();
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string selected = args.InvokedItem.ToString();
            Nav.Header = selected;
            switch (selected)
            {
                case "Home": Content.Navigate(typeof(Core.Home)); break;
                case "Notes": Content.Navigate(typeof(Core.Applications.Notes)); break;
                case "Photo View": Content.Navigate(typeof(Core.Applications.Photo_View)); break;
                case "Todo": Content.Navigate(typeof(Core.Applications.Todo)); break;
                default:
                    Content.Navigate(typeof(Core.Settings));
                    Nav.Header = "Settings";
                    break;
            };
        }

        private async void CheckForUpdates()
        {
            if (Core.lib.Web.Set() == 0 && (bool)roamingSettings.Values["checkForUpdates"])
            {
                string webVersion = Core.lib.Web.package[0];
                if (Core.lib.Data.Version != webVersion)
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = $"BSC Applications@{webVersion}",
                        Content = $"BSC Applications@{webVersion} is out now. Would you like to update to {webVersion}?",
                        PrimaryButtonText = "Update",
                        SecondaryButtonText = "Stop Checking",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult dialogResult = await dialog.ShowAsync();
                    if (dialogResult == ContentDialogResult.Primary)
                    {
                        await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#update"));
                        await Launcher.LaunchUriAsync(new Uri($"https://github.com/BitSoftwareCo/BSC-Applications/releases/download/{webVersion}/BSC.Applications.zip"));

                        CoreApplication.Exit();
                    }
                    else if (dialogResult == ContentDialogResult.Secondary)
                    {
                        roamingSettings.Values["checkForUpdates"] = false;
                    }
                }
            }
        }
    }
}