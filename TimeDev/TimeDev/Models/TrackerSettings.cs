using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class TrackerSettings : INotifyPropertyChanged
    {
        string apikey;
        string selectedInstance;
        string selectedTrackerType;
        string selectedGroupBy;
        string selectedSortBy;

        public TrackerSettings()
        {
            TrackerTypeList = new ObservableCollection<string>
            {
                "ManageEngine 10",
                "Atlassian JIRA"
            };

            GroupByList = new ObservableCollection<string>
            {
                "Task type",
                "Task status"
            };

            SortByList = new ObservableCollection<string>
            {
                "Task number (a-z)",
                "Task number (z-a)",
                "Description (a-z)",
                "Description (z-a)"
            };
        }

        public ObservableCollection<string> InstanceList { get; set; }
        public ObservableCollection<string> TrackerTypeList { get; set; }
        public ObservableCollection<string> GroupByList { get; set; }
        public ObservableCollection<string> SortByList { get; set; }
        public string APIkey { get => apikey; set => SetProperty(ref apikey, value); }
        public string SelectedInstance { get => selectedInstance; set => SetProperty(ref selectedInstance, value); }
        public string SelectedTrackerType { get => selectedTrackerType; set => SetProperty(ref selectedTrackerType, value); }
        public string SelectedGroupBy { get => selectedGroupBy; set => SetProperty(ref selectedGroupBy, value); }
        public string SelectedSortBy { get => selectedSortBy; set => SetProperty(ref selectedSortBy, value); }

        #region INotifyPropertyChanged Members
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
