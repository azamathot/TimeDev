using Newtonsoft.Json;
using System.Collections.Generic;

namespace TimeDev.Models.Dto
{
    public class ResponceTasksDto
    {
        [JsonProperty("tasks")]
        public List<ManageEngineTaskTaskTypeDto> Tasks { get; set; }
        //[JsonProperty("list_info")]
        //public ListInfo ListInfo { get; set; }
    }
}
