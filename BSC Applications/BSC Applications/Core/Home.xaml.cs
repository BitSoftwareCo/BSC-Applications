using System;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Core
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home
    {
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Home()
        {
            this.InitializeComponent();

            Pfp.DisplayName = roamingSettings.Values["displayName"] as string;
            Name.Text = roamingSettings.Values["displayName"] as string;
        }

        private void AppButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name.Replace("_", " ");

            MainPage.nav.Header = buttonName;

            switch (buttonName)
            {
                case "Notes":
                    MainPage.frame.Navigate(typeof(Applications.Notes), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[2];
                    break;
                case "Photo View":
                    MainPage.frame.Navigate(typeof(Applications.Photo_View), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[3];
                    break;
                case "Todo":
                    MainPage.frame.Navigate(typeof(Applications.Todo), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[4];
                    break;
                case "Settings":
                    MainPage.frame.Navigate(typeof(Settings), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.SettingsItem;
                    break;
            };
        }
    }
}
