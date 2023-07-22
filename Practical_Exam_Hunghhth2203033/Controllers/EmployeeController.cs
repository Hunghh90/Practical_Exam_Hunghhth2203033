using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Exam_Hunghhth2203033.Dtos;
using Practical_Exam_Hunghhth2203033.Entities;

namespace Practical_Exam_Hunghhth2203033.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly PracticalExamContext _context;
        public EmployeeController(PracticalExamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employee = _context.Employees.ToList<Employee>();
            return Ok(employee);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult Details(string employeeName)
        {
            var employee = _context.Employees.Where(p => p.EmployeeName.Contains(employeeName)).FirstOrDefault();
            var detail = _context.Projectemployees
                .Where(p => p.ProjectId == employee.EmployeeId)
                .Include(p => p.Project)
                .Include(p => p.Employee)
                .FirstOrDefault();
            return Ok( detail);

        }

        [HttpPost]
        public IActionResult Create(EmployeeData employeeData)
        {
            var pr = _context.Employees.Find(employeeData.EmployeeName);
            if (pr != null)
                return BadRequest("Project is exists");
            var newemployee = new Entities.Employee
            {
                EmployeeName = employeeData.EmployeeName,
                EmployeeDob = employeeData.EmployeeDob,
                EmployeeDepartment = employeeData.EmployeeDepartment,
            };
            _context.Employees.Add(newemployee);
            _context.SaveChanges();
            return Ok(employeeData);
        }

        [HttpPut]
        public IActionResult Update(int id, EmployeeData employeeData)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound("Not Found Employee");
            employee.EmployeeName = employeeData.EmployeeName;
            employee.EmployeeDob = employeeData.EmployeeDob;
            employee.EmployeeDepartment = employeeData.EmployeeDepartment;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delette(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound("Not Found Employee");
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
