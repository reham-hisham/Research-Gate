using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;


[assembly: OwinStartup(typeof(IAproject.App_Start.Startup1))]

namespace IAproject.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            CookieAuthenticationOptions cookieAuthentication = new CookieAuthenticationOptions();
            cookieAuthentication.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthentication.LoginPath = new PathString("/Auther/Login");
            app.UseCookieAuthentication(cookieAuthentication);
        }
    }
}
