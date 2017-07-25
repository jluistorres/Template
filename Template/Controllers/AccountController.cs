using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Template.Models;

namespace Template.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            if (!string.IsNullOrEmpty(username))
            {
                List<Claim> claims = new List<Claim>();
                // adding following 2 claim just for supporting default antiforgery provider
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));

                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));

                //UserData
                var user = new AppUser()
                {
                    Usuario = username.ToUpper(),                    
                    Nombres = "Jorge Luis",
                    ApellidoPaterno = "Torres",
                    ApellidoMaterno = "Zárate"
                };

                claims.Add(new Claim(ClaimTypes.UserData, new JavaScriptSerializer().Serialize(user)));

                var ident = new ClaimsIdentity(claims.ToArray(), DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.Now.AddMinutes(Session.Timeout)
                }, ident);

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Las credenciales no son válidas para iniciar sesión";
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("login");
        }
	}
}