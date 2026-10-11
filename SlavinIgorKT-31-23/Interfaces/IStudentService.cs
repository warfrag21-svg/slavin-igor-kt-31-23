using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Interfaces
{
    public interface IStudentService
    {
        Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default);
        Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task<Student> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
        Task<int> GetStudentsCountByGroup(int groupId, CancellationToken cancellationToken = default);
        Task<string[]> GetStudentsWithGradeAsync(CancellationToken cancellationToken = default);
        Task<string[]> GetLastNamesBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken = default);
        Task<string[]> GetGroupsStudentsCountBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken = default);
    }
}