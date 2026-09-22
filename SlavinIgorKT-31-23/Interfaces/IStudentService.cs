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
    }
}