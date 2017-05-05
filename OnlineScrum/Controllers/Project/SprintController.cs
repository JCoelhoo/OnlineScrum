using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var sprint = SprintManager.GetSprintFromID(id);

            if(sprint == null)
            {
                ViewBag.Error = "Sprint not found";
                return RedirectToAction("Home", "Project");
            }

            ViewBag.Sprint = sprint;
            return View();
        }
    }
}