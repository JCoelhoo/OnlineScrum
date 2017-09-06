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
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            proj = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            //ViewBag.Project = proj;
            ViewBag.Sprints = ProjectManager.GetSprintFromProject(proj.Sprints).OrderByDescending(m => m.FinishDate)
                .ThenBy(x => x.StartDate).ToList();
            var dict = new Dictionary<Sprint, List<Item>>();
            foreach (var sprint in ((List<Sprint>)ViewBag.Sprints).OrderBy(m => m.FinishDate))
            {
                dict.Add(sprint, SprintManager.GetItemsFromSprint(sprint.Items));
            }
            ViewBag.ItemsSprints = dict;
            ViewBag.ProjectName = proj.Name;
            var memberList = SharedManager.SplitString(proj.DevTeam);
            memberList.Insert(0, proj.ScrumMaster);
            ViewBag.MemberList = memberList;
            Session["Project"] = proj;
            Session["Meetings"] = MeetingManager.GetMeetingsByEmail(user.Email, -1, proj.ProjectID);
            return View();
        }

        [Route("project")]
        [HttpPost]
        public ActionResult HomePost(string projectName)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            Project proj = null;
            if (projectName == null && (Project)Session["Project"] == null)
                return RedirectToAction("Dashboard", "Dashboard");
            else if ((Project)Session["Project"] != null && projectName == null)
            {
                proj = ProjectManager.GetProjectsByEmail(user.Email).First(p => p.Name == ((Project)Session["Project"]).Name);
                Session["Project"] = proj;
            }
            else if ((Project)Session["Project"] == null && projectName != null)
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
                return RedirectToAction("Dashboard", "Dashboard");

            return RedirectToAction("Home", "Project");
        }

        [Route("project/items")]
        public ActionResult Items()
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            proj = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            var sprint = new Dictionary<Sprint, List<Item>>();
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            foreach (var s in sprints)
            {
                sprint.Add(s, SprintManager.GetItemsFromSprint(s.Items).OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList());
            }
            ViewBag.Sprints = sprint;
            ViewBag.SprintlessItems = ProjectManager.GetSprintlessItems(proj);
            ViewBag.SprintsAvailable = sprint.Keys.ToList();
            ViewBag.User = user;
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
                return RedirectToAction("Dashboard", "Dashboard");

            if (!ModelState.IsValid)
            {
                return View("Items");
            }

            ProjectManager.ChangeSprintInItem(item, proj.ProjectID);


            var sprint = new Dictionary<Sprint, List<Item>>();
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            foreach (var s in sprints)
            {
                sprint.Add(s, SprintManager.GetItemsFromSprint(s.Items).OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList());
            }
            ViewBag.Sprints = sprint;
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

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
                return RedirectToAction("Dashboard", "Dashboard");

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
                return RedirectToAction("Dashboard", "Dashboard");

            if (!ModelState.IsValid)
            {
                return View();
            }
            item.SprintlessProjectID = proj.ProjectID;
            SprintManager.AddItem(null, item);
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            return RedirectToAction("Items", "Project");
        }

        [Route("project/create_sprint")]
        public ActionResult Create_Sprint()
        {
            var user = (User)Session["UserInfo"];
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
            var user = (User)Session["UserInfo"];
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
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            if (string.IsNullOrEmpty(result))
                return RedirectToAction("Home", "Project");

            ViewBag.Error = result;
            return View();
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

            if (!ModelState.IsValid)
            {
                return PartialView("MemberList");
            }

            ViewBag.AddMemberError = ProjectManager.AddMember(userEmail, proj.ProjectID);
            var project = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            var memberList = SharedManager.SplitString(project.DevTeam);
            memberList.Insert(0, proj.ScrumMaster);
            ViewBag.MemberList = memberList;
            SharedManager.RepeatMethod = false;
            Session["Project"] = project;
            return PartialView("MemberList");
        }
    }
}