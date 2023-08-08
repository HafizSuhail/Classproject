using System;
using System.Collections.Generic;

namespace NextLeapAcademy.BusinessEntities;

public partial class Nationality
{
    public int NationId { get; set; }

    public string NationName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
