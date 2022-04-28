using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class Tracker : INotifyPropertyChanged
    {
        string apikey;
        string instance;
        string trackerType;
        string groupBy;
        string sortBy;

        public Tracker(string instance)
        {
            this.instance = instance;
        }

        public string APIkey { get => apikey; set => SetProperty(ref apikey, value); }
        public string Instance { get => instance; set => SetProperty(ref instance, value); }
        public string TrackerType { get => trackerType; set => SetProperty(ref trackerType, value); }
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
