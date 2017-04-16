using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("login")]
        [Route("")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        [Route("")]
        public ActionResult Login(Login login)
        {
            var status = UserManager.Login(login);
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if (status == UserManager.LoginStatus.RegularUser)
            {
                Session["Type"] = "RegularUser";
                Session["Email"] = login.Email;
                Session["TimeOfCreation"] = DateTime.Now;
                return RedirectToAction("Home", "Dashboard");
            }
            //else if (status == CommonManager.LoginStatus.Student)
            //{
            //    Session["IsStudent"] = true;
            //    Session["Email"] = login.Email;
            //    Session["TimeOfCreation"] = DateTime.Now;
            //    Session["Id"] = StudentManager.GetStudentIdByEmail(login.Email);
            //    return RedirectToAction("Manager", "Dashboard");
            //}

            if (status == UserManager.LoginStatus.Fail) ViewBag.Error = "Invalid credentials";
            else if (status == UserManager.LoginStatus.DBFail) ViewBag.Error = "Database error";

            return RedirectToAction("Login", new { errorMessage = ViewBag.Error });
        }
    }
}