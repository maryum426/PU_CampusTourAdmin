using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
//using WebMatrix.WebData;

namespace PU_CampusTour
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool login;
        protected void Application_Start()
        {
          /*  if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "UserName", autoCreateTables: true);
            }*/

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            login = false;

        }
        protected void Session_Start()
        {
            Response.Cookies["Login"].Value = false.ToString();
        }

        protected void Session_End()
        {
           // Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);

            if (Request.Cookies["Login"] != null)
            {
                HttpCookie myCookie = new HttpCookie("Login");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                
            }

            
        }
    }
}