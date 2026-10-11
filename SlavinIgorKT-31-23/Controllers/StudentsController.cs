using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;
using SlavinIgorkt_31_23.Services;

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

        [HttpGet("CountByGroup")]
        public async Task<IActionResult> GetStudentsCountByGroup(int groupId, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetStudentsCountByGroup(groupId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("StudentWithNone")]
        public async Task<IActionResult> GetStudentsWithGradeAsync(CancellationToken cancellationToken)
        {
            var result = await _studentService.GetStudentsWithGradeAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("StudentsBySpecialty")]
        public async Task<IActionResult> GetLastNamesBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetLastNamesBySpecialtyAsync(specialtyTitle, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GroupsStudentsCountBySpecialty")]
        public async Task<IActionResult> GetGroupsStudentsCountBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetGroupsStudentsCountBySpecialtyAsync(specialtyTitle, cancellationToken);
            return Ok(result);
        }
    }
}