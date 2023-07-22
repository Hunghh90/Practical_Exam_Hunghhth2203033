using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Exam_Hunghhth2203033.Dtos;
using Practical_Exam_Hunghhth2203033.Entities;
using System.Linq;

namespace Practical_Exam_Hunghhth2203033.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly PracticalExamContext _context;
        public ProjectsController(PracticalExamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList<Project>();
            return Ok(projects);
        }
        [HttpGet]
        [Route("detail")]
        public IActionResult Details(int id) 
        {
            var pr =  _context.Projects
        .Include(p => p.ProjectEmployees)
        .ThenInclude(pe => pe.Employee)
        .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (pr == null)
            {
                return NotFound();
            }
            return Ok(pr);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult SearchByName( string projectName, bool inProgress)
        {
            IQueryable<Project> query = _context.Projects;

            if (!string.IsNullOrEmpty(projectName))
            {
                var pr = query.Where(p => p.ProjectName.Contains(projectName)).FirstOrDefault();
                return Ok(pr);
            }

            if (inProgress)
            {
                var pr = query.Where(p => p.ProjectEndDate == null || p.ProjectEndDate > DateTime.Now).ToList();
                return Ok(pr);
            }
            else
            {
                var pr = query.Where(p => p.ProjectEndDate != null && p.ProjectEndDate <= DateTime.Now).ToList();
                return Ok(pr);

            }

            

        }

        [HttpGet]
        [Route("SearchProgress")]
        public IActionResult SearchProgress(string projectName)
        {
            var pr = _context.Projects.Where(p => p.ProjectName.Contains(projectName)).FirstOrDefault();
            return Ok(new ProjectData
            {
                ProjectName = pr.ProjectName,
                ProjectStartDate = pr.ProjectStartDate,
                ProjectEndDate = pr.ProjectEndDate,
            });
        }


        [HttpPost]
        public IActionResult Create(ProjectData projectData)
        {
            var pr = _context.Projects.Where(p => p.ProjectName.Contains(projectData.ProjectName)).FirstOrDefault();
            if (pr != null )
                return BadRequest("Project is exists");
            var newpr = new Entities.Project
            {
                ProjectName = projectData.ProjectName,
                ProjectStartDate = projectData.ProjectStartDate,
                ProjectEndDate = projectData.ProjectEndDate,
            };
            _context.Projects.Add(newpr);
            _context.SaveChanges();
            return Ok(projectData);
        }

        [HttpPut]
        public IActionResult Update(int id, ProjectData projectData)
        {
            var pr = _context.Projects.Find(id);
            if (pr == null) return NotFound("Not Found Project");
            pr.ProjectStartDate = projectData.ProjectStartDate;
            pr.ProjectEndDate = projectData.ProjectEndDate;
            pr.ProjectName = pr.ProjectName;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delette(int id)
        {
            var pr = _context.Projects.Find(id);
            if (pr == null) return NotFound("Not Found Project");
            _context.Projects.Remove(pr);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
