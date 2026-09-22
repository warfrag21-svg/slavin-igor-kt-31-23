using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Interfaces
{
    public interface IDisciplineService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);
        Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
        Task<Discipline> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
        Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default);
    }
}