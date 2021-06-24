using BSC_Applications.src;
using BSC_Applications.src.app;
using BSC_Applications.src.lib;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using System;

namespace BSC_Applications
{

    public sealed partial class MainPage
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public static Frame frame;
        public static NavigationView nav;

        public MainPage()
        {
            new AppSettings();

            this.InitializeComponent();

            ElementSoundPlayer.State = Boolean.Parse(roamingSettings.Values["sound"].ToString()) ? ElementSoundPlayerState.On
                                                                                                 : ElementSoundPlayerState.Off;

            SetBG();

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
                case "Notes": Content.Navigate(typeof(Notes)); break;
                case "Photo View": Content.Navigate(typeof(Photo_View)); break;
                case "Stopwatch": Content.Navigate(typeof(Stop_Watch)); break;
                case "Todo": Content.Navigate(typeof(Todo)); break;
                default:
                    Content.Navigate(typeof(Settings));
                    Nav.Header = "Settings";
                    break;
            };
        }

        public void SetBG()
        {
            SolidColorBrush bgColor = Application.Current.Resources.ThemeDictionaries["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush;
            Color tintColor;
            if (roamingSettings.Values["backgroundColor"].ToString() == "default") tintColor = bgColor.Color;
            else
            {
                string[] colors = roamingSettings.Values["backgroundColor"].ToString().Split(" , ");
                byte r = byte.Parse(colors[0]);
                byte g = byte.Parse(colors[1]);
                byte b = byte.Parse(colors[2]);
                byte a = byte.Parse(colors[3]);
                tintColor = Color.FromArgb(a, r, g, b);
            }

            AcrylicBrush bg = new AcrylicBrush();
            bg.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
            bg.TintColor = tintColor;
            bg.FallbackColor = tintColor;
            bg.TintOpacity = 0.8;

            Nav.Background = bg;
        }
    }
}