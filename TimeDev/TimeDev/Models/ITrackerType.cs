using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TimeDev.Models
{
    public interface ITrackerType
    {
        string Name { get; }
        int Index { get; set; }
        Task<List<ITaskLocal>> LoadTasks(Tracker tracker);
        Task<HttpStatusCode> SendTask(TaskLocalDto taskLocalDto);
    }
}
