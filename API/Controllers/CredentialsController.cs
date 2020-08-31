using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]

    public class CredentialsController : ApiController
    {
        public IHttpActionResult Post(/*[FromBody]UserDto usre*/)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
