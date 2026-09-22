using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Services
{
    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Groups.AsQueryable();

            if (filter.SpecialtyId.HasValue)
                query = query.Where(g => g.SpecialtyId == filter.SpecialtyId.Value);
            if (filter.Course.HasValue)
                query = query.Where(g => g.Course == filter.Course.Value);
            if (filter.IsDeleted.HasValue)
                query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Groups.FindAsync(new object[] { groupId }, cancellationToken);
            if (group != null)
            {
                _dbContext.Groups.Remove(group);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}