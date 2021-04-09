using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            new Page.lib.Setup();

            if (Boolean.Parse(roamingSettings.Values["sound"].ToString()))
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            else
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;

            this.InitializeComponent();

            frame = Content;
            nav = Nav;

            Content.Navigate(typeof(Page.Home));
            MainPage.nav.SelectedItem = MainPage.nav.MenuItems[1];
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            Nav.Header = args.InvokedItem.ToString();

            if (args.InvokedItem.ToString() == "Home")
                Content.Navigate(typeof(Page.Home));

            // ---

            else if (args.InvokedItem.ToString() == "Notes")
                Content.Navigate(typeof(Page.Applications.File_View));

            else if (args.InvokedItem.ToString() == "Phone Book")
                Content.Navigate(typeof(Page.Applications.Phone_Book));

            else if (args.InvokedItem.ToString() == "Photo View")
                Content.Navigate(typeof(Page.Applications.Photo_View));

            else if (args.InvokedItem.ToString() == "Todo")
                Content.Navigate(typeof(Page.Applications.Todo));

            else if (args.IsSettingsInvoked)
            {
                Content.Navigate(typeof(Page.Settings));
                Nav.Header = "Settings";
            }
        }
    }
}
