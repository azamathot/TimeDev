using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    internal class Settings : INotifyPropertyChanged
    {
        string server;
        string username;
        string password;

        string apikey;
        string selectedInstance;
        string selectedTrackerType;
        string selectedGroupBy;
        string selectedSortBy;

        private bool widgetOnTop;
        private string selectedTheme;
        private string selectedLanguage;

        public string Server { get => server; set => SetProperty(ref server, value); }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        public ObservableCollection<Tracker> InstanceList { get; set; }
        public ObservableCollection<string> TrackerTypeList { get; set; }
        public ObservableCollection<string> GroupByList { get; set; }
        public ObservableCollection<string> SortByList { get; set; }
        public string APIkey { get => apikey; set => SetProperty(ref apikey, value); }
        public string SelectedInstance { get => selectedInstance; set => SetProperty(ref selectedInstance, value); }
        public string SelectedTrackerType { get => selectedTrackerType; set => SetProperty(ref selectedTrackerType, value); }
        public string SelectedGroupBy { get => selectedGroupBy; set => SetProperty(ref selectedGroupBy, value); }
        public string SelectedSortBy { get => selectedSortBy; set => SetProperty(ref selectedSortBy, value); }

        public ObservableCollection<string> Themes { get; }

        public ObservableCollection<string> Languages { get; }

        public string SelectedTheme { get => selectedTheme; set => SetProperty(ref selectedTheme, value); }

        public string SelectedLaguage { get => selectedLanguage; set => SetProperty(ref selectedLanguage, value); }

        public bool WidgetOnTop { get => widgetOnTop; set => SetProperty(ref widgetOnTop, value); }

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
