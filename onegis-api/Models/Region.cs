using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onegis_api.Models
{
    public class RegionModel
    {
        public RegionModel() {}
        public RegionModel(String Name, String Region, String LocalizedName) 
        {
            this.Name = Name;
            this.Region = Region;
            this.LocalizedName = LocalizedName;
        }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("region")]
        public String Region { get; set; }
        [JsonProperty("localizedName")]
        public String LocalizedName { get; set; }
    }
}