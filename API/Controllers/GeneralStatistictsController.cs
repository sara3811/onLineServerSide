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
    public class GeneralStatistictsController : ApiController
    {
        public IHttpActionResult GetServices(int id)
        {
            try
            {
                return Ok(BL.BusinessBL.GetAvgForBusiness(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        public IHttpActionResult GetInformation()
        {
            try
            {
                return Ok(BL.services.onLineInformation.GetGeneralInformation());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
