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
                    Name = "OnlineScrum",
                    DevTeamList = new List<string> {"2@g.c", "3@g.c", "5@g.c"},
                    ProjectID = 1,
                    Sprints = "",
                    ScrumMaster = "1@g.c"
                };
                var sprint1 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-60),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Requirements Analysis",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(-45),
                    Meetings =""
                };
                var sprint2 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-45),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 2,
                    SprintName = "Design",
                    SprintNumber = 2,
                    FinishDate = DateTime.Now.AddDays(-30),
                    Meetings = ""
                };
                var sprint3 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-30),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 3,
                    SprintName = "Initial Implementation",
                    SprintNumber = 3,
                    FinishDate = DateTime.Now.AddDays(-15),
                    Meetings = ""
                };
                var sprint3_2 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(-15),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 4,
                    SprintName = "Final Implementation",
                    SprintNumber = 4,
                    FinishDate = DateTime.Now.AddDays(15),
                    Meetings = ""
                };
                var sprint4 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(15),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 5,
                    SprintName = "Verification",
                    SprintNumber = 5,
                    FinishDate = DateTime.Now.AddDays(30),
                    Meetings = ""
                };
                var sprint5 = new Sprint
                {
                    StartDate = DateTime.Now.AddDays(30),
                    MeetingInterval = 1,
                    MeetingLocation = "X",
                    SprintID = 1,
                    SprintName = "Maintenance",
                    SprintNumber = 1,
                    FinishDate = DateTime.Now.AddDays(45),
                    Meetings = ""
                };
                var item1 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 1,
                    ItemName = "Customer Requirements",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-55),
                };
                var item2 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 2,
                    ItemName = "Architectural Requirements",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-53)
                };
                var item3 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 4,
                    ItemID = 3,
                    ItemName = "Functional Requirements",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-50)
                };
                var item4 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort =2,
                    ItemID = 4,
                    ItemName = "Core Funcionality Requirements",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-47)
                };
                var item5 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 5,
                    ItemID = 5,
                    ItemName = "Non Functional Requirements",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-46)
                };
                var item6 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 4,
                    ItemID = 6,
                    ItemName = "DB Design",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-44)
                };
                var item7 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 7,
                    ItemName = "Architecture Design",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-40),
                };
                var item8 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 2,
                    ItemID = 8,
                    ItemName = "Security Design",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-38)
                };
                var item9 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort =3,
                    ItemID = 9,
                    ItemName = "Interface Mockup",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-37)
                };
                var item10 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 3,
                    ItemID = 10,
                    ItemName = "Usability Design",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-33),
                };
                var item11 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 11,
                    ItemName = "Use Cases",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-31)
                };
                var item12 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 3,
                    ItemID = 12,
                    ItemName = "Item",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-28)
                };
                var item13 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 13,
                    ItemName = "Login",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-27),
                };
                var item14 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 2,
                    ItemID = 14,
                    ItemName = "Project",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-23),
                };
                var item15 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 15,
                    ItemName = "Database",
                    ItemStatus = "Delayed"
                };
                var item16 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 3,
                    ItemID = 16,
                    ItemName = "Interaction with Database",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-17),
                };
                var item17 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 17,
                    ItemName = "User",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-16),
                };
                var item18 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 2,
                    ItemID = 18,
                    ItemName = "Meeting",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-14),
                };
                var item19 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 4,
                    ItemID = 19,
                    ItemName = "Statistics",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-12),
                };
                var item20 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 1,
                    ItemID = 20,
                    ItemName = "Interface",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-10),
                };
                var item21 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 2,
                    ItemID = 21,
                    ItemName = "Security",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-7),
                };
                var item22 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 3,
                    ItemID = 22,
                    ItemName = "Burndown Chart",
                    ItemStatus = "Closed",
                    DateClosed = DateTime.Now.AddDays(-3),
                };
                var item23 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 4,
                    ItemID = 23,
                    ItemName = "Velocity Chart",
                    ItemStatus = "Testing",
                };
                var item24 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 3,
                    ItemID = 24,
                    ItemName = "Multiple Projects",
                    ItemStatus = "Developing",
                };
                var item25 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 1,
                    ItemID = 25,
                    ItemName = "Settings",
                    ItemStatus = "Developing",
                };
                var item26 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 2,
                    ItemID = 26,
                    ItemName = "Refactor",
                    ItemStatus = "Developing",
                };
                var item27 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 2,
                    ItemID = 27,
                    ItemName = "Usability tests",
                    ItemStatus = "Testing",
                };
                var item28 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 3,
                    ItemID = 28,
                    ItemName = "Sprint",
                    ItemStatus = "Testing",
                };
                var item29 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 2,
                    ItemID = 29,
                    ItemName = "Product Backlog",
                    ItemStatus = "Developing",
                };
                var item30 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 4,
                    ItemID = 30,
                    ItemName = "Meeting Table",
                    ItemStatus = "Testing",
                };

                var s1 = new Item
                {
                    AssignedTo = "3@g.c",
                    EstimatedEffort = 5,
                    ItemID = 28,
                    ItemName = "Migrate to Cloud",
                    ItemStatus = "Developing",
                };

                var s2 = new Item
                {
                    AssignedTo = "2@g.c",
                    EstimatedEffort = 4,
                    ItemID = 29,
                    ItemName = "Language Support",
                    ItemStatus = "Development",
                };

                var s3 = new Item
                {
                    AssignedTo = "5@g.c",
                    EstimatedEffort = 3,
                    ItemID = 30,
                    ItemName = "Contact Support",
                    ItemStatus = "Development",
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

                SprintManager.AddItem(sprint2, item6);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item7);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item8);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item9);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item10);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item11);
                sprint2 = SprintManager.GetSprintFromID(sprint2.SprintID);
                SprintManager.AddItem(sprint2, item12);

                sprint3 = SprintManager.GetSprintFromID(sprint3.SprintID);
                SprintManager.AddItem(sprint3, item13);
                sprint3 = SprintManager.GetSprintFromID(sprint3.SprintID);
                SprintManager.AddItem(sprint3, item14);
                sprint3 = SprintManager.GetSprintFromID(sprint3.SprintID);
                SprintManager.AddItem(sprint3, item15);
                sprint3 = SprintManager.GetSprintFromID(sprint3.SprintID);
                SprintManager.AddItem(sprint3, item16);
                sprint3 = SprintManager.GetSprintFromID(sprint3.SprintID);
                SprintManager.AddItem(sprint3, item17);

                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item18);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item19);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item20);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item21);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item22);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item23);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item24);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item25);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item26);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item27);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item28);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item29);
                sprint3_2 = SprintManager.GetSprintFromID(sprint3_2.SprintID);
                SprintManager.AddItem(sprint3_2, item30);

                var i = new SprintItem();
                i.Item = "15";
                i.Sprint = sprint3_2.SprintName;

            ProjectManager.ChangeSprintInItem(new List<SprintItem>
                {
                    i
                });

                SprintManager.AddItem(null, item27);
                SprintManager.AddItem(null, item27);
                SprintManager.AddItem(null, item27);
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