using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Day1.Models;

namespace Day1.Controllers
{
    [AllowAnonymous] // when you set authorize globally, allow user to get login view
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (!ModelState.IsValid) return View("Login");
            var ebl = new EmployeeBusinessLayer();
            if (ebl.IsValidUser(u))
            {
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index", "Employee");
            }
            ModelState.AddModelError("CredentialError", "Invalid Username or Password");
            return View("Login");
        }*/

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (!ModelState.IsValid) return View("Login");
            var ebl = new EmployeeBusinessLayer();
            var status = ebl.GetUserValidity(u);
            var IsAdmin = false;
            switch (status)
            {
                case UserStatus.AuthenticatedAdmin:
                    IsAdmin = true;
                    break;
                case UserStatus.AuthenticatedUser:

                    break;
                default:
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
            }
            FormsAuthentication.SetAuthCookie(u.UserName, false);
            Session["IsAdmin"] = IsAdmin;
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}