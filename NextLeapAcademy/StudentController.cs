﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;
using System.Linq.Expressions;

namespace NextLeapAcademy
{
    [Authorize]
    public class StudentController : Controller
    {
        public IActionResult StudentList()
        {
            //To show the student list you need database class data so follow these steps
            //We have a database class called [Nextleapdbcontex]

            // (1), Create the Object of the database class
            var DBStudentList = new Nextleapdbcontex(); // Created the object of combined class of DB & C# Class

            // (2), Create the list of objects Student class (Varible of list )
            var Student = DBStudentList.Students             // Here the 1sStudent is List name & 2nd Students is DB Class Table Name  
                                        .Include(p => p.Course)
                                        .Include(p => p.Nation)
                                        .ToList();
            //var Student = DBStudentList.Students.ToList();

            return View(Student);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            var coursesnNations = new StudenteditorModel();

            using (var dbcontex = new Nextleapdbcontex()) 
            {
                var courses = dbcontex.Courses.ToList(); // we are getting list of course objects from DB

                coursesnNations.Courses = new List<SelectListItem>();
                foreach (var course in courses) // Course list loop
                {
                    var courseTitle = $"{course.Title}|{course.Duration}Days|{course.Price}/INR";

                    var courseItem = new SelectListItem
                    {
                        Value = course.CourseId.ToString(),
                        Text = courseTitle
                    };

                    coursesnNations.Courses.Add(courseItem);
                }

                var nations = dbcontex.Nationalities.ToList();

                coursesnNations.Nations = new List<SelectListItem>();

                foreach (var nation in nations) // Nations list loop
                {
                    var nationame = $"{nation.NationName}";
                    var nationitem = new SelectListItem { Value = nation.NationId.ToString(), Text = nationame };

                    coursesnNations.Nations.Add(nationitem);
                }
            }

            //coursesnNations.Courses = courses.Select(Course => new SelectListItem
            //{
            //    Value = course.CourseId.ToString(),
            //    Text = courseTitle

            //}).ToList();
            //var courselist= new StudenteditorModel();

            //var dbcontex = new Nextleapdbcontex();

            // we are looping through courses and will prepare an object of selectListItem and will
            // add to model.Courses
            //foreach (var course in courses) // Course list loop
            //{
            //    var courseTitle = $"{course.Title}|{course.Duration}Days|{course.Price}/INR";

            //    var courseItem = new SelectListItem {Value = course.CourseId.ToString(),
            //    Text = courseTitle
            //    };

            //    courselist.Courses.Add(courseItem);

            //}
            //var nationlist = new StudenteditorModel();
            //nationlist.Nations = new List<SelectListItem>();
            //var dbcontex2 = new Nextleapdbcontex();
            //var Nations = dbcontex2.Nationalities.ToList();

            //foreach (var nation in Nations) // Nations list loop
            //{
            //    var nationame = $"{nation.NationName}";
            //    var nationitem = new SelectListItem { Value = nation.NationId.ToString(), Text = nationame };

            //    nationlist.Nations.Add(nationitem);
            //}

            
             
            return View(coursesnNations);
        }

        [HttpPost]
        public IActionResult StudentForm(StudenteditorModel Admininputs)
        {
            //ModelState.Remove("Courses");
            //ModelState.Remove("Nationalities");

            if (ModelState.IsValid)
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
                newStudent.CourseId = Admininputs.Courseid;
                newStudent.NationId = Admininputs.Nationid;
                
                

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
                editStudent.Courseid = fetchstuid.CourseId;
                editStudent.Nationid = fetchstuid.NationId;

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
                fetinputid.CourseId = updateinputs.Courseid;
                fetinputid.NationId = updateinputs.Nationid;

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
