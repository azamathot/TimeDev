using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeDev.Models;
using TimeDev.Services;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SaveChangesCommand { get; }
        public Command<TaskLocal> ItemTapped { get; }
        public Command CommentUpdateCommand { get; }

        public TasksViewModel()
        {
            Title = "Tasks";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SaveChangesCommand = new Command(async () => await TasksService.SaveTaskAsync());
            ItemTapped = new Command<TaskLocal>(OnItemSelected);
            CommentUpdateCommand = new Command(async () =>
            {
                await TasksService.CommentUpdate();
                await TasksService.SaveTaskAsync();
            });
        }

        public ObservableCollection<ITaskLocal> Tasks => TasksService.Tasks;
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                //Tasks?.Clear();
                var items = await TasksService.GetItemsAsync();
                //foreach (var item in items)
                //{
                //    Tasks.Add(item);
                //}
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
            //SelectedItem = null;
        }

        //public TaskLocal SelectedItem => TasksService.SelectedTask;
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        SetProperty(ref _selectedItem, value);
        //        //if (!Equals(_selectedItem, value))
        //        //    OnItemSelected(value);
        //    }
        //}

        void OnItemSelected(TaskLocal item)
        {
            if (item == null)
                return;

            if (!Equals(TasksService.SelectedTask, item))
            {
                TasksService.SelectedTask = item;
            }
        }

    }
}
