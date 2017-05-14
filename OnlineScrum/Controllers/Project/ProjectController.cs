using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
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
            var memberList = SharedManager.SplitString(proj.DevTeam);
            memberList.Insert(0,proj.ScrumMaster);
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
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return RedirectToAction("Create_Project", "Dashboard");

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = ProjectManager.AddSprint(sprint, ((Project)proj));

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
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return RedirectToAction("Create_Project", "Dashboard");

            ViewBag.AddMemberError = ProjectManager.AddMember(userEmail, proj.ProjectID);
            var project = ProjectManager.GetProjectByEmail(user.Email);
            var memberList = SharedManager.SplitString(project.DevTeam);
            memberList.Add(project.ScrumMaster);
            ViewBag.MemberList = memberList;
            return PartialView("MemberList");
        }
    }
}