using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace BSC_Applications.src
{
    public sealed partial class Home
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public Home()
        {
            this.InitializeComponent();

            Name.Text = roamingSettings.Values["displayName"] as string;

            new lib.Events("Home Loaded", 0);
        }

        private void AppButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name.Replace("_", " ");

            MainPage.nav.Header = buttonName;

            switch (buttonName)
            {
                case "Notes":
                    MainPage.frame.Navigate(typeof(app.Notes), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[2];
                    break;
                case "Photo View":
                    MainPage.frame.Navigate(typeof(app.Photo_View), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[3];
                    break;
                case "Stopwatch":
                    MainPage.frame.Navigate(typeof(app.Stop_Watch), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[4];
                    break;

                case "Todo":
                    MainPage.frame.Navigate(typeof(app.Todo), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[5];
                    break;
                case "Settings":
                    MainPage.frame.Navigate(typeof(Settings), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.SettingsItem;
                    break;
            };
        }
    }
}
