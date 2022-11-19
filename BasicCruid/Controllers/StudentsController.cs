using BasicCruid.Data;
using BasicCruid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicCruid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudents([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentsById(int id)
        {
            var students = await _context.Students.FirstOrDefaultAsync(x => x.Id==id);
            if (students == null) 
            {
                return NotFound();
            }
            return Ok(students);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existstudents = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existstudents == null)
            {
                return NotFound();
            }
            existstudents.StudentName = student.StudentName;
            existstudents.Standart = student.Standart;
            existstudents.Address = student.Address;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var existstudents = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existstudents == null)
            {
                return NotFound();
            }
            _context.Students.Remove(existstudents);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}
