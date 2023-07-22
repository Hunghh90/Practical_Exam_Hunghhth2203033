using System.ComponentModel.DataAnnotations;

namespace Practical_Exam_Hunghhth2203033.Dtos
{
    public class EmployeeData
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string? EmployeeName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [MinimumAge(16)]
        public DateTime? EmployeeDob { get; set; }

        [Required]
        [EmailAddress]
        public string? EmployeeDepartment { get; set; }
    }
}
