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
    public class BusinessesController : ApiController
    {
     
        public IHttpActionResult GetBusinesses()
        {
            try
            {
                return Ok(BusinessBL.GetBusinesses());
            }
            catch
            {
                return BadRequest();
            }
        }
    
        public IHttpActionResult GetBusiness(string id)
        {
            try
            {
                return Ok(BL.BusinessBL.GetBusinessByPassword(id));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
