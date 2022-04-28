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

        void OnItemSelected(TaskLocal item)
        {
            if (item == null)
                return;
            _timerVM.UpdateTask(item);

            if (!Equals(_selectedItem, item))
            {
                SelectedItem = item;
            }
        }

    }
}
