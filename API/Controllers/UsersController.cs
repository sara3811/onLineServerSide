﻿using BL;
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
        public IHttpActionResult GetToken(string name, string phone,string notifactionToken)
        {
            try
            {
                Token.AddCustomer(name, phone,notifactionToken);
                return Ok(Token.GetToken(name, phone));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
