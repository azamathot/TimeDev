using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class TaskLocal : INotifyPropertyChanged
    {
        private DateTime? _beginTimeTask;
        private TimeSpan _duration;

        public TaskLocal()
        {
        }
        public TaskLocal(string id, string taskType, string descriptin, TimeSpan duration, DateTime? beginTimeTask = null, bool isChanged = false)
        {
            Id = id;
            TaskType = taskType;
            Description = descriptin;
            _duration = duration;
            _beginTimeTask = beginTimeTask;
            IsChanged = isChanged;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public TimeSpan Duration { get => _duration; set => SetProperty(ref _duration, value); }
        public DateTime? BeginTimeTask { get => _beginTimeTask; set => SetProperty(ref _beginTimeTask, value); }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsChanged { get; set; }

        public override string ToString()
        {
            return $"{Id} {TaskType} {Description} {(int)Duration.TotalHours + Duration.ToString(@"\:mm\:ss")}";
        }
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                IsChanged = true;
                return true;
            }

            return false;
        }
    }
}
