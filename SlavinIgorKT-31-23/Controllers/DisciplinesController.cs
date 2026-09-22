using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpPost("GetDisciplines")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

        [HttpPost("AddDiscipline")]
        public async Task<IActionResult> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            var result = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);
            return Ok(result);
        }

        [HttpPost("UpdateDiscipline")]
        public async Task<IActionResult> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            var result = await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("DeleteDiscipline/{disciplineId}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
            return Ok();
        }
    }
}