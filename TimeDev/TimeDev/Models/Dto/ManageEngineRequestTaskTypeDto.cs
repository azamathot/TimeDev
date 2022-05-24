namespace TimeDev.Models.Dto
{
    public class ManageEngineRequestTaskTypeDto : TaskLocal
    {
        public ManageEngineRequestTaskTypeDto() : base()
        {
            TaskType = "request";
        }

        public string Subject { get => DescriptionTask; set => DescriptionTask = value; }
        public User Technician { get => User; set => User = value; }

    }
}
