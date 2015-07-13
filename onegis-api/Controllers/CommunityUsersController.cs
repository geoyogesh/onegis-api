using onegis_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace onegis_api.Controllers
{

    public class CommunityUsersController : ApiController
    {
        [AllowAnonymous]
        [Route("sharing/rest/community/users/{username}/notifications")]
        [HttpGet]
        public Dictionary<string, Object> GetNotifications(string username)
        {
            return new Dictionary<string, object>
            {
                {"notifications",new List<String>()}
            };
        }

        [AllowAnonymous]
        [Route("sharing/rest/community/users/{username}/tags")]
        [HttpGet]
        public object GetTags(string username)
        {
            return new
            {
                Tags = new List<TagModel>
                {
                    new TagModel{
                        Tag="html",Count=1
                    },new TagModel{
                        Tag="html",Count=1
                    }
                }
            };
        }
    }
}
