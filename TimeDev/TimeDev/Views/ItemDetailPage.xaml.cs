using System.ComponentModel;
using TimeDev.ViewModels;
using Xamarin.Forms;

namespace TimeDev.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}