using Microsoft.AspNetCore.Mvc;
using NextLeapAcademy.BusinessEntities;

namespace NextLeapAcademy
{
    public class CourseController : Controller
    {
        public IActionResult CoursesList()
        {
            var DBCourselist = new Nextleapdbcontex();
            var Courses = DBCourselist.Courses.ToList();
            return View(Courses);
        }
    }
}
