using Microsoft.AspNetCore.Mvc;

namespace NextLeapAcademy
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View("Homepage");
        }
    }
}
