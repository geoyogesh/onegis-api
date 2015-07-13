using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using onegis_api.Utils;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using onegis_api.Models;

namespace onegis_api.Controllers
{
    public class UsersController : ApiController
    {
        [AllowAnonymous]
        [Route("sharing/rest/content/users/{username}/createFolder")]
        [HttpPost]
        public async Task<Dictionary<string, Object>> CreateFolder(HttpRequestMessage request, string username)
        {
            var folderkey = "folderName";
            var getq = await request.GetFormData();
            if (getq.ContainsKey(folderkey))
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLStore"].ToString()))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "[dbo].[SP_CREATE_FOLDER]",
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@USERNAME",
                        SqlDbType = SqlDbType.VarChar,
                        Value = username,
                        Direction = ParameterDirection.Input
                    });

                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@FOLDERNAME",
                        SqlDbType = SqlDbType.VarChar,
                        Value = getq[folderkey],
                        Direction = ParameterDirection.Input
                    });
                    var guid = Guid.NewGuid();
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@GUID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        Value = guid,
                        Direction = ParameterDirection.Input
                    });


                    //command.Parameters.AddWithValue("@USERNAME",username);
                    //command.Parameters.AddWithValue("@FOLDERNAME", getq[folderkey]);

                    var parameter2 = new SqlParameter()
                    {
                        ParameterName = "@DATETIME",
                        SqlDbType = SqlDbType.SmallDateTime,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(parameter2);

                    connection.Open();
                    var result_count = command.ExecuteNonQueryAsync();
                    connection.Close();
                        return new Dictionary<string, Object>
                        {
                            {"success",true},
                            {"folder",new Folder(){Created=null,Id=(Guid)guid,Title=getq[folderkey],UserName=username}}
                        };
                }
            }
            return new Dictionary<string, Object>
                        {
                            {"success",false}
                        };
        }


    }
}
