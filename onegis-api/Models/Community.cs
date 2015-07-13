using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onegis_api.Models
{
    public class TagModel
    {
        [JsonProperty("tag")]
        public String Tag { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}