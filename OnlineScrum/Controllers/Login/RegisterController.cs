using OnlineScrum.BusinessLayer;
using OnlineScrum.Models;
using System.Web.Mvc;

namespace OnlineScrum.Controllers
{
    public class RegisterController : Controller
    {
        [Route("register")]
        [HttpGet]
        public ActionResult Register(string errorMessage)
        {
            ViewBag.Error = errorMessage;
            return View();
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (register.Role != "Developer" || register.Role != "ScrumMaster")
            {
                ViewBag.Error = "Role not valid";
                return RedirectToAction("register");
            }

            var status = UserManager.RegisterUser(register);

            if ( status != "" )
            {
                ViewBag.Error = status;
                return RedirectToAction("register", new { errorMessage = status });
            }
            //return RedirectToAction("Dashboard/Lecturer");
            return RedirectToAction("login", "login");
        }
    }
}