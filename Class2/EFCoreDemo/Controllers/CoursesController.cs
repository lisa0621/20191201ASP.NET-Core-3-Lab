using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;
using Microsoft.Extensions.FileProviders;

namespace EFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public CoursesController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/Courses/Git
        [HttpGet("Git")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseForGitAsync()
        {
            return await _context.Course.Where(p => p.Title.Contains("Git")).ToListAsync();
        }

        // GET: api/Courses/Pic
        [HttpGet("Pic")]
        public IActionResult GetCourseForPic(string filename = "mang.png")
        {
            string filePath = @"C:\Users\lisa7\Downloads";

            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(filename);
            var readStream = fileInfo.CreateReadStream();

            return File(readStream, "image/jpeg");
        }

        // GET: api/Courses/File
        [HttpGet("File")]
        public ActionResult<CourseFile> GetCourseForFile(string filename)
        {
            return new CourseFile() { FileName = filename };
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        ////[HttpGet("{id:int}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            //var course = await _context.Course
            //    .Include(p => p.Department)
            //    .SingleAsync(p => p.CourseId == id);

            var course = await (
                   from p in _context.Course.Include(p => p.Department)
                   where p.CourseId == id
                   select new
                   {
                       p.CourseId,
                       p.Title,
                       p.Credits,
                       p.DepartmentId,
                       p.Department.Name
                   }).SingleAsync();

            if (course == null)
            {
                return NotFound();
            }

            //return course;

            return new JsonResult(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }

    public class CourseFile
    {
        public string FileName { get; set; }
    }
}
