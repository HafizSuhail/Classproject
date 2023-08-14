using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NextLeapAcademy.Models
{
    public class StudenteditorModel
    {
        [HiddenInput]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please Enter RollNumber")]
        [StringLength(10)]
        [Display(Name = "RollNumber")]
        public string? RollNumber { get; set; }

        [Required(ErrorMessage = "Please Enter StudentName")]
        [StringLength(25)]
        [Display(Name = "StudentName")]
        public string? Name { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Enter the Date of Birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Enter the Mobile Number")]
        [Display(Name = "MobileNumber")]
        [Phone]
        public string? MobileNumber { get; set; }

        [Required(ErrorMessage ="Enter the Mail_ID")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please select a Course")]
        [Display(Name = "Course")]
        public int Courseid { get; set; }

        [Required(ErrorMessage = "Please select a Nationality")]
        [Display(Name = "Nationality")]
        public int Nationid { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Courses { get; set; }
        [IgnoreDataMember]
        public List<SelectListItem> Nations { get; set; }


    }
}
