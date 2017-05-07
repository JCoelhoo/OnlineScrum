using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Controllers.About
{
    public class AboutController : Controller
    {
        [Route("about")]
        public ActionResult Home()
        {
            return View();
        }
    }
}