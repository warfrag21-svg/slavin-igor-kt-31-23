using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Interfaces
{
    public interface IGroupService
    {
        Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default);
        Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default);
        Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);
        Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default);
    }
}