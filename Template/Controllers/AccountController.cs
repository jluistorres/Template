using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username))
            {
                Session["Usuario"] = username;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("login");
        }
	}
}