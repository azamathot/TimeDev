using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackerSettingsPage : ContentPage
    {
        private AppSettingsViewModel _viewModel;

        [System.Obsolete]
        public TrackerSettingsPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = AppSettingsViewModel.GetInstance();
        }
    }
}