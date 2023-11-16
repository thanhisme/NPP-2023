using ABCAPI.Controllers;
using DomainService.Interfaces.NPP;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModel;

namespace NPP.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IHttpContextAccessor httpContextAccessor, IProjectService projectService) : base(httpContextAccessor)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetMany([FromQuery] PaginationRequest req)
        {
            var (totalRecords, users) = _projectService.GetMany(req.Page, req.PageSize);

            return Ok(users, totalRecords);
        }
    }
}
