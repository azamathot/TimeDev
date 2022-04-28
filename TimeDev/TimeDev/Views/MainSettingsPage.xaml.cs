using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainSettingsPage : ContentPage
    {
        private AppSettingsViewModel _viewModel;
        public MainSettingsPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new AppSettingsViewModel();
        }
    }
}