﻿using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class DashboardController : Controller
    {
        [Route("dashboard")]
        [Route("home")]
        [HttpGet]
        public ActionResult Home()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            ViewBag.Link = "Home";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj == null)
                return View("New_Project");

            ViewBag.ProjectName = proj.Name;
            return View();
        }

        [Route("new_project")]
        [HttpGet]
        public ActionResult Create_Project()
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            ViewBag.Link = "Home";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj != null)
                return RedirectToAction("Home");

            return View();
        }

        [Route("new_project")]
        [HttpPost]
        public ActionResult Create_Project(Project project)
        {
            var user = (User) Session["UserInfo"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            ViewBag.Link = "Home";
            var proj = ProjectManager.GetProjectByEmail(user.Email);
            if (proj != null)
                return RedirectToAction("Home");

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = ProjectManager.AddProject(project, user.Email);
            ViewBag.Error = result;
            if (!String.IsNullOrEmpty(result)) return View("Create_Project");


            Session["Project"] = project;

            return RedirectToAction("Home", "Dashboard");
        }
    }
}