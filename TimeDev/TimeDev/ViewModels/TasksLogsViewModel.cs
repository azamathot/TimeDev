using System;
using System.Diagnostics;
using TimeDev.Models;
using TimeDev.Views;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TasksLogsViewModel : BaseViewModel
    {
        private TaskLocalDto _selectedItem;
        public TasksLogsViewModel()
        {
            SubmitCommand = new Command(async () => await TasksService.SubmitTasksLogFiles());
            LoadTasksLogsFilesCommand = new Command(ExecuteLoadItemsCommand);
            ItemTapped = new Command<TaskLocalDto>(OnItemSelected);
        }

        public Command<TaskLocalDto> ItemTapped { get; }
        public Command SubmitCommand { get; }
        public Command LoadTasksLogsFilesCommand { get; }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public TaskLocalDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                TasksService.LoadTaskLogFiles();
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

        async void OnItemSelected(TaskLocalDto item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync(nameof(NewItemPage));

            //await Shell.Current.GoToAsync($"{nameof(TaskEditPage)}?{nameof(TaskEditViewModel.ItemId)}={item.TaskLocal.Id}");
            //await Shell.Current.GoToAsync($"{nameof(TaskEditPage)}?{nameof(TaskEditViewModel.Item)}={item}");
            //await Shell.Current.GoToAsync(nameof(TaskEditPage));
            await Shell.Current.Navigation.PushAsync(new TaskEditPage(item));
            //TaskEditViewModel.

        }
    }
}
