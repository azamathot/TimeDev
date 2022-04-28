using System.Collections.Generic;
using TimeDev.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherSettingsPage : ContentPage
    {
        private AppSettingsViewModel _viewModel;
        public OtherSettingsPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new AppSettingsViewModel();
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Picker picker = sender as Picker;
            string theme = (string)picker.SelectedItem;

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case "Dark":
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case "Light":
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
    }
}