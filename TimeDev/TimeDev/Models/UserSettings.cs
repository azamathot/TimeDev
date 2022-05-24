using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class UserSettings : INotifyPropertyChanged
    {
        private string server;
        private string username;
        private string password;
        private bool widgetOnTop;
        private string selectedTheme;
        private string selectedLanguage;
        private int selectedInstanceIndex;

        public UserSettings()
        {
            InstanceList = new ObservableCollection<Tracker>();
        }

        public string Server { get => server; set => SetProperty(ref server, value); }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string SelectedTheme { get => selectedTheme; set => SetProperty(ref selectedTheme, value); }
        public string SelectedLaguage { get => selectedLanguage; set => SetProperty(ref selectedLanguage, value); }
        public bool WidgetOnTop { get => widgetOnTop; set => SetProperty(ref widgetOnTop, value); }
        public int SelectedInstanceIndex { get => selectedInstanceIndex; set => SetProperty(ref selectedInstanceIndex, value); }
        public ObservableCollection<Tracker> InstanceList { get; set; }

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
