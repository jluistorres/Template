using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Script.Serialization;
using Template.Models;

namespace Template
{
    public class SessionHelper
    {
        private const string VarSessionUsuario = "AppUser";

        private static IEnumerable<Claim> Claims
        {
            get
            {
                IEnumerable<Claim> claims = null;
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is ClaimsIdentity)//FormsIdentity | ClaimsIdentity
                {
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    claims = identity.Claims;
                }

                return claims;
            }
        }

        //Retorna el usuario que ha iniciado sesión
        public static AppUser User
        {
            get
            {
                AppUser user = (AppUser)HttpContext.Current.Session[VarSessionUsuario];
                if (user == null)
                {
                    user = new AppUser();
                    IEnumerable<Claim> claims = Claims;

                    if (claims != null)
                    {
                        var userData = claims.SingleOrDefault(c => c.Type == ClaimTypes.UserData);
                        if (userData != null)
                        {
                            user = new JavaScriptSerializer().Deserialize<AppUser>(userData.Value);
                        }
                    }
                }

                return user;
            }

            set
            {
                //Serializamos los datos de usuario
                string userData = new JavaScriptSerializer().Serialize(value);

                //Acutalizamos la data del usuario
                UpdateClaims(new List<string>() { ClaimTypes.UserData },
                    new List<Claim>() { new Claim(ClaimTypes.UserData, userData) });

                //Guardamos un temporal en la sesión para que no desencripte cada vez que solicita los datos de usuario
                HttpContext.Current.Session[VarSessionUsuario] = value;
            }
        }

        //public static bool IsAdministrador
        //{
        //    get
        //    {
        //        return HttpContext.Current.User.IsInRole("Administrador");
        //    }
        //}        

        //Actualiza los claims / intentos
        private static void UpdateClaims(IEnumerable<string> removeListType, IEnumerable<Claim> addList)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            foreach (var type in removeListType)
            {
                //Remove claim
                identity.RemoveClaim(identity.FindFirst(type));
            }

            //Add new claim
            identity.AddClaims(addList);

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                new AuthenticationProperties() { IsPersistent = false, ExpiresUtc = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout) });
        }

        //Determina si la entidad de seguridad actual pertenece al Rol especificado
        //Se ha realizado esta funcion ya que HttpContext.Current.User.IsInRole("Rol") es case-sensitive. Ejm: User.IsInRole("Admin") != User.IsInRole("admin")
        public static bool IsInRole(string role)
        {
            var claims = Claims ?? new List<Claim>();
            var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToLower()).ToList();
            return roles.Any(x => x == role.Trim().ToLower());
        }
    }
}