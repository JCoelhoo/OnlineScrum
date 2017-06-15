using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class DashboardController : Controller
    {
        [Route("dashboard")]
        [Route("home")]
        [HttpGet]
        public ActionResult Home()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            ViewBag.Link = "Home";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return View("New_Project");

            ViewBag.Projects = ProjectManager.GetProjectsByEmail(user.Email);
            Session["Meetings"] = null;
            return View();
        }

        [Route("new_project")]
        [HttpGet]
        public ActionResult Create_Project()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            ViewBag.Link = "Home";

            return View();
        }

        [Route("new_project")]
        [HttpPost]
        public ActionResult Create_Project(Project project)
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            ViewBag.Link = "Home";

            if (!ModelState.IsValid)
            {
                return View();
            }
            if ("ScrumMaster" != UserManager.GetUserByEmail(user.Email).Role)
            {
                ViewBag.Error = "Only Scrum Masters can create Projects";
                return View();
            }

            var result = ProjectManager.AddProject(project, user.Email);
            ViewBag.Error = result;
            if (!String.IsNullOrEmpty(result)) return View();


            Session["Project"] = project;

            return RedirectToAction("Home", "Dashboard");
        }
    }
}