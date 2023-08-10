using Microsoft.AspNetCore.Mvc.Rendering;

namespace NextLeapAcademy.Models
{
    public class viewmodel
    {

        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> Nations { get; set; }
    }
}
