using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksLogsPage : ContentPage
    {
        TasksLogsViewModel _viewModel;
        public TasksLogsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TasksLogsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}