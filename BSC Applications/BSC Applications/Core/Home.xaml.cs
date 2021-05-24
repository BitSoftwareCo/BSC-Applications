using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace BSC_Applications.Core
{
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
