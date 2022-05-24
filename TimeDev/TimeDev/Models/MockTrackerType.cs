using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TimeDev.Models
{
    public class MockTrackerType : ITrackerType
    {
        public string Name => "Mock";
        public int Index { get; set; }
        public async Task<List<ITaskLocal>> LoadTasks(Tracker tracker)
        {
            return await Task.FromResult(new List<ITaskLocal>()
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
            });
        }

        public Task<HttpStatusCode> SendTask(TaskLocalDto taskLocalDto)
        {
            return Task.FromResult(HttpStatusCode.OK);
        }
    }
}
