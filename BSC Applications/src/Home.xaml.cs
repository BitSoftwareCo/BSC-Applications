using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace BSC_Applications.src
{
    public sealed partial class Home
    {
        private lib.AppSettings appSettings = new lib.AppSettings();

        public Home()
        {
            this.InitializeComponent();

            Pfp.DisplayName = appSettings.DisplayName;
            Name.Text = appSettings.DisplayName;

            new lib.Events("Home Loaded", 0);
        }

        private void AppButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name.Replace("_", " ");

            Navigation.nav.Header = buttonName;

            switch (buttonName)
            {
                case "Notes":
                    Navigation.frame.Navigate(typeof(app.Notes), null, new DrillInNavigationTransitionInfo());
                    Navigation.nav.SelectedItem = Navigation.nav.MenuItems[2];
                    break;
                case "Photo View":
                    Navigation.frame.Navigate(typeof(app.Photo_View), null, new DrillInNavigationTransitionInfo());
                    Navigation.nav.SelectedItem = Navigation.nav.MenuItems[3];
                    break;
                case "Stopwatch":
                    Navigation.frame.Navigate(typeof(app.Stop_Watch), null, new DrillInNavigationTransitionInfo());
                    Navigation.nav.SelectedItem = Navigation.nav.MenuItems[4];
                    break;

                case "Todo":
                    Navigation.frame.Navigate(typeof(app.Todo), null, new DrillInNavigationTransitionInfo());
                    Navigation.nav.SelectedItem = Navigation.nav.MenuItems[5];
                    break;
                case "Settings":
                    Navigation.frame.Navigate(typeof(Settings), null, new DrillInNavigationTransitionInfo());
                    Navigation.nav.SelectedItem = Navigation.nav.SettingsItem;
                    break;
            };
        }
    }
}
