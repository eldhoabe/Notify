using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Notify.Helpers
{
    public class BackgroundDispatcherTimer : DispatcherTimer
    {
        #region Constructor

        public BackgroundDispatcherTimer()
        {
            Tick += BackgroundDispatcherTimer_Tick;
        }

        #endregion

        #region Public Properties

        public bool IsReentrant { get; set; }

        public bool IsRunning { get; private set; }

        public Func<Task> TickTask { get; set; }

        #endregion

        #region Event Handler

        private async void BackgroundDispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (TickTask == null) return;

            // previous task hasn't completed
            if (IsRunning && !IsReentrant) return;

            try
            {
                // running the background task
                IsRunning = true;

                await TickTask.Invoke();
            }
            catch (Exception)
            {
                Debug.WriteLine("Background dispatcher timer Task Failed");
            }
            finally
            {
                IsRunning = false;
            }
        }

        #endregion

    }
}
