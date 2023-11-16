using ABCAPI.Controllers;
using DomainService.Interfaces.NPP;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModel;
using System.Security.Claims;

namespace NPP.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AssignmentController : BaseController
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IHttpContextAccessor httpContextAccessor, IAssignmentService assignmentService) : base(httpContextAccessor)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssignmentRequest req)
        {
            var (userId, username) = GetCurrentUser();
            var id = await _assignmentService.Create(req, userId, username);

            return Ok(id);
        }

        [HttpGet]
        public IActionResult GetMany([FromQuery] AssignmentFilterRequest req)
        {
            var (totalRecord, assignments) = _assignmentService.GetMany(req);

            return Ok(assignments, totalRecord);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var assignment = _assignmentService.GetById(id);

            return Ok(assignment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssignmentRequest req)
        {
            var (userId, username) = GetCurrentUser();
            var updatedId = await _assignmentService.Update(id, req, userId, username);

            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var removedId = await _assignmentService.Remove(id);

            return Ok(removedId);
        }

        private (Guid, string) GetCurrentUser()
        {
            string userId = User.FindFirst("Id")!.Value;
            string username = User.FindFirst(ClaimTypes.Role)!.Value;

            return (Guid.Parse(userId), username);
        }
    }
}
