using System;
using System.Collections.Generic;
using System.Linq;
using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class ProjectController : Controller
    {
        [Route("project")]
        [HttpGet]
        public ActionResult Home(string projectName)
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            Project proj = null;
            if (projectName == null && (Project) Session["Project"] == null)
                return RedirectToAction("Home", "Dashboard");
            else if ((Project) Session["Project"] != null && projectName == null)
            {
                proj = ProjectManager.GetProjectsByEmail(user.Email).First(p => p.Name == ((Project)Session["Project"]).Name);
                Session["Project"] = proj;
            }
            else if ((Project) Session["Project"] == null && projectName != null)
            {
                proj = ProjectManager.GetProjectsByEmail(user.Email).First(p => p.Name == projectName);
                Session["Project"] = proj;
            }
            else if ((Project)Session["Project"] != null && ((Project)Session["Project"]).Name == projectName)
            {
                proj = ProjectManager.GetProjectsByEmail(user.Email).First(p => p.Name == projectName);
                Session["Project"] = proj;
            }
            else
            {
                var project = (Project)Session["Project"];
                if (project != null && project.Name != projectName)
                {
                    proj = ProjectManager.GetProjectsByEmail(user.Email).First(p => p.Name == projectName);
                    Session["Project"] = proj;
                }
            }

            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");

            //ViewBag.Project = proj;
            ViewBag.Sprints = ProjectManager.GetSprintFromProject(proj.Sprints).OrderByDescending(m => m.FinishDate)
                .ThenBy(x => x.StartDate).ToList();
            ViewBag.ProjectName = proj.Name;
            var memberList = SharedManager.SplitString(proj.DevTeam);
            memberList.Insert(0,proj.ScrumMaster);
            ViewBag.MemberList = memberList;
            Session["Project"] = proj;
            return View();
        }

        [Route("project")]
        [HttpPost]
        public ActionResult HomePost(string projectName)
        {
            return RedirectToAction("Home", "Project", new { projectName = projectName });
        }

        [Route("project/items")]
        public ActionResult Items()
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project) Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");

            var sprint = new Dictionary<Sprint, List<Item>>();
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            foreach (var s in sprints)
            {
                if (s.FinishDate.Date < DateTime.Now.Date) continue;
                sprint.Add(s, SprintManager.GetItemsFromSprint(s.Items).OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList());
            }
            ViewBag.Sprints = sprint;
            ViewBag.SprintlessItems = ProjectManager.GetSprintlessItems();
            ViewBag.SprintsAvailable = sprint.Keys.ToList();
            return View();
        }

        [Route("project/items")]
        [HttpPost]
        public ActionResult Items2(List<SprintItem> item)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");

            ProjectManager.ChangeSprintInItem(item);


            var sprint = new Dictionary<Sprint, List<Item>>();
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            foreach (var s in sprints)
            {
                sprint.Add(s, SprintManager.GetItemsFromSprint(s.Items).OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList());
            }
            ViewBag.Sprints = sprint;

            return RedirectToAction("Items", "Project");
        }

        [Route("project/create_item")]
        public ActionResult Create_Item()
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");

            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            return View();
        }

        [Route("project/create_item")]
        [HttpPost]
        public ActionResult Create_Item(Item item)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");

            SprintManager.AddItem(null, item);

            return RedirectToAction("Items", "Project");
        }

        [Route("project/create_sprint")]
        public ActionResult Create_Sprint()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Create_Project", "Dashboard");

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
            var proj = (Project)Session["Project"];
            if (proj == null)
                return RedirectToAction("Create_Project", "Dashboard");

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = ProjectManager.AddSprint(sprint, proj);

            if (string.IsNullOrEmpty(result))
                return RedirectToAction("Home", "Project");

            ViewBag.Error = result;
            return RedirectToAction("Create_Sprint", "Project");
        }

        [Route("project/new_member")]
        [HttpPost]
        public ActionResult New_Member(string userEmail)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            ViewBag.Link = "Project";
            var proj = (Project)Session["Project"];
            if (proj == null)
                return RedirectToAction("Create_Project", "Dashboard");

            ViewBag.AddMemberError = ProjectManager.AddMember(userEmail, proj.ProjectID);
            var project = ProjectManager.GetProjectByEmail(user.Email);
            var memberList = SharedManager.SplitString(project.DevTeam);
            memberList.Add(project.ScrumMaster);
            ViewBag.MemberList = memberList;
            SharedManager.RepeatMethod = false;
            return PartialView("MemberList");
        }
    }
}