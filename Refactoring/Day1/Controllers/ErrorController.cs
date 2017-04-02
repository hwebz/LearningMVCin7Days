using System;
using System.Web.Mvc;

namespace Day1.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            var e = new Exception("Invalid Controller or/and Action Name");
            var eInfo = new HandleErrorInfo(e, "Unknown", "Unknown");
            return View("Error", eInfo);
        }
    }
}