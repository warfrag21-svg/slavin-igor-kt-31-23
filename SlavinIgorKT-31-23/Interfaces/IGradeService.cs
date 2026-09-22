using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Interfaces
{
    public interface IGradeService
    {
        Task<Grade[]> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken = default);
        Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default);
        Task<Grade> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default);
        Task<double> GetAverageGradeByDisciplineInGroupAsync(int disciplineId, int groupId, CancellationToken cancellationToken = default);
        Task<double> GetAverageGradeByYearAsync(int year, CancellationToken cancellationToken = default);
        Task<int?> GetStudentGradeAsync(int studentId, int disciplineId, CancellationToken cancellationToken = default);
        Task<Grade> AddOrUpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default);
        
    }
}