using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class Tracker : INotifyPropertyChanged
    {
        string apikey;
        string instance;
        //ITrackerType trackerType;
        string groupBy;
        string sortBy;
        private int trackerTypeIndex;
        private string url;

        public Tracker(string instance)
        {
            this.instance = instance;
        }

        public string URL { get => url; set => SetProperty(ref url, value); }
        public string APIkey { get => apikey; set => SetProperty(ref apikey, value); }
        public string Instance { get => instance; set => SetProperty(ref instance, value); }
        //public ITrackerType TrackerType { get => trackerType; set => SetProperty(ref trackerType, value); }
        public int TrackerTypeIndex { get => trackerTypeIndex; set => SetProperty(ref trackerTypeIndex, value); }
        public string GroupBy { get => groupBy; set => SetProperty(ref groupBy, value); }
        public string SortBy { get => sortBy; set => SetProperty(ref sortBy, value); }

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
