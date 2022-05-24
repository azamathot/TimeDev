using System;
using TimeDev.Models;
using Xamarin.Forms;

namespace TimeDev.ViewModels
{
    public class TaskEditViewModel : BaseViewModel
    {
        private TaskLocalDto item;
        private string comment;
        private string duration;

        public TaskLocalDto Item
        {
            get => item;
            set
            {
                item = value;
                Comment = item.TaskLocal.Comment;
                Duration = item.TaskLocal.Duration.ToString(@"hh\:mm\:ss");
            }
        }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public TaskEditViewModel(TaskLocalDto taskLocalDto)
        {
            Item = taskLocalDto;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string TaskType { get => Item?.TaskLocal.TaskType; }
        public string Id { get => Item?.TaskLocal.Id; }
        public string DescriptionTask { get => Item?.TaskLocal.DescriptionTask; }
        public DateTimeOffset? BeginTimeTask { get => Item?.TaskLocal.BeginTimeTask; }
        public string Comment { get => comment; set => SetProperty(ref comment, value); }
        public string Duration
        {
            get => duration;
            set
            {
                if (TimeSpan.TryParse(value, out var tmp))
                    SetProperty(ref duration, value);
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Item?.TaskLocal.Comment)
                && !String.IsNullOrWhiteSpace(Item?.TaskLocal.Duration.ToString());
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            // This will pop the current page off the navigation stack
            Item.TaskLocal.Comment = Comment;
            Item.TaskLocal.Duration = TimeSpan.Parse(Duration);
            await TasksService.UpdateTaskLogAsync(Item);
            await Shell.Current.GoToAsync("..");
        }

    }
}
