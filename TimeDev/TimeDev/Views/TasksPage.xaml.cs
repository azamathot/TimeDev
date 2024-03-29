﻿using TimeDev.ViewModels;
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
            BindingContext = _viewModel = new TasksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}