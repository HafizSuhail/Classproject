using Microsoft.AspNetCore.Mvc;

namespace NextLeapAcademy
{
    public class StudentController : Controller
    {
        public IActionResult StudentList()
        {
            return View();
        }
    }
}
