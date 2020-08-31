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
    [EnableCors("*","*","*")]
    public class CategoriesController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {

                return Ok(CategoryBL.GetCategories());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

       
        
    }
}
