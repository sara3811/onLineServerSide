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
        //todo: לחשוב מהו המיקום המתאים לפונקציה
        public IHttpActionResult Get(int businesseId)
        {
            try
            {
                return Ok(BL.BusinessBL.GetAvgForBusiness(businesseId));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
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
