using System;
using System.Collections.Generic;

namespace Practical_Exam_Hunghhth2203033.Entities;

public partial class Projectemployee
{
    public int? EmployeeId { get; set; }

    public int? ProjectId { get; set; }

    public string? Tasks { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Project? Project { get; set; }
}
