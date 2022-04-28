using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TimeDev.Models;

namespace TimeDev.Services
{
    public class AppSettingsManager
    {
        const string filenameBaseSettings = "TimeDev.appsettings.json";
        const string filenameUserSettings = "UserSettings.json";
        public BaseSettings BaseSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public AppSettingsManager()
        {
            _ = LoadSettings();
        }

        public async Task SaveSettings()
        {
            using (var streamWriter = new StreamWriter(Path.Combine(App.folderPath, filenameUserSettings)))
            {
                var jsonString = JsonConvert.SerializeObject(UserSettings);
                var encryptString = Encrypt(jsonString);
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
                    BaseSettings = JsonConvert.DeserializeObject<BaseSettings>(jsonString);
                }
            }

            if (File.Exists(Path.Combine(App.folderPath, filenameUserSettings)))
            {
                using (var streamReadersr = new StreamReader(Path.Combine(App.folderPath, filenameUserSettings)))
                {
                    var encryptString = await Task.FromResult(streamReadersr.ReadToEnd());
                    var jsonString = Decrypt(encryptString);
                    UpdateUserSettings(JsonConvert.DeserializeObject<UserSettings>(jsonString));
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

        private string Key { get; }
        private string Encrypt(string source)
        {
            return source;
        }
        private string Decrypt(string source)
        {
            return source;
        }
    }
}
