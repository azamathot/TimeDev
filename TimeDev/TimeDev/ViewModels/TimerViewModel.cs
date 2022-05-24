using TimeDev.Services;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        public TimerService TimerService => DependencyService.Get<TimerService>();
        public Command TimerStartStopCommand { get; }

        public TimerViewModel()
        {
            TimerStartStopCommand = new Command(StartStopTimer);
        }

        private void StartStopTimer()
        {
            if (TimerService.IsRunning)
            {
                TimerService.Stop();
            }
            else
            {
                TimerService.Start();
            }
        }


    }
}
