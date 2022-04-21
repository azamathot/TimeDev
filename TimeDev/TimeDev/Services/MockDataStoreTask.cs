using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeDev.Models;

namespace TimeDev.Services
{
    public class MockDataStoreTask : IDataStore<TaskLocal>
    {
        readonly List<TaskLocal> items;
        const string filename = "TaskTrackerList.txt";

        public MockDataStoreTask()
        {
            items = new List<TaskLocal>()
            {
                new TaskLocal( "1001", "Task", "This is an item description.", new TimeSpan(0,20,0)) ,
                new TaskLocal( "1002", "Task", "This is an item description.", new TimeSpan(1,20,0)) ,
                new TaskLocal( "1003", "Task", "This is an item description.", new TimeSpan(1,50,0)) ,
                new TaskLocal( "1004", "Task", "This is an item description.", new TimeSpan(32,20,0)) ,
                new TaskLocal( "1005", "Task", "This is an item description.", new TimeSpan(0,20,0)) ,
                new TaskLocal( "1006", "Task", "This is an item description.", new TimeSpan(0,20,0)) ,
                new TaskLocal( "1007", "Task", "This is an item description.", new TimeSpan(0,0,0)) ,
                new TaskLocal( "1008", "Task", "This is an item description.", new TimeSpan(25,0,0)) ,
                new TaskLocal( "1009", "Task", "This is an item description.", new TimeSpan(1,0,0)) ,
                new TaskLocal( "1010", "Task", "This is an item description.", new TimeSpan(1,0,0)) ,
            };
        }

        public async Task<bool> AddItemAsync(TaskLocal item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TaskLocal item)
        {
            var oldItem = items.Where((TaskLocal arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((TaskLocal arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TaskLocal> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TaskLocal>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<int> SaveChanges()
        {
            var changesList = items.Where(s => s.IsChanged).ToList();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(Path.Combine(App.folderPath, filename)))
            {
                await Task.Run(() => serializer.Serialize(sw, changesList));
            }
            await LoadFromFile();
            return await Task.FromResult(items.Count);
        }

        public async Task<int> LoadFromFile()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(Path.Combine(App.folderPath, filename)))
            {
                var taskList = await Task.FromResult(serializer.Deserialize(sr, typeof(List<TaskLocal>)));
                items.Clear();
                items.AddRange((List<TaskLocal>)taskList);
            }
            return await Task.FromResult(items.Count);
        }

    }
}
