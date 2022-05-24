using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public delegate void TaskStatusChanged();
    public class TaskLocal : INotifyPropertyChanged, ITaskLocal
    {
        private DateTimeOffset? _beginTimeTask;
        private TimeSpan _duration;
        private string comment;

        public TaskLocal()
        {
        }
        public TaskLocal(string id, string taskType, string descriptin, TimeSpan duration = new TimeSpan(), DateTime? beginTimeTask = null, string comment = "")
        {
            Id = id;
            TaskType = taskType;
            DescriptionTask = descriptin;
            _duration = duration;
            _beginTimeTask = beginTimeTask;
            Comment = comment;
        }

        public string Id { get; set; }
        public string DescriptionTask { get; set; }

        public string TaskType { get; set; }
        public TimeSpan Duration { get => _duration; set => SetProperty(ref _duration, value); }
        public DateTimeOffset? BeginTimeTask { get => _beginTimeTask; set => SetProperty(ref _beginTimeTask, value); }
        public string Comment { get => comment; set => SetProperty(ref comment, value); }
        public User User { get; set; }

        public override string ToString()
        {
            return $"{Id} {TaskType} {DescriptionTask} {(int)Duration.TotalHours + Duration.ToString(@"\:mm\:ss")}";
        }

        public event TaskStatusChanged Started;
        public event TaskStatusChanged Stoped;
        public void Start() => Started?.Invoke();
        public void Stop() => Stoped?.Invoke();

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
