using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineScrum.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("login")]
        [Route("")]
        public ActionResult Login()
        {
            UserManager.RegisterUser(new Register { Email = "1@g.c", Password = "1", Username = "1" });
            UserManager.RegisterUser(new Register { Email = "2@g.c", Password = "2", Username = "2" });
            UserManager.RegisterUser(new Register { Email = "3@g.c", Password = "3", Username = "3" });
            UserManager.RegisterUser(new Register { Email = "4@g.c", Password = "4", Username = "4" });
            //ProjectManager.AddProject(new Project { Name = "Mock", DevTeamList = new List<string> { "1@g.c" } }, "2@g.c");
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
                var user = UserManager.GetUserByEmail(login.Email);
                user.Password = null;
                Session["UserInfo"] = user;
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

            return View("Login");
        }

        [Route("logout")]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}