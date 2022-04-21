using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TrackerSettingsViewModel
    {
        public TrackerSettingsViewModel()
        {
            AddInstanceCommand = new Command(OnAddInstance);
            InstanceList = new ObservableCollection<string>();
        }

        public Command AddInstanceCommand { get; }
        public ObservableCollection<string> InstanceList { get; set; }
        public ObservableCollection<string> TrackerTypeList { get; set; }
        public ObservableCollection<string> GroupByList { get; set; }
        public ObservableCollection<string> SortByList { get; set; }
        public string APIkey { get; set; }

        private async void OnAddInstance()
        {
            var instance = await Shell.Current.CurrentPage.DisplayPromptAsync("Add Instance", "Name: ");
            InstanceList.Add(instance);
        }
    }
}
