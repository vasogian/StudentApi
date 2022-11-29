using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {

        private readonly StudentService? _studentService;

        public StudentController(StudentService studentService)
        {

            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var allStudents = _studentService.GetAllStudents();
            if (!allStudents.Any())
            {
                return NotFound("My list is empty");
            }
            return Ok(allStudents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var selectStudent = _studentService.GetStudentFromDb(id);
            if (selectStudent == null)
            {
                return NotFound();
            }

            return selectStudent;

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateStudent(long id, Student student)
        {
            var studentToBeupdated = _studentService.UpdateStudentFromDb(id, student);
            if (studentToBeupdated == null)
            {
                return NotFound("The student with " + id + " has not been found.");
            }
            return Ok(studentToBeupdated);


        }
        /// <summary>
        /// Creates a student.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// Post/Create 
        /// {
        /// "id": 0,
        ///  "name": "string",
        ///  "grade": 0,
        ///  //  "studentSubjects": [
        ///    {
        ///      "id": 0,
        ///      "name": "string"
        ///    }
        ///  ]
        ///}
        ///</remarks>
        /// <response code="201">Returns the newly created student</response>
        /// <response code="400">If the student is null</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {

            this._studentService.AddStudent(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);

        }
        /// <summary>
        /// Deletes a student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(long id)
        {
            var studentDeleted = _studentService.DeleteStudentFromDb(id);
            if (studentDeleted == 1)
            {
                return NoContent();
            }

            return NotFound("Student with" + id + " was not found");
        }

    }
}
