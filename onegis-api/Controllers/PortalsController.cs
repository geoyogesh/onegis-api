using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
    }
}
