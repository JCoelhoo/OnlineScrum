﻿using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            if (UserManager.RegisterUser(new Register
                {
                    Email = "1@g.c",
                    Password = "1",
                    Username = "1",
                    Role = "ScrumMaster"
                }) == "")
            {
                UserManager.RegisterUser(new Register
                {
                    Email = "2@g.c",
                    Password = "2",
                    Username = "2",
                    Role = "Developer"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "3@g.c",
                    Password = "3",
                    Username = "3",
                    Role = "Developer"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "4@g.c",
                    Password = "4",
                    Username = "4",
                    Role = "ScrumMaster"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "5@g.c",
                    Password = "5",
                    Username = "5",
                    Role = "Developer"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "6@g.c",
                    Password = "6",
                    Username = "6",
                    Role = "Developer"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "7@g.c",
                    Password = "7",
                    Username = "7",
                    Role = "Developer"
                });
                UserManager.RegisterUser(new Register
                {
                    Email = "8@g.c",
                    Password = "8",
                    Username = "8",
                    Role = "Developer"
                });

                var proj = new Project
                {
                    Name = "OnlineScrum",
                    DevTeamList = new List<string> {"2@g.c", "3@g.c", "5@g.c"},
                    ProjectID = 1,
                    Sprints = "1",
                    ScrumMaster = "1@g.c"
                };
                var sprint1 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-45),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Requirements Analysis",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(-30),
                    Meetings =""
                };
                var sprint2 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-30),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Design",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(-15),
                    Meetings = ""
                };
                var sprint3 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-15),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Initial Implementation",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(0),
                    Meetings = ""
                };
                var sprint3_2 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-15),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Final Implementation",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(0),
                    Meetings = ""
                };
                var sprint4 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(0),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Verification",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(15),
                    Meetings = ""
                };
                var sprint5 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(15),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Maintenance",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(30),
                    Meetings = ""
                };
                var item1 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 1,
                    ItemName = "Item#1",
                    ItemStatus = "Developing"
                };
                var item2 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 2,
                    ItemName = "Item#2",
                    ItemStatus = "Delayed"
                };
                var item3 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 4,
                    ItemID = 3,
                    ItemName = "Item#3",
                    ItemStatus = "Developing"
                };
                var item4 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort =2,
                    ItemID = 4,
                    ItemName = "Item#4",
                    ItemStatus = "Testing"
                };
                var item5 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 5,
                    ItemID = 5,
                    ItemName = "Item#5",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-13),
                };
                var item6 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 4,
                    ItemID = 6,
                    ItemName = "Item#6",
                    ItemStatus = "Testing"
                };
                var item7 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 7,
                    ItemName = "Item#7",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-11),
                };
                var item8 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 2,
                    ItemID = 8,
                    ItemName = "Item#8",
                    ItemStatus = "Developing"
                };
                var item9 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort =3,
                    ItemID = 9,
                    ItemName = "Item#9",
                    ItemStatus = "Delayed"
                };
                var item10 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 3,
                    ItemID = 10,
                    ItemName = "Item#10",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-10),
                };
                var item11 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 11,
                    ItemName = "Item#11",
                    ItemStatus = "Developing"
                };
                var item12 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 3,
                    ItemID = 12,
                    ItemName = "Item#12",
                    ItemStatus = "Testing"
                };
                var item13 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 13,
                    ItemName = "Item#13",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-7),
                };
                var item14 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 14,
                    ItemName = "Item#14",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-6),
                };
                var item15 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 15,
                    ItemName = "Item#15",
                    ItemStatus = "Developing",
                };
                var item16 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 16,
                    ItemName = "Item#16",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-4),
                };
                var item17 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 17,
                    ItemName = "Item#17",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-4),
                };
                var item18 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 18,
                    ItemName = "Item#18",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-2),
                };
                var item19 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 19,
                    ItemName = "Item#19",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-2),
                };
                var item20 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 20,
                    ItemName = "Item#20",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-1),
                };



                ProjectManager.AddProject(proj, "1@g.c");
                ProjectManager.AddSprint(sprint1, proj);
                ProjectManager.AddSprint(sprint2, proj);
                ProjectManager.AddSprint(sprint3, proj);
                ProjectManager.AddSprint(sprint3_2, proj);
                ProjectManager.AddSprint(sprint4, proj);
                ProjectManager.AddSprint(sprint5, proj);
                SprintManager.AddItem(sprint1, item1);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item2);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item3);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item4);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item5);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item6);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item7);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item8);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item9);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item10);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item11);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item12);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item13);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item14);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item15);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item16);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item17);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item18);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item19);
                sprint1 = SprintManager.GetSprintFromID(sprint1.SprintID);
                SprintManager.AddItem(sprint1, item20);
            }

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
                SharedManager.RepeatMethod = false;
                return RedirectToAction("Home", "Dashboard");
            }

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