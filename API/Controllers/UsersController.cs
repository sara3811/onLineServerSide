using BL;
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
    public class UsersController : ApiController
    {
        //todo: what is  this  token?
        public IHttpActionResult GetToken(string name, string phone,string token="")
        {
            try
            {
                Token.AddCustomer(name, phone,token);
                return Ok(Token.GetToken(name, phone));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
