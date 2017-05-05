using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class ProjectController : Controller
    {
        [Route("project")]
        public ActionResult Home()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return View();

            //ViewBag.Project = proj;
            ViewBag.Sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            ViewBag.ProjectName = proj.Name;
            return View();
        }

        [Route("project/create_sprint")]
        public ActionResult Create_Sprint()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("New_Project", "Dashboard");

            return View();
        }

        [Route("project/create_sprint")]
        [HttpPost]
        public ActionResult Create_Sprint(Sprint sprint)
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            ViewBag.Link = "Project";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return RedirectToAction("New_Project", "Dashboard");

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = ProjectManager.AddSprint(sprint, ((Project)proj).ProjectID);

            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.Error = result;
                return RedirectToAction("Create_Sprint", "Project");
            }

            return RedirectToAction("Home", "Project");
        }
    }
}