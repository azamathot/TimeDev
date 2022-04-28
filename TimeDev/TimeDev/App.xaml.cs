using System;
using TimeDev.Services;
using Xamarin.Forms;

namespace TimeDev
{
    public partial class App : Application
    {
        public static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataStoreTask>();
            DependencyService.Register<AppSettingsManager>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
