using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NextLeapAcademy.Models
{
    public class CourseEditorModel
    {
        [HiddenInput]
        public int Course_id { get; set; }

        [Required(ErrorMessage = "Enter Course Title")]
        [StringLength(50)]
        [Display(Name = "CourseTitle")]
        public string CourseTitle { get; set; }

       
        public int Duration { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }




    }
}
