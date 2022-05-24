using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using TimeDev.Models;

namespace TimeDev.Services
{
    public class TimerService : INotifyPropertyChanged
    {
        const int intervalTime = 1000;

        private Stopwatch stopwatch;
        private TaskLocal taskLocal;
        private Timer timer;

        public TimerService()
        {
            timer = new Timer(intervalTime);
            timer.Elapsed += (sender, e) => OnPropertyChanged("Duration");
            stopwatch = new Stopwatch();
            taskLocal = new TaskLocal();
        }

        public bool IsRunning { get => stopwatch.IsRunning; }
        public TimeSpan Duration { get => /*taskLocal.Duration + */stopwatch.Elapsed; }
        public string Status { get => taskLocal?.ToString(); }

        public void Start()
        {
            stopwatch.Restart();
            timer.Start();
            taskLocal?.Start();
            OnPropertyChanged("IsRunning");
        }
        public void Stop()
        {
            stopwatch.Stop();
            timer.Stop();
            taskLocal?.Stop();
            stopwatch.Reset();
            OnPropertyChanged("Comment");
            OnPropertyChanged("IsRunning");
        }

        public async Task SetTask(TaskLocal task)
        {
            await Task.Run(Stop);
            taskLocal = task;
            OnPropertyChanged("Duration");
            OnPropertyChanged("Status");
        }

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
