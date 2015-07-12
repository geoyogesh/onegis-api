using Newtonsoft.Json;
using System;

namespace onegis_api.Models
{
    public class Folder
    {
        [JsonProperty("username")]
        public String UserName { get; set; }
        [JsonProperty("id")]
        [JsonConverter(typeof(GuidJsonConverter))]
        public Guid Id { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
        [JsonProperty("created",NullValueHandling=NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }
    }

    public class GuidJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //IEnumerable collectionObj = value as IEnumerable;
            //writer.WriteStartObject();

            //foreach (object currObj in collectionObj)
            //{
            //    writer.WritePropertyName("");
            //    serializer.Serialize(writer, currObj);
            //}
            writer.WriteRawValue("\"" + ((Guid)value).ToString("N") + "\"" );
            //writer.WriteEndObject();
        }
    }
}