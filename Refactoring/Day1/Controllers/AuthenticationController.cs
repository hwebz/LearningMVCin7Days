using System.Web.Mvc;
using System.Web.Security;
using BusinessEntities;
using BusinessLayer;

namespace Day1.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

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