using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onegis_api.Models
{
    public class Folder
    {
        [JsonProperty("username")]
        public String UserName { get; set; }
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}