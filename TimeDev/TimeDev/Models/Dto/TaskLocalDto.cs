namespace TimeDev.Models
{
    public class TaskLocalDto
    {
        public TaskLocalDto(int trackerTypeId, TaskLocal taskLocal, int instanceIndex)
        {
            TrackerTypeId = trackerTypeId;
            TaskLocal = taskLocal;
            InstanceIndex = instanceIndex;
        }

        public int TrackerTypeId { get; set; }
        public int InstanceIndex { get; set; }
        public TaskLocal TaskLocal { get; set; }
    }
}
