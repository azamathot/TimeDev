using System;
using TimeDev.Models;
using TimeDev.Resx;
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

            if (Device.RuntimePlatform != Device.UWP)
            {
                Resource.Culture = DependencyService.Get<ILocalize>()
                                    .GetCurrentCultureInfo();
            }

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataStoreTask>();
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
