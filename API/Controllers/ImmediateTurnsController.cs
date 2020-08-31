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
    public class ImmediateTurnsController : ApiController
    {

        public IHttpActionResult GetImmediateTurn(HttpRequestMessage request, string latitude, string longitude, string isDriving, string categoryId = null, string serviceId = null)
        {
            try
            {
                String access_token = request.Headers.Authorization.ToString();
                int custId = Token.GetCustIdFromToken(access_token);
                if (categoryId != null)
                    return Ok(FindOptionalTurns.GetPossibleBusinessesWithHour(int.Parse(categoryId), latitude, longitude, bool.Parse(isDriving), custId));
                else
                    return Ok(FindOptionalTurns.GetPossibleBusinessWithHour(int.Parse(serviceId), latitude, longitude, bool.Parse(isDriving), custId));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      

        [HttpPut]
        public IHttpActionResult ConfirmTurn([FromBody]TurnDetailsDTO turn)
        {
            try
            {
                return Ok(ImmediateTurn.ConfirmImmediateTurn(turn));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }

}
