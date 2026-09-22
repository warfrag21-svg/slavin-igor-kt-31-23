using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Interfaces
{
    public interface ISpecialtyService
    {
        Task<Specialty[]> GetSpecialtysAsync(SpecialtyFilter filter, CancellationToken cancellationToken = default);
        Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default);
        Task<Specialty> UpdateSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default);
        Task DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default);
    }
}