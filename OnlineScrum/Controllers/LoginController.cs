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
            UserManager.RegisterUser(new Register { Email = "jk@gmail.com", Password = "jk", Username = "JK" });
            UserManager.RegisterUser(new Register { Email = "a@gmail.com", Password = "a", Username = "A" });
            UserManager.RegisterUser(new Register { Email = "b@gmail.com", Password = "b", Username = "B" });
            //ProjectManager.AddProject(new Project { Name = "Mock", DevTeam = "a@gmail.com" }, "jk@gmail.com");
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
                Session["UserInfo"] = UserManager.getUserByEmail(login.Email);
                Session["Project"] = ProjectManager.getProjectByEmail(login.Email);
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