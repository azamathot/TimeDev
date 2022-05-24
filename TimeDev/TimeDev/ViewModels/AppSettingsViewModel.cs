using TimeDev.Models;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class AppSettingsViewModel : BaseViewModel
    {
        private static AppSettingsViewModel instance;

        [System.Obsolete]
        public static AppSettingsViewModel GetInstance()
        {
            return instance ?? (instance = new AppSettingsViewModel());
        }
        [System.Obsolete]
        private AppSettingsViewModel()
        {
            AddInstanceCommand = new Command(OnAddInstance);
            SaveSettingsCommand = new Command(async () => await AppSettings.SaveSettings());
            CheckSettingsCommand = new Command(async () => await AppSettings.SaveSettings());
            CancelChangeCommand = new Command(async () => await AppSettings.LoadSettings());
            ApplySettingsCommand = new Command(AppSettings.ApplySettings);
        }

        public Command AddInstanceCommand { get; }
        public Command SaveSettingsCommand { get; }
        public Command CheckSettingsCommand { get; }
        public Command CancelChangeCommand { get; }
        public Command ApplySettingsCommand { get; }
        private async void OnAddInstance()
        {
            var instance = await Shell.Current.CurrentPage.DisplayPromptAsync("Add Instance", "Name: ");
            AppSettings.UserSettings.InstanceList.Add(new Tracker(instance));
        }
    }
}
