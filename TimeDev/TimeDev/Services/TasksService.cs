using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TimeDev.Models;
using Xamarin.Forms;

namespace TimeDev.Services
{
    public class TasksService : INotifyPropertyChanged
    {
        private TimerService TimerService => DependencyService.Get<TimerService>();
        private AppSettingsManager Settings => DependencyService.Get<AppSettingsManager>();
        private TaskLocal selectedTask;
        private List<ITaskLocal> taskList;

        public TasksService()
        {
            Tasks = new ObservableCollection<ITaskLocal>();
            TaskLogList = new ObservableCollection<TaskLocalDto>();
            //_ = LoadTasks();
        }

        public ObservableCollection<ITaskLocal> Tasks { get; set; }
        public TaskLocal SelectedTask
        {
            get => selectedTask;
            set
            {
                if (!(selectedTask is null))
                {
                    selectedTask.Stoped -= OnTaskStoped;
                    selectedTask.Started -= OnTaskStarted;
                }
                SetProperty(ref selectedTask, value);
                selectedTask.Stoped += OnTaskStoped;
                selectedTask.Started += OnTaskStarted;
                _ = TimerService.SetTask(SelectedTask);
            }
        }

        private void OnTaskStarted()
        {
            SelectedTask.BeginTimeTask = DateTimeOffset.UtcNow;
        }

        public async void OnTaskStoped()
        {
            SelectedTask.Duration = TimerService.Duration;
            await CommentUpdate();
            await SaveTaskAsync();
        }

        public async Task CommentUpdate()
        {
            SelectedTask.Comment = await Shell.Current.DisplayPromptAsync("Add comment to task", "Comment: ", initialValue: SelectedTask.Comment);
        }

        public async Task LoadTasks(Tracker tracker)
        {
            taskList = await Settings.SelectedTrackerType.LoadTasks(tracker);
            Tasks.Clear();
            foreach (var item in taskList)
            {
                Tasks.Add(item);
            }

        }

        private async Task SubmitTask(TaskLocalDto task)
        {
            var tracker = Settings.BaseSettings.TrackerTypeList[task.TrackerTypeId];
            var result = await tracker.SendTask(task);
            if (result == HttpStatusCode.OK)
            {
                File.Delete(TaskFilename(task.TaskLocal));
            }
        }

        public async Task SubmitTasksLogFiles()
        {
            foreach (var item in TaskLogList)
            {
                await SubmitTask(item);
            }
            LoadTaskLogFiles();
        }

        public async Task<ITaskLocal> GetItemAsync(string id)
        {
            return await Task.FromResult(taskList.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ITaskLocal>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(taskList);
        }

        private string TaskFilename(ITaskLocal taskLocal) => Path.Combine(App.folderPath, string.Join("_", taskLocal.Id, taskLocal.BeginTimeTask?.Ticks.ToString(), ".task"));
        public async Task SaveTaskAsync()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(TaskFilename(SelectedTask)))
            {
                await Task.Run(() => serializer.Serialize(sw, new TaskLocalDto(Settings.SelectedInstance.TrackerTypeIndex, SelectedTask, Settings.UserSettings.SelectedInstanceIndex)));
            }
        }

        public async Task UpdateTaskLogAsync(TaskLocalDto taskLocalDto)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(TaskFilename(taskLocalDto.TaskLocal)))
            {
                await Task.Run(() => serializer.Serialize(sw, taskLocalDto));
            }
        }

        public ObservableCollection<TaskLocalDto> TaskLogList { get; set; }

        public void LoadTaskLogFiles()
        {
            JsonSerializer serializer = new JsonSerializer();
            var fileList = Directory.GetFiles(App.folderPath, "*.task");
            TaskLogList.Clear();
            foreach (var file in fileList)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    var task = serializer.Deserialize(sr, typeof(TaskLocalDto));
                    TaskLogList.Add((TaskLocalDto)task);
                }
            }
        }

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
        #endregion

    }
}
