using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Web.Http;
using onegis_api.Models;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace onegis_api.Controllers
{
    public class PortalsController : ApiController
    {
        [Route("sharing/rest/portals/self")]
        [HttpGet]
        public String Self()
        {
            return "hi";
        }



        [AllowAnonymous]
        [Route("sharing/rest/portals/languages")]
        [HttpGet]
        public String GetLanguages()
        {
            //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect();
            //IDatabase cache = connection.GetDatabase();

            //String
            //cache.StringSet("i",1);
            //Console.WriteLine(cache.StringGet("i"));

            //Object
            //cache.StringSet("i",JsonConvert.SerializeObject(contact));
            //JsonConvert.DeserializeObject<Object>

            //using (var context = new OneGISDBEntities())
            //{
            //    //var L2EQuery = context.LANGUAGES.where(s => s.StudentName == "Bill");

            //    //var student = L2EQuery.FirstOrDefault<Student>();

            //}

            return "hi";
        }

        [AllowAnonymous]
        [Route("sharing/rest/portals/regions")]
        [HttpGet]
        public List<RegionModel> GetRegions(string culture = "en")
        {
            culture = culture.ToLower();
            var key = "urn:regions:" + culture;

            using (var cachecon = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["CacheDB"].ToString()))
            {
                IDatabase cache = cachecon.GetDatabase();
                if (cache.KeyExists(key))
                {
                    return JsonConvert.DeserializeObject<List<RegionModel>>(cache.StringGet(key));
                }
            }

            var regions = new List<RegionModel>(80);
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLStore"].ToString()))
            {
                var command = new SqlCommand("SELECT [NAME],[REGION],[LOCALIZEDNAME] FROM [dbo].[REGIONS] WHERE [CULTURE]=@lang;", connection);
                var parameter = new SqlParameter()
                {
                    ParameterName = "@lang",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = culture
                };
                command.Parameters.Add(parameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        regions.Add(new RegionModel(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                reader.Close();
            }
            if (regions.Count == 0)
            {
                using (var cachecon = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["CacheDB"].ToString()))
                {
                    IDatabase cache = cachecon.GetDatabase();
                    if (cache.KeyExists(key))
                    {
                        cache.StringSetAsync(key, JsonConvert.SerializeObject(regions));
                    }
                }
            }

            return regions;
        }

    }
}
