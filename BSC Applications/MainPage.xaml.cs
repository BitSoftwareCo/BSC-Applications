using muxc = Microsoft.UI.Xaml.Controls;
using BSC_Applications.src;
using BSC_Applications.src.app;
using BSC_Applications.src.lib;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.Storage;
using System;

namespace BSC_Applications
{

    public sealed partial class MainPage
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public static Frame frame;
        public static muxc.NavigationView nav;

        public MainPage()
        {
            new AppSettings();

            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            titleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            

            ElementSoundPlayer.State = Boolean.Parse(roamingSettings.Values["sound"].ToString()) ? ElementSoundPlayerState.On
                                                                                                 : ElementSoundPlayerState.Off;

            frame = Content;
            nav = Nav;

            new Event("MainPage loaded", Event.load);

            Content.Navigate(typeof(Home));
        }

        private void Nav_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            string selected = args.InvokedItem.ToString();
            Nav.Header = selected;
            
            switch (selected)
            {
                case "Home": Content.Navigate(typeof(Home)); break;
                case "Media View": Content.Navigate(typeof(Media_View)); break;
                case "Notes": Content.Navigate(typeof(Notes)); break;
                case "Stopwatch": Content.Navigate(typeof(Stop_Watch)); break;
                case "Todo": Content.Navigate(typeof(Todo)); break;
                default:
                    Content.Navigate(typeof(Settings));
                    Nav.Header = "Settings";
                    break;
            };
        }

    }
}