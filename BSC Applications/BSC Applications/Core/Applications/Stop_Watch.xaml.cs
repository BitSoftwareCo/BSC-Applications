using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.Core.Applications
{
    public sealed partial class Stop_Watch : Page
    {
        private DispatcherTimer Timer = new DispatcherTimer();
        private int tick = 0;

        public Stop_Watch()
        {
            this.InitializeComponent();

            Time.Text = "0";

            DataContext = this;
            Timer.Tick += Timer_Tick;
            Timer.Interval = TimeSpan.FromSeconds(1);

            MainPage.nav.ItemInvoked += Nav_ItemInvoked;
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (StartStopIcon.Symbol == Symbol.Play)
            {
                Timer.Start();
                StartStopIcon.Symbol = Symbol.Pause;
            }
            else
            {
                Timer.Stop();
                StartStopIcon.Symbol = Symbol.Play;
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            tick = 0;
            Time.Text = $"0";
        }

        private void Timer_Tick(object sender, object e)
        {
            tick++;
            Time.Text = $"{tick}";
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            Timer.Stop();
        }
    }
}
