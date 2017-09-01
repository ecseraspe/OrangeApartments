using System.Web.Http;
using WebApiContrib.Formatting.Jsonp;

namespace OrangeApartments
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
           
            GlobalConfiguration.Configure(WebApiConfig.Register);
       }
    }
}
