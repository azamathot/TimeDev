namespace TimeDev.Models.Dto
{
    public class ManageEngineTaskTaskTypeDto : TaskLocal
    {
        public ManageEngineTaskTaskTypeDto()
        {
            TaskType = "task";
        }

        public string Title { get => DescriptionTask; set => DescriptionTask = value; }
        public User Owner { get => User; set => User = value; }
    }
}
