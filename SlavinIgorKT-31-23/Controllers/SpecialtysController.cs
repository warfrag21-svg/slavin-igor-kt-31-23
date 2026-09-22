using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecialtysController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtysController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpPost("GetSpecialtys")]
        public async Task<IActionResult> GetSpecialtysAsync(SpecialtyFilter filter, CancellationToken cancellationToken)
        {
            var specialtys = await _specialtyService.GetSpecialtysAsync(filter, cancellationToken);
            return Ok(specialtys);
        }

        [HttpPost("AddSpecialty")]
        public async Task<IActionResult> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken)
        {
            var result = await _specialtyService.AddSpecialtyAsync(specialty, cancellationToken);
            return Ok(result);
        }

        [HttpPost("UpdateSpecialty")]
        public async Task<IActionResult> UpdateSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken)
        {
            var result = await _specialtyService.UpdateSpecialtyAsync(specialty, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("DeleteSpecialty/{specialtyId}")]
        public async Task<IActionResult> DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken)
        {
            await _specialtyService.DeleteSpecialtyAsync(specialtyId, cancellationToken);
            return Ok();
        }
    }
}