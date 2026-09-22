using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly StudentDbContext _dbContext;

        public SpecialtyService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Specialty[]> GetSpecialtysAsync(SpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Specialtys.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));

            if (!string.IsNullOrEmpty(filter.Code))
                query = query.Where(s => s.Code.Contains(filter.Code));

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
        {
            _dbContext.Specialtys.Add(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return specialty;
        }

        public async Task<Specialty> UpdateSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
        {
            _dbContext.Specialtys.Update(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return specialty;
        }

        public async Task DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default)
        {
            var specialty = await _dbContext.Specialtys.FindAsync(new object[] { specialtyId }, cancellationToken);
            if (specialty != null)
            {
                // Примечание: В конфигурации Group стоит Cascade delete. 
                // При удалении специальности удалятся все связанные группы и студенты.
                _dbContext.Specialtys.Remove(specialty);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}