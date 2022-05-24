using Newtonsoft.Json;
using Plugin.DeviceInfo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TimeDev.Models;
using TimeDev.Resx;
using Xamarin.Forms;

namespace TimeDev.Services
{
    public class AppSettingsManager : INotifyPropertyChanged
    {
        const string filenameBaseSettings = "TimeDev.appsettings.json";
        const string filenameUserSettings = "UserSettings.json";
        private Tracker selectedInstance;

        private TasksService TasksService => DependencyService.Get<TasksService>();
        public Tracker SelectedInstance
        {
            get => selectedInstance;
            set
            {
                SetProperty(ref selectedInstance, value);
                if (IsValidateted)
                    _ = TasksService.LoadTasks(SelectedInstance);
            }
        }
        public ITrackerType SelectedTrackerType => BaseSettings.TrackerTypeList[SelectedInstance.TrackerTypeIndex];
        public BaseSettings BaseSettings { get; set; }
        public UserSettings UserSettings { get; set; }

        [System.Obsolete]
        public AppSettingsManager()
        {
            _ = InitializeSettings();
        }

        [System.Obsolete]
        private async Task InitializeSettings()
        {
            //await SaveSettingsBase();
            await LoadSettings();
            ApplySettings();
        }

        public void ApplySettings()
        {
            //SaveSettings().Wait();
            UpdateLanguage();
            UpdateTheme();
        }

        public void UpdateLanguage()
        {
            CultureInfo language = new CultureInfo(UserSettings.SelectedLaguage == "Русский" ? "ru" : "en");
            Thread.CurrentThread.CurrentUICulture = language;
            Resource.Culture = language;
            //Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public void UpdateTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (UserSettings.SelectedTheme)
                {
                    case "Dark":
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case "Light":
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }

        }

        private bool IsValidateted
        {
            get
            {
                var si = SelectedInstance;
                return (si != null &&
                    !string.IsNullOrEmpty(si.Instance) &&
                    !string.IsNullOrEmpty(si.GroupBy) &&
                    !string.IsNullOrEmpty(si.SortBy) &&
                    !string.IsNullOrEmpty(si.APIkey) &&
                    !string.IsNullOrEmpty(si.URL) &&
                    UserSettings != null &&
                    UserSettings.InstanceList != null &&
                    si.TrackerTypeIndex >= 0 &&
                    si.TrackerTypeIndex < UserSettings.InstanceList.Count &&
                    !string.IsNullOrEmpty(UserSettings.Username) &&
                    !string.IsNullOrEmpty(UserSettings.Password) &&
                    !string.IsNullOrEmpty(UserSettings.Server));
            }
        }

        [System.Obsolete]
        public async Task SaveSettings()
        {
            using (var streamWriter = new StreamWriter(Path.Combine(App.folderPath, filenameUserSettings)))
            {
                var jsonString = JsonConvert.SerializeObject(UserSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                });
                var encryptString = Encrypto.Encrypt(jsonString, EncryptionKey);

                await streamWriter.WriteAsync(encryptString);
            }
        }

        [System.Obsolete]
        public async Task SaveSettingsBase()
        {
            BaseSettings = new BaseSettings();
            BaseSettings.TrackerTypeList.Add(new MockTrackerType() { Index = BaseSettings.TrackerTypeList.Count });
            BaseSettings.TrackerTypeList.Add(new ManageEngineTrackerType() { Index = BaseSettings.TrackerTypeList.Count });
            using (var streamWriter = new StreamWriter(Path.Combine(App.folderPath, filenameBaseSettings)))
            {
                var jsonString = JsonConvert.SerializeObject(BaseSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                });
                var encryptString = jsonString;
                await Task.FromResult(streamWriter.WriteAsync(encryptString));
            }
        }

        public async Task LoadSettings()
        {
            var embeddedResourceStream = Assembly.GetAssembly(typeof(BaseSettings)).GetManifestResourceStream(filenameBaseSettings);
            if (embeddedResourceStream != null && BaseSettings is null)
            {
                using (var streamReader = new StreamReader(embeddedResourceStream))
                {
                    var jsonString = await Task.FromResult(streamReader.ReadToEnd());
                    BaseSettings = JsonConvert.DeserializeObject<BaseSettings>(jsonString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                }
            }

            if (File.Exists(Path.Combine(App.folderPath, filenameUserSettings)))
            {
                using (var streamReadersr = new StreamReader(Path.Combine(App.folderPath, filenameUserSettings)))
                {
                    var encryptString = await Task.FromResult(streamReadersr.ReadToEnd());
                    var jsonString = Encrypto.Decrypt(encryptString, EncryptionKey);
                    var userSettings = JsonConvert.DeserializeObject<UserSettings>(jsonString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                    UpdateUserSettings(userSettings);
                }
            }
            else
                UserSettings = new UserSettings();
        }

        private void UpdateUserSettings(UserSettings userSettings)
        {
            if (UserSettings is null)
            {
                UserSettings = userSettings;
                return;
            }
            UserSettings.InstanceList.Clear();
            foreach (var item in userSettings.InstanceList)
            {
                UserSettings.InstanceList.Add(item);
            }
            UserSettings.WidgetOnTop = userSettings.WidgetOnTop;
            UserSettings.Username = userSettings.Username;
            UserSettings.SelectedLaguage = userSettings.SelectedLaguage;
            UserSettings.Server = userSettings.Server;
            UserSettings.Password = userSettings.Password;
            UserSettings.SelectedInstanceIndex = userSettings.SelectedInstanceIndex;
            UserSettings.SelectedTheme = userSettings.SelectedTheme;
            UserSettings.WidgetOnTop = userSettings.WidgetOnTop;
        }

        private string EncryptionKey { get => "AzamatHot12345" + CrossDeviceInfo.Current.Id; }

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
