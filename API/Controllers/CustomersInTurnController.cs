using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]

    public class CustomersInTurnController : ApiController
    {//toask: איך אפשר לפנות לא רק בשם ID
        public IHttpActionResult GetNearestTurn(int id)
        {
            try
            {
                return Ok( Cashier.GetNearestTurn(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult GetTurnsToCustomer(HttpRequestMessage request)
        {
            try
            {
                String access_token = request.Headers.Authorization.ToString();
                int custId = Token.GetCustIdFromToken(access_token);
                return Ok(CustInLineBL.GetTurnsToCustomer(custId));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult DeleteTurn(int turnId)
        {
            try
            {
                TurnBL.DeleteTurn(turnId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateTurn([FromBody]DTO.TurnDetailsDTO turn)
        {
            try
            {
                Cashier.CompleteTurn(turn);
                return Ok(Cashier.GetNearestTurn(turn.ServiceId));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
