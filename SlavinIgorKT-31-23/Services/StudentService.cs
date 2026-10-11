using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;
using System.Text.RegularExpressions;

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

        public async Task<int> GetStudentsCountByGroup(int groupId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Students
                .CountAsync(g => g.GroupId == groupId);
        }

        public async Task<string[]> GetStudentsWithGradeAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Students
                .Where(s => _dbContext.Grades.Any(g => g.StudentId == s.StudentId && g.Value == 2))
                .Select(s => s.LastName)
                .Distinct()
                .ToArrayAsync(cancellationToken);
        }

        public async Task<string[]> GetLastNamesBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken = default) {
            if (string.IsNullOrWhiteSpace(specialtyTitle))
                throw new ArgumentException("Название специальности не может быть пустым", nameof(specialtyTitle));

            return await _dbContext.Students
                .Where(s => s.Group.Specialty.Title == specialtyTitle)
                .Select(s => s.LastName)
                .OrderBy(l => l)
                .Distinct()
                .ToArrayAsync(cancellationToken);
        }

        public async Task<string[]> GetGroupsStudentsCountBySpecialtyAsync(string specialtyTitle, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(specialtyTitle))
                throw new ArgumentException("Название специальности не может быть пустым", nameof(specialtyTitle));

            return await _dbContext.Students
                .Where(s => !s.IsDeleted && s.Group.Specialty.Title == specialtyTitle)
                .GroupBy(s => s.Group.Name)
                .Select(g => g.Key + ": " + g.Count().ToString())
                .OrderBy(s => s)
                .ToArrayAsync(cancellationToken);
        }

    }
}