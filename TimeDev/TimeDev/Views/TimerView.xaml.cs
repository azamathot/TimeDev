using System;
using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerView : ContentView
    {
        private TimerViewModel _viewModel;
        public TimerView()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new TimerViewModel();
            //_viewModel.SaveTimeEvent += (sender, e) => Dispatcher.BeginInvokeOnMainThread(() => SaveTimeEvent?.Invoke(sender, e));
        }
        public event EventHandler SaveTimeEvent;
    }
}