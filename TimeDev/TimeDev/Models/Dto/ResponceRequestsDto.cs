using Newtonsoft.Json;
using System.Collections.Generic;

namespace TimeDev.Models.Dto
{
    public class ResponceRequestsDto
    {
        [JsonProperty("requests")]
        public List<ManageEngineRequestTaskTypeDto> Request { get; set; }
        //[JsonProperty("list_info")]
        //public ListInfo ListInfo { get; set; }
    }
}
