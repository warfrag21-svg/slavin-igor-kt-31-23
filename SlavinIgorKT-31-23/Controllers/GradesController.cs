using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("GetGrades")]
        public async Task<IActionResult> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken)
        {
            var grades = await _gradeService.GetGradesAsync(filter, cancellationToken);
            return Ok(grades);
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            var result = await _gradeService.AddGradeAsync(grade, cancellationToken);
            return Ok(result);
        }

        [HttpPost("UpdateGrade")]
        public async Task<IActionResult> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            var result = await _gradeService.UpdateGradeAsync(grade, cancellationToken);
            return Ok(result);
        }

        [HttpGet("AverageByDisciplineInGroup")]
        public async Task<IActionResult> GetAverageGradeByDisciplineInGroupAsync(int disciplineId, int groupId, CancellationToken cancellationToken)
        {
            var result = await _gradeService.GetAverageGradeByDisciplineInGroupAsync(disciplineId, groupId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("AverageByYear")]
        public async Task<IActionResult> GetAverageGradeByYearAsync(int year, CancellationToken cancellationToken)
        {
            var result = await _gradeService.GetAverageGradeByYearAsync(year, cancellationToken);
            return Ok(result);
        }

        [HttpGet("StudentGrade")]
        public async Task<IActionResult> GetStudentGradeAsync(
    int studentId, int disciplineId, CancellationToken cancellationToken)
        {
            var result = await _gradeService.GetStudentGradeAsync(studentId, disciplineId, cancellationToken);
            return Ok(result);
        }

        [HttpPost("AddOrUpdateGrade")]
        public async Task<IActionResult> AddOrUpdateGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            var result = await _gradeService.AddOrUpdateGradeAsync(grade, cancellationToken);
            return Ok(result);
        }
    }
}