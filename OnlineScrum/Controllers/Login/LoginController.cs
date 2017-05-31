using OnlineScrum.BusinessLayer;
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
                    Name = "Mock",
                    DevTeamList = new List<string> {"2@g.c", "3@g.c"},
                    ProjectID = 1,
                    Sprints = "1",
                    ScrumMaster = "1@g.c"
                };
                var sprint = new Sprint
                {
                    StartDate = new DateTime(2017, 05, 01),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Example Sprint",
                    SprintNumber = 1,
                    FinishDate = new DateTime(2017, 05, 31),
                    Meetings=""
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
                    AssignedTo = "2@g.c",
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
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 5,
                    ItemName = "Item#3",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017,05,05)
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
                    DateClosed = new DateTime(2017, 05, 8)
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
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
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
                    DateClosed = new DateTime(2017, 05, 7)
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
                    ItemName = "Item#11",
                    ItemStatus = "Testing"
                };
                var item13 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 13,
                    ItemName = "Item#13",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 18)
                };
                var item14 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 14,
                    ItemName = "Item#14",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 15)
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
                    DateClosed = new DateTime(2017, 05, 11)
                };
                var item17 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 17,
                    ItemName = "Item#17",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 28)
                };
                var item18 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 18,
                    ItemName = "Item#18",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 27)
                };
                var item19 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 19,
                    ItemName = "Item#19",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 22)
                };
                var item20 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 20,
                    ItemName = "Item#20",
                    ItemStatus = "Closed",
                    DateClosed = new DateTime(2017, 05, 22)
                };



                ProjectManager.AddProject(proj, "2@g.c");
                ProjectManager.AddSprint(sprint, proj);
                SprintManager.AddItem(sprint, item1);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item2);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item3);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item4);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item5);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item6);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item7);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item8);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item9);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item10);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item11);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item12);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item13);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item14);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item15);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item16);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item17);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item18);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item19);
                sprint = SprintManager.GetSprintFromID(sprint.SprintID);
                SprintManager.AddItem(sprint, item20);
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