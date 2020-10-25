using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BL.services.NnotificationTimer.StartTimer();
        }
        
        protected void Application_BeginRequest()
        {
           // Response.AddHeader("Access-Control-Allow-Origin", "*");

        }
        

    }
}
