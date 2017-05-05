using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class RegisterController : Controller
    {
        [Route("register")]
        [HttpGet]
        public ActionResult Register(string errorMessage)
        {
            ViewBag.Error = errorMessage;
            return View();
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var status = UserManager.RegisterUser(register);
            if ( status != "" )
            {
                ViewBag.Error = status;
                return RedirectToAction("register", new { errorMessage = status });
            }
            //return RedirectToAction("Dashboard/Lecturer");
            return RedirectToAction("login", "login");
        }
    }
}