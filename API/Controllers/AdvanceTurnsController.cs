using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;
using DTO;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AdvanceTurnsController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetDaysToService(string serviceId, string day = null)
        {
            try
            {
                if (day == null)
                    return Ok(MakeAppointment.GetOptionalDaysPerService(int.Parse(serviceId)));
                else
                    return Ok(MakeAppointment.GetOptionalHoursPerDay(int.Parse(serviceId), (DateTime.Parse(day))));

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public IHttpActionResult BookAppointment(HttpRequestMessage request, [FromBody]TurnDetailsDTO appointment)
        {
            String access_token = request.Headers.Authorization.ToString();
            int custId = Token.GetCustIdFromToken(access_token);
            appointment.CustId = custId;
            try
            {

                return Ok(MakeAppointment.BookAppointment(appointment));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
