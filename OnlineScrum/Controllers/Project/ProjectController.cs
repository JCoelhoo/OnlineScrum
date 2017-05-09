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
                return RedirectToAction("Home", "Dashboard");

            //ViewBag.Project = proj;
            ViewBag.Sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            ViewBag.ProjectName = proj.Name;
            var memberList = (List<string>)proj.DevTeam.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            memberList.Add(proj.ScrumMaster);
            ViewBag.MemberList = memberList;
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

            var result = ProjectManager.AddSprint(sprint, ((Project)proj));

            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.Error = result;
                return RedirectToAction("Create_Sprint", "Project");
            }

            return RedirectToAction("Home", "Project");
        }

        [Route("project/new_member")]
        [HttpPost]
        public ActionResult New_Member(string userEmail)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            ViewBag.Link = "Project";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return RedirectToAction("New_Project", "Dashboard");

            ViewBag.AddMemberError = ProjectManager.AddMember(userEmail, proj.ProjectID);
            var project = ProjectManager.GetProjectByEmail(user.Email);
            var memberList = project.DevTeam.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            memberList.Add(project.ScrumMaster);
            ViewBag.MemberList = memberList;
            return PartialView("MemberList");
        }
    }
}