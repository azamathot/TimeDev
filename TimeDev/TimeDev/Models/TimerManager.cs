using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using Xamarin.Forms;

namespace TimeDev.Models
{
    public class TimerManager : INotifyPropertyChanged
    {
        const int intervalTime = 1000;

        private TimeSpan duration;
        private Stopwatch Stopwatch;
        private bool isRunning;
        private string status;
        private TaskLocal taskLocal;
        private Timer mainTimer;

        public Command StopCommand { get; }
        public Command StartCommand { get; }
        public Command TimerStartStopCommand { get; }

        public DateTime? BeginTimeTask { get; set; }
        public bool IsRunning { get => isRunning; set => SetProperty(ref isRunning, value); }
        public TimeSpan Duration { get => duration + Stopwatch.Elapsed; set => SetProperty(ref duration, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }

        public void Start()
        {
            Stopwatch.Restart();
            IsRunning = true;
            if (BeginTimeTask is null)
                BeginTimeTask = DateTime.Now;

            mainTimer.Start();
            //SaveTimer?.Start();

        }
        public void Stop()
        {
            Stopwatch.Stop();
            IsRunning = false;
            duration += Stopwatch.Elapsed;
            Stopwatch.Reset();

            mainTimer.Stop();
            if (!(taskLocal is null))
            {
                taskLocal.BeginTimeTask = BeginTimeTask;
                taskLocal.Duration = Duration;
            }
            //SaveTimer?.Stop();
            SaveTimeEvent?.Invoke(Duration, null);
        }

        public void UpdateTask(TaskLocal task)
        {
            Stop();
            taskLocal = task;
            Duration = taskLocal.Duration;
            BeginTimeTask = taskLocal.BeginTimeTask;
            Status = task.ToString();
        }

        public void UpdateSettings(DateTime? beginTimeTask, TimeSpan duration, string status)
        {
            BeginTimeTask = beginTimeTask;
            Duration = duration;
            Status = status;
        }

        public event EventHandler SaveTimeEvent;

        private void TimerCommand()
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
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
        #endregion
    }
}
