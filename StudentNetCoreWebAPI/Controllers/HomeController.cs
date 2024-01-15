using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentNetCoreWebAPI.Data;
using StudentNetCoreWebAPI.Entities;
using StudentNetCoreWebAPI.Models;

namespace StudentNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private StudentDBContext _context;
        public HomeController(StudentDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                    return Ok(student);
                else return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentModel model)
        {
            var student = new StudentEntity()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id:guid}, {nickName:alpha}")]
        public async Task<IActionResult> UpdateStudent(Guid id, string nickName, StudentModel model)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    student.Name = model.Name + " " + nickName;
                    student.Email = model.Email;
                    await _context.SaveChangesAsync();
                    return Ok(student);
                }
                else
                    return NotFound();
            }
            catch
            { 
                return BadRequest(); 
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if(student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    return Ok(student);
                }
                else
                    return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
