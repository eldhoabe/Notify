using Hardcodet.Wpf.TaskbarNotification;
using Notify.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Notify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal BackgroundDispatcherTimer backgroundTimer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartTimer();
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        /// <summary>
        /// On state changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState.Minimized == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        #region Private methods

        private void StartTimer()
        {
            backgroundTimer = new BackgroundDispatcherTimer();
            backgroundTimer.IsReentrant = false;
            backgroundTimer.Interval = TimeSpan.FromSeconds(int.Parse(IntervalTextBox.Text));
            backgroundTimer.TickTask = async () =>
            {
                try
                {
                    //await Task.Run(() => DatabaseVersionBLInstance.GetLatestDatbaseVersion());
                    //IsConnectionLostNotified = false;
                    ShowContent("Hi", "dfs", BalloonIcon.Info);
                }
                catch (Exception)
                {


                    throw;
                }
            };
            backgroundTimer.Start();
        }

        private void ShowContent(string title, string content, BalloonIcon iconType)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.myNotifyIcon.ShowBalloonTip(title, content, iconType);
        }
        
        #endregion
        

    }
}
