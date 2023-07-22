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
        public IActionResult Details(int id)
        {
            var employee =  _context.Employees
        .Include(e => e.ProjectEmployees)
        .ThenInclude(pe => pe.Project)
        .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);

        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string employeeName, DateTime? dobFrom, DateTime? dobTo)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(employeeName))
            {
                query = query.Where(e => e.EmployeeName.Contains(employeeName));
            }

            if (dobFrom.HasValue)
            {
                query = query.Where(e => e.EmployeeDob >= dobFrom.Value);
            }

            if (dobTo.HasValue)
            {
                query = query.Where(e => e.EmployeeDob <= dobTo.Value);
            }

            return await query.ToListAsync();
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
