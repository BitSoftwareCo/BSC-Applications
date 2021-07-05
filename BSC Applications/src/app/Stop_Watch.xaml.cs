using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.src.app
{
    public sealed partial class Stop_Watch
    {
        private DispatcherTimer Timer = new DispatcherTimer();
        private int tick = 0;

        private int sec = 0;
        private int min = 0;
        private int hou = 0;

        public Stop_Watch()
        {
            this.InitializeComponent();

            Time.Text = "h:m:s";

            DataContext = this;
            Timer.Tick += Timer_Tick;
            Timer.Interval = TimeSpan.FromSeconds(1);

             MainPage.nav.ItemInvoked += Nav_ItemInvoked;

            new lib.Event("Stopwatch loaded", lib.Event.load);
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
            sec = 0;
            min = 0;
            hou = 0;
            Time.Text = "h:m:s";

            Timer.Stop();
            StartStopIcon.Symbol = Symbol.Play;
        }

        private void Timer_Tick(object sender, object e)
        {
            tick++;
            sec = tick;
            min = tick / 60;
            hou = tick / 3600;

            if (sec == 60) sec = 0;
            if (min == 60) min = 0;

            Time.Text = $"{hou}:{min}:{sec}";
        }

        private void Nav_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            Timer.Stop();
        }
    }
}
