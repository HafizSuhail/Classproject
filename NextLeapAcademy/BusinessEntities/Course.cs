using System;
using System.Collections.Generic;

namespace NextLeapAcademy.BusinessEntities;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; }

    public int Duration { get; set; }

    public decimal Price { get; set; }
}
