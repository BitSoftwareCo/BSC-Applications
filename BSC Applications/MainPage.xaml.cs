using Windows.UI.Xaml.Controls;

namespace BSC_Applications
{

    public sealed partial class MainPage
    {
        private src.lib.AppSettings appSettings = new src.lib.AppSettings();
        public static Frame frame;

        public MainPage()
        {
            this.InitializeComponent();

            frame = Content;

            if (appSettings.New) Content.Navigate(typeof(src.Setup));
            else Content.Navigate(typeof(src.Navigation));

            new src.lib.Events("MainPage Loaded", 0);
        }
    }
}