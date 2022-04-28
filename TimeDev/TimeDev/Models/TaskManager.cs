using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TimeDev.Models
{
    public class TaskManager : INotifyPropertyChanged
    {
        private TaskLocal selectedTask;

        public TaskLocal SelectedTask { get => selectedTask; set => selectedTask = value; }
        public ObservableCollection<TaskLocal> Tasks { get; set; }
        public Command StopCommand { get; }
        public Command StartCommand { get; }

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
