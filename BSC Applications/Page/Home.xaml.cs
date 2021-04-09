using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Page
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
                    MainPage.frame.Navigate(typeof(Page.Applications.File_View), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[2];
                    break;
                case "Phone Book":
                    MainPage.frame.Navigate(typeof(Page.Applications.Phone_Book), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[3];
                    break;
                case "Photo View":
                    MainPage.frame.Navigate(typeof(Page.Applications.Photo_View), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[4];
                    break;

                case "Todo":
                    MainPage.frame.Navigate(typeof(Page.Applications.Todo), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.MenuItems[5];
                    break;
                case "Settings":
                    MainPage.frame.Navigate(typeof(Page.Settings), null, new DrillInNavigationTransitionInfo());
                    MainPage.nav.SelectedItem = MainPage.nav.SettingsItem;
                    break;
            }
        }
    }
}
