using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Services;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService? _teacherService;
        public TeacherController(TeacherService? teacherService)
        {
            _teacherService = teacherService;
        }
         
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            var getAllTeachers = _teacherService.GetAllTeachers();
            if (!getAllTeachers.Any())
            {
                return NotFound("My List is empty.");
            }
            return Ok(getAllTeachers);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var selectedTeacher = _teacherService.GetTeacher(id);
            if (selectedTeacher == null)
            {
                return NotFound("The " + id + " has not been found");
            }
            return selectedTeacher;
        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> CreateTeacher(Teacher teacher)
        {
            this._teacherService.AddTeacher(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher);

        }

        [HttpDelete]

        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var teacherToBeDeleted = _teacherService.DeleteTeacher(id);
            if (teacherToBeDeleted == 1)
            {
                return NoContent();
            }
            return NotFound("Teacher's " + id + " cannot be found.");
        }

        [HttpPut]

        public async Task<ActionResult<Teacher>> UpdateTeacher(int id, Teacher teacher)
        {
            var teacherToUpdate = _teacherService.UpdateStudent(id, teacher);
            if(teacherToUpdate.Id == id)
            {
                return Ok(teacherToUpdate);
            }
            return NotFound();
        }


    }
}

