using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplineService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Disciplines.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(d => d.Name.Contains(filter.Name));
            if (filter.IsDeleted.HasValue)
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Discipline> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            _dbContext.Disciplines.Update(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var discipline = await _dbContext.Disciplines.FindAsync(new object[] { disciplineId }, cancellationToken);
            if (discipline != null)
            {
                discipline.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}