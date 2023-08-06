using Microsoft.AspNetCore.Mvc;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;
using System.Linq.Expressions;

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
        [HttpGet]
        public IActionResult Seditorpage(int Studentid)
        {
            if (ModelState.IsValid) 
            {
                // Object of DB CLASS
                var dbobject = new Nextleapdbcontex();

                // Create a varaible & Fetch the studentID from DB_Class
                var fetchstuid = dbobject.Students.Where(P => P.StudentId == Studentid).FirstOrDefault();

                // Object of Model Class
                var editStudent = new StudenteditorModel();

                editStudent.RollNumber = fetchstuid.RollNumber;
                editStudent.Name = fetchstuid.StudentName;
                editStudent.Gender = fetchstuid.Gender;
                editStudent.Dob = fetchstuid.Dob;
                editStudent.MobileNumber = fetchstuid.MobileNumber;
                editStudent.Email = fetchstuid.Email;
                editStudent.StudentId = fetchstuid.StudentId;

                return View(editStudent);
            }else 
            {
                ModelState.AddModelError("", "Student record not Save, please fix errors and save again!");
                return RedirectToAction("StudentList", Studentid);
                    
            } 
        }
        [HttpPost]
        public IActionResult Update (StudenteditorModel updateinputs) 
        {
            
           
                // Create an object of DB_Class
                var uptodb = new Nextleapdbcontex();
                //fetching the student obj from database
                var fetinputid = uptodb.Students.Where(P => P.StudentId == updateinputs.StudentId).FirstOrDefault();

                
                fetinputid.RollNumber = updateinputs.RollNumber;
                fetinputid.StudentName = updateinputs.Name;
                fetinputid.Gender = updateinputs.Gender;
                fetinputid.Dob = updateinputs.Dob;
                fetinputid.MobileNumber = updateinputs.MobileNumber;
                fetinputid.Email = updateinputs.Email;
                fetinputid.StudentId = updateinputs.StudentId;

                uptodb.Students.Update(fetinputid);
                uptodb.SaveChanges();
                return RedirectToAction("StudentList");
            
        }

        [HttpPost]
        public JsonResult deleteStudent (int studId)
        {
            try
            {
                var dbcontext = new Nextleapdbcontex();
                //get Student ob
                var StudentObj = dbcontext.Students.Where(p => p.StudentId == studId).FirstOrDefault();

                dbcontext.Students.Remove(StudentObj);
                dbcontext.SaveChanges();

                return Json(true);
            }
            catch
            {
                return Json(false);
            }

            
        }




    }
}
