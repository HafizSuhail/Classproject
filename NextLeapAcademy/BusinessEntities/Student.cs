using System;
using System.Collections.Generic;

namespace NextLeapAcademy.BusinessEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? RollNumber { get; set; }

    public string StudentName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public int CourseId { get; set; }

    public int NationId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Nationality? Nation { get; set; }
}
