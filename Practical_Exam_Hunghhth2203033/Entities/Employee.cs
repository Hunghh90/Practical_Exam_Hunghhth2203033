using System;
using System.Collections.Generic;

namespace Practical_Exam_Hunghhth2203033.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public DateTime? EmployeeDob { get; set; }

    public string? EmployeeDepartment { get; set; }

    public virtual ICollection<Projectemployee>? ProjectEmployees { get; set; }
}
