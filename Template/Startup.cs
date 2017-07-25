using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Template.Startup))]

namespace Template
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //AuthenticationMode = AuthenticationMode.Active,                
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //ReturnUrlParameter = "/Home/Index",
                //ExpireTimeSpan = System.TimeSpan.FromHours(12),
                CookieName = ".APPAUTHCOOKIE"
            });

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie); 
        }
    }
}
