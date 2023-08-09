using Microsoft.AspNetCore.Mvc;

namespace NextLeapAcademy
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            //string userName = Request.Cookies["username"];
           string userName = HttpContext.Session.GetString("Username");
            return View("Homepage", userName);
        }
    }
}
