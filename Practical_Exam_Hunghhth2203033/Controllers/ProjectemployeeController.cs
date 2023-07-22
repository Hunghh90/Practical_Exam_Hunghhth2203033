using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Exam_Hunghhth2203033.Entities;

namespace Practical_Exam_Hunghhth2203033.Controllers
{
    [ApiController]
    [Route("projectemployee")]
    public class ProjectemployeeController : ControllerBase
    {
        private readonly PracticalExamContext _context;
        public ProjectemployeeController(PracticalExamContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(string projectName)
        {
            var pr = _context.Projectemployees
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefault();
            return Ok(pr);
        }
    }
}
