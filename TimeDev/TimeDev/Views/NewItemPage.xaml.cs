using System;
using System.Collections.Generic;
using System.ComponentModel;
using TimeDev.Models;
using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}