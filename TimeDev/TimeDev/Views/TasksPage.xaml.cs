using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage
    {
        TasksViewModel _viewModel;
        public TasksPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TasksViewModel((TimerViewModel)MyTimer.BindingContext);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void TimerView_SaveTimeEvent(object sender, System.EventArgs e)
        {
            var context = (TimerViewModel)MyTimer.BindingContext;
            //TimeLabel.Text = context.Duration.ToString("");
        }
    }
}