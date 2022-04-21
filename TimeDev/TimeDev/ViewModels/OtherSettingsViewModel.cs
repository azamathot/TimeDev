using System.Collections.ObjectModel;

namespace TimeDev.ViewModels
{
    public class OtherSettingsViewModel : BaseViewModel
    {
        public OtherSettingsViewModel()
        {
            Themes = new ObservableCollection<string>
            {
                "Light",
                "Dark"
            };
        }

        public ObservableCollection<string> Themes { get; }

        private System.Collections.IList language;

        public System.Collections.IList Language { get => language; set => SetProperty(ref language, value); }

        private bool isChecked;

        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
    }
}
