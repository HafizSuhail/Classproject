using Microsoft.AspNetCore.Mvc;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;

namespace NextLeapAcademy
{
    public class StudentController : Controller
    {
        public IActionResult StudentList()
        {
            //To show the student list you need database class data so follow these steps
            //We have a database class called [Nextleapdbcontex]

            // (1), Create the Object of the database class
            var DBStudentList = new Nextleapdbcontex(); // Created the object of combined class of DB & C# Class

            // (2), Create the list of objects Student class (Varible of list )
            var Student = DBStudentList.Students.ToList(); // Here the 1st Student is List name & 2nd Students is DB Class Table Name  

            return View(Student);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StudentForm(StudenteditorModel Admininputs)
        {

            if(ModelState.IsValid)
            {

                // Here we have to do 3 Things
                // Model Binding// Taking the userinputs into entity class object  (Internally its done by binding the model Class here & its object name is   userdetails)
                // Create an abject of Stdent Entity Class

                Student newStudent = new Student(); // here Student is Entity Class name & newstudent is Its object name

                // Taking the userinputs into entity class object 

                
                newStudent.RollNumber = Admininputs.RollNumber;
                newStudent.StudentName = Admininputs.Name;
                newStudent.Gender = Admininputs.Gender;
                newStudent.Dob = Admininputs.Dob;
                newStudent.MobileNumber = Admininputs.MobileNumber;
                newStudent.Email = Admininputs.Email;

                // Created the new object of dbcontex class
                var UItodbclass = new Nextleapdbcontex();

                // Give this object To DB Context to Add the data in the Database
                UItodbclass.Add(newStudent);

                //Now Save the datachanges in Databas

                UItodbclass.SaveChanges();

                return RedirectToAction("StudentList");

            }
            else
            {
                ModelState.AddModelError("", "Student record not Save, please fix errors and save again!");
                return View("AddStudent", Admininputs);
            }

            
        }
    }
}
