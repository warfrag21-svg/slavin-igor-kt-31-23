using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Services
{
    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;

        public GradeService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Grade[]> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Grades.AsQueryable();

            if (filter.StudentId.HasValue)
                query = query.Where(g => g.StudentId == filter.StudentId.Value);
            if (filter.DisciplineId.HasValue)
                query = query.Where(g => g.DisciplineId == filter.DisciplineId.Value);
            if (filter.GroupId.HasValue)
                query = query.Where(g => g.Student.GroupId == filter.GroupId.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            _dbContext.Grades.Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        public async Task<Grade> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            _dbContext.Grades.Update(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        public async Task<double> GetAverageGradeByDisciplineInGroupAsync(int disciplineId, int groupId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.DisciplineId == disciplineId && g.Student.GroupId == groupId)
                .AverageAsync(g => g.Value, cancellationToken);
        }

        public async Task<double> GetAverageGradeByYearAsync(int year, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.Student.Group.Course == year)
                .AverageAsync(g => g.Value, cancellationToken);
        }

        public async Task<int?> GetStudentGradeAsync(int studentId, int disciplineId, CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.DisciplineId == disciplineId, cancellationToken);
            return grade?.Value;
        }
        public async Task<Grade> AddOrUpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == grade.StudentId
                                       && g.DisciplineId == grade.DisciplineId,
                                     cancellationToken);

            if (existing != null)
            {
                existing.Value = grade.Value;
                _dbContext.Grades.Update(existing);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return existing;
            }

            _dbContext.Grades.Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }
    }
}