using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class SprintController : Controller
    {
        [Route("project/sprint/{id:int}")]
        public ActionResult Home(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if(sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);

            ViewBag.Items = SprintManager.GetItemsFromSprint(sprint.Items);
            ViewBag.Sprint = sprint;
            ViewBag.Short = true;
            ViewBag.Meetings = MeetingManager.GetMeetingsByEmail(user.Email, sprint.SprintID).OrderBy(m => m.Time).ToList();
            ViewBag.ScrumMaster = proj.ScrumMaster;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            return View();
        }

        [Route("project/sprint/{id:int}/meetings")]
        public ActionResult Meetings(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);

            ViewBag.Short = false;
            ViewBag.Meetings = MeetingManager.GetMeetingsByEmail(user.Email, sprint.SprintID).OrderBy(m => m.Time).ToList();
            ViewBag.ScrumMaster = proj.ScrumMaster;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            return View();
        }

        [Route("project/sprint/{id:int}/new_item")]
        public ActionResult Create_Item(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            return View();            
        }

        [Route("project/sprint/{id:int}/new_item")]
        [HttpPost]
        public ActionResult Create_Item(int id, Item item)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = SprintManager.AddItem(sprint, item);

            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.Error = result;
                return View();
            }

            return RedirectToAction("Home", "Sprint");
        }

        [Route("project/sprint/{id:int}/create_meeting")]
        [HttpPost]
        public ActionResult Create_Meeting(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            if (!ModelState.IsValid)
            {
                ViewBag.Meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();
                ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
                ViewBag.ScrumMaster = proj.ScrumMaster;
                ViewBag.Short = false;
                return PartialView("MeetingList");
            }

            ViewBag.Error = MeetingManager.AddMeeting(meeting, id);
            ViewBag.Meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.ScrumMaster = proj.ScrumMaster;
            ViewBag.Short = false;
            //return RedirectToAction("Home", "Sprint");
            return PartialView("MeetingList");
        }

        [Route("project/sprint/{id:int}/answer_questions")]
        [HttpPost]
        public ActionResult Answer_Questions(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Error = MeetingManager.AddMeetingQuestions(id, meeting);
            return PartialView("Error");
        }
    }
}