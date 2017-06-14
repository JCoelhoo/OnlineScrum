using System.Web.Mvc;

namespace OnlineScrum.Controllers.About
{
    public class AboutController : Controller
    {
        [Route("about")]
        public ActionResult Home()
        {
            ViewBag.Link = "About";
            return View();
        }
    }
}