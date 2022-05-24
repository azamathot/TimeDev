using System;
using TimeDev.Models;
using TimeDev.ViewModels;
using Xamarin.Forms;

namespace TimeDev.Views
{
    public partial class TaskEditPage : ContentPage
    {
        public TaskEditPage(TaskLocalDto taskLocalDto)
        {
            InitializeComponent();
            BindingContext = new TaskEditViewModel(taskLocalDto);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!TimeSpan.TryParse(e.NewTextValue, out var tmp))
                ((Entry)sender).Text = e.OldTextValue;
        }
    }
}