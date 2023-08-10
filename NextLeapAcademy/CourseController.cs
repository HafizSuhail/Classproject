using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;

namespace NextLeapAcademy
{
    [Authorize]
    public class CourseController : Controller
    {
        public IActionResult CoursesList()
        {
            var DBCourselist = new Nextleapdbcontex();
            var Courses = DBCourselist.Courses.ToList();
            return View(Courses);
        }

        public IActionResult AddCourses()
        {
            return View();  
        
        }

        public IActionResult CourseForm(CourseEditorModel Admininputs)
        {

            if(ModelState.IsValid)
            {
                Course newcourse = new Course();

                newcourse.Title = Admininputs.CourseTitle;
                newcourse.Duration = Admininputs.Duration;
                newcourse.Price = Admininputs.Price;

                var DbCourseclass = new Nextleapdbcontex();

                DbCourseclass.Add(newcourse);

                DbCourseclass.SaveChanges();

                return RedirectToAction("CoursesList");
            }
            else 
            {
                ModelState.AddModelError("", "Course record not Save, please fix errors and save again!");
                return View("AddCourses", Admininputs); 
            }
           
        }
    }
}
