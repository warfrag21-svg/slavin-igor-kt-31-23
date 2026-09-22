using Microsoft.AspNetCore.Mvc;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Models;

namespace SlavinIgorkt_31_23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("GetGroups")]
        public async Task<IActionResult> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken)
        {
            var groups = await _groupService.GetGroupsAsync(filter, cancellationToken);
            return Ok(groups);
        }

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroupAsync(Group group, CancellationToken cancellationToken)
        {
            var result = await _groupService.AddGroupAsync(group, cancellationToken);
            return Ok(result);
        }

        [HttpPost("UpdateGroup")]
        public async Task<IActionResult> UpdateGroupAsync(Group group, CancellationToken cancellationToken)
        {
            var result = await _groupService.UpdateGroupAsync(group, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("DeleteGroup/{groupId}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            await _groupService.DeleteGroupAsync(groupId, cancellationToken);
            return Ok();
        }
    }
}