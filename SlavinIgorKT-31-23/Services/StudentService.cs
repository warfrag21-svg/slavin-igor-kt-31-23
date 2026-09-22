using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Students.AsQueryable();

            if (filter.GroupId.HasValue)
                query = query.Where(s => s.GroupId == filter.GroupId.Value);
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(s => s.FirstName.Contains(filter.FirstName));
            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(s => s.LastName.Contains(filter.LastName));
            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Students.FindAsync(new object[] { studentId }, cancellationToken);
            if (student != null)
            {
                student.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}