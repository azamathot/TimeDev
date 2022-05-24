using System;

namespace TimeDev.Models
{
    public interface ITaskLocal
    {
        string Id { get; set; }
        string DescriptionTask { get; set; }
        string TaskType { get; set; }
        TimeSpan Duration { get; set; }
        DateTimeOffset? BeginTimeTask { get; set; }
        string Comment { get; set; }
    }
}
