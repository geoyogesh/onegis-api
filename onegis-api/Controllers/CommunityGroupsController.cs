using onegis_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace onegis_api.Controllers
{
    public class CommunityGroupsController : ApiController
    {
        [AllowAnonymous]
        [Route("sharing/rest/community/groups/{groupid}")]
        [HttpGet]
        public Group GetGroupDetials(string groupid)
        {
            return new Group();
        }
    }
}
