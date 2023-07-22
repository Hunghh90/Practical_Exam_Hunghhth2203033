using System;
using System.Collections.Generic;

namespace Practical_Exam_Hunghhth2203033.Entities;

public partial class Project
{
    public int ProjectId { get; set; }

    public string? ProjectName { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }
    public virtual ICollection<Projectemployee>? ProjectEmployees { get; set; }
}
