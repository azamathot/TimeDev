using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class OtherSettings : INotifyPropertyChanged
    {
        private bool widgetOnTop;
        private string selectedTheme;
        private string selectedLanguage;

        public OtherSettings()
        {
            Themes = new ObservableCollection<string>
            {
                "Light",
                "Dark"
            };
            Languages = new ObservableCollection<string>
            {
                "English",
                "Русский"
            };
        }

        public ObservableCollection<string> Themes { get; }

        public ObservableCollection<string> Languages { get; }

        public string SelectedTheme { get => selectedTheme; set => SetProperty(ref selectedTheme, value); }

        public string SelectedLaguage { get => selectedLanguage; set => SetProperty(ref selectedLanguage, value); }

        public bool WidgetOnTop { get => widgetOnTop; set => SetProperty(ref widgetOnTop, value); }

        #region INotifyPropertyChanged
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
