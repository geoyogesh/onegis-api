using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onegis_api.Models
{
    public class UserMembership
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("memberType")]
        public string MemberType { get; set; }
        [JsonProperty("applications")]
        public int Applications { get; set; }
    }

    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("isInvitationOnly")]
        public bool IsInvitationOnly { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("snippet")]
        public string Snippet { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("phone")]
        public object Phone { get; set; }
        [JsonProperty("sortField")]
        public string SortField { get; set; }
        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
        [JsonProperty("isViewOnly")]
        public bool IsViewOnly { get; set; }
        [JsonProperty("isFav")]
        public bool IsFav { get; set; }
        [JsonProperty("thumbnail")]
        public object Thumbnail { get; set; }
        [JsonProperty("created")]
        public long Created { get; set; }
        [JsonProperty("modified")]
        public long Modified { get; set; }
        [JsonProperty("provider")]
        public object Provider { get; set; }
        [JsonProperty("providerGroupName")]
        public string ProviderGroupName { get; set; }
        [JsonProperty("isReadOnly")]
        public bool IsReadOnly { get; set; }
        [JsonProperty("access")]
        public string Access { get; set; }
        [JsonProperty("capabilities")]
        public List<string> Capabilities { get; set; }
        [JsonProperty("userMembership")]
        public UserMembership UserMembership { get; set; }
    }
}