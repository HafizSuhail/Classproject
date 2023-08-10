using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NextLeapAcademy
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            //string userName = Request.Cookies["username"];
            //string userName = HttpContext.Session.GetString("Username");
            string userName = HttpContext.User.Identity.Name;
            return View("Homepage", userName);
        }
    }
}
