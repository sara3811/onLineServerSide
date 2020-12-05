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
    public class ServicesController : ApiController
    {
        public IHttpActionResult GetServices(int id)
        {
            try
            {
                return Ok(BL.ServiceBL.GetServicesInformation(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
