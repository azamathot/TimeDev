using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeDev.Models;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private TaskLocal _selectedItem;
        private TimerViewModel _timerVM;

        public ObservableCollection<TaskLocal> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SaveChangesCommand { get; }
        public Command<TaskLocal> ItemTapped { get; }

        public TasksViewModel(TimerViewModel timerView)
        {
            Title = "Tasks";
            Items = new ObservableCollection<TaskLocal>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SaveChangesCommand = new Command(async () => await DataStoreTask.SaveChanges());

            ItemTapped = new Command<TaskLocal>(OnItemSelected);
            _timerVM = timerView;
            //AddItemCommand = new Command(OnAddItem);
        }

        private void SelectedItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DataStoreTask.SaveChanges();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStoreTask.GetItemsAsync(true);
                //IEnumerable<TaskLocal> tasksMESD = await DataStoreTask.GetItemsAsync(true);
                //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskLocal, TaskViewModel>()).CreateMapper();
                ////Items = mapper.Map<IEnumerable<TaskMESD>, ObservableCollection<TaskViewModel>>(tasksMESD);
                //var items = mapper.Map<IEnumerable<TaskLocal>, ObservableCollection<TaskViewModel>>(tasksMESD);

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public TaskLocal SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                if (!Equals(_selectedItem, value))
                    OnItemSelected(value);
            }
        }

        //private async void OnAddItem(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(NewItemPage));
        //}

        void OnItemSelected(TaskLocal item)
        {
            if (item == null)
                return;
            //await Shell.Current.CurrentPage.DisplayAlert("Selected", item.Description, "close");
            _timerVM.UpdateTask(item);

            if (!Equals(_selectedItem, item))
            {
                SelectedItem = item;
            }

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

    }
}
