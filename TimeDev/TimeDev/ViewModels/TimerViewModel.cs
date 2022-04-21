using System;
using System.Diagnostics;
using System.Timers;
using TimeDev.Models;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        //const int intervalSave = 6000;
        const int intervalTime = 1000;

        private TimeSpan duration;
        private Stopwatch Stopwatch;
        private bool isRunning;
        private string status;
        private TaskLocal taskLocal;

        private Timer mainTimer;
        //private Timer SaveTimer;

        public TimerViewModel()
        {
            mainTimer = new Timer(intervalTime);
            mainTimer.Elapsed += (sender, e) => OnPropertyChanged("Duration");
            TimerStartStopCommand = new Command(TimerCommand);
            Stopwatch = new Stopwatch();

            //SaveTimer = new Timer(intervalSave);
            //SaveTimer.Elapsed += (sender, e) => SaveTimeEvent?.Invoke(TimeSpan, e);
        }

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
        public Command TimerStartStopCommand { get; }

        //private TimeSpan timeSpan;
        //public TimeSpan TimeSpan { get => timeSpan; set => SetProperty(ref timeSpan, value); }

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
    }
}
