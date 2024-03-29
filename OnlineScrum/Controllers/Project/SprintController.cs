﻿using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);


            var DailyTime = "08:00:00";
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            SharedManager.DailyScrumMeeting(proj, sprint, false);
            var meetings = MeetingManager.GetMeetingsByEmail(user.Email, sprint.SprintID);
            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => SharedManager.DailyScrumMeeting(proj, sprint));

            ViewBag.Items = SprintManager.GetItemsFromSprint(sprint.Items);
            ViewBag.Sprint = sprint;
            ViewBag.Short = true;
            ViewBag.Meetings = meetings.Where(m => m.Time > DateTime.Now).Take(5).OrderBy(m => m.Time).ToList();
            ViewBag.ScrumMaster = proj.ScrumMaster;
            ViewBag.Type = user.Role;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.Email = user.Email;
            return View();
        }

        [Route("project/sprint/{id:int}/meetings")]
        public ActionResult Meetings(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            proj = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);

            SharedManager.DailyScrumMeeting(proj, sprint, false);

            ViewBag.Short = false;
            ViewBag.Meetings = MeetingManager.GetMeetingsByEmail(user.Email, sprint.SprintID).OrderBy(m => m.Time).ToList();
            ViewBag.ScrumMaster = proj.ScrumMaster;
            ViewBag.Type = user.Role;
            ViewBag.Error = null;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            return View();
        }

        [Route("project/sprint/{id:int}/items")]
        public ActionResult Items(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            proj = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);
            ViewBag.Sprint = sprint;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.Items = SprintManager.GetItemsFromSprint(sprint.Items);//.OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList();
            return View();
        }

        [Route("project/sprint/{id:int}/statistics")]
        public ActionResult Statistics(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            proj = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
            if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }
            var sprint = sprints.First(m => m.SprintID == id);
            ViewBag.Sprint = sprint;
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.Items = SprintManager.GetItemsFromSprint(sprint.Items).OrderByDescending(m => m.AssignedTo == user.Email).ThenBy(m => m.ItemStatus).ToList();
            return View();
        }

        //[Route("project/sprint/{id:int}/settings")]
        //public ActionResult Settings(int id)
        //{
        //    var user = (User)Session["UserInfo"];
        //    if (user == null)
        //        return RedirectToAction("Login", "Login");
        //    var proj = (Project)Session["Project"];
        //    ViewBag.Link = "Project";
        //    if (proj == null)
        //        return RedirectToAction("Dashboard", "Dashboard");
        //    var sprints = ProjectManager.GetSprintFromProject(proj.Sprints);
        //    if (sprints == null || sprints.Count(m => m.SprintID == id) == 0)
        //    {
        //        ViewBag.Error = "Sprint not found";
        //        return RedirectToAction("Home", "Project");
        //    }

        //    return View();
        //}

        [Route("project/sprint/{id:int}/new_item")]
        public ActionResult Create_Item(int id)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
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
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
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
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            return RedirectToAction("Items", "Sprint");
        }

        [Route("project/sprint/{id:int}/create_meeting")]
        [HttpPost]
        public ActionResult Create_Meeting(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Type = user.Role;
            ViewBag.Short = false;
            ViewBag.Meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.ScrumMaster = proj.ScrumMaster;

            if (!ModelState.IsValid)
            {
                return PartialView("MeetingList");
            }

            if (meeting.Time.Date > sprint.FinishDate)
            {
                ViewBag.Error = "Meeting date after sprint finish date";
                return PartialView("MeetingList");
            }

            meeting.ProjectID = proj.ProjectID;
            if (meeting.Developer == "everyone@everyone.os")
            {
                meeting.Developer = "," + proj.DevTeam + ",";
                ViewBag.Error = MeetingManager.AddMeeting(meeting, id);
            }
            else
            {
                ViewBag.Error = MeetingManager.AddMeeting(meeting, id);
            }
            
            var meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();

            Session["Meetings"] = meetings;
            ViewBag.Meetings = meetings;
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            return PartialView("MeetingList");
        }

        [Route("project/sprint/{id:int}/remove_meeting")]
        [HttpPost]
        public ActionResult Remove_Meeting(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Type = user.Role;
            ViewBag.Short = false;
            ViewBag.Meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();
            ViewBag.Members = SharedManager.SplitString(proj.DevTeam);
            ViewBag.ScrumMaster = proj.ScrumMaster;

            meeting.ProjectID = proj.ProjectID;

            ViewBag.Error = MeetingManager.RemoveMeeting(meeting);


            var meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();

            Session["Meetings"] = meetings;
            ViewBag.Meetings = meetings;
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);

            return PartialView("MeetingList");
        }

        [Route("project/sprint/{id:int}/answer_questions")]
        [HttpPost]
        public ActionResult Answer_Questions(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Error = MeetingManager.AddMeetingQuestions(id, meeting);
            ViewBag.Meetings = (MeetingManager.GetMeetingsByEmail(user.Email, id)).OrderBy(m => m.Time).ToList();
            return PartialView("ModalView");
        }

        [Route("project/sprint/{id:int}/change_item")]
        [HttpPost]
        public ActionResult Change_Item(int id, List<Item> items)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            foreach (var item in items)
            {
                SprintManager.ChangeStatus(item);
            }
            SprintManager.ReprioritiseItems(id, items);
            Session["Project"] = ProjectManager.GetProjectByID(proj.ProjectID, user.Email);
            return null;
        }

        [Route("project/sprint/{id:int}/add_notes")]
        [HttpPost]
        public ActionResult Add_Notes(int id, Item item)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Home", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            ViewBag.Item = SprintManager.Add_Notes(item.ItemID, item.ItemNotes);
            return PartialView("ItemModalBody");
        }

        [Route("project/sprint/{id:int}/add_meeting_notes")]
        [HttpPost]
        public ActionResult Add_Meeting_Notes(int id, Meeting meeting)
        {
            var user = (User)Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var proj = (Project)Session["Project"];
            ViewBag.Link = "Project";
            if (proj == null)
                return RedirectToAction("Dashboard", "Dashboard");
            var sprint = SprintManager.GetSprintFromID(id);
            if (sprint == null)
                return RedirectToAction("Home", "Project");

            
            ViewBag.Meeting = SprintManager.Add_Meeting_Notes(meeting);
            return PartialView("OtherMeetModalBody");
        }
    }
}