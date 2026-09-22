using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("GetStudents")]
        public async Task<IActionResult> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            var result = await _studentService.AddStudentAsync(student, cancellationToken);
            return Ok(result);
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            var result = await _studentService.UpdateStudentAsync(student, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(studentId, cancellationToken);
            return Ok();
        }
    }
}