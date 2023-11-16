using Common.Constant;
using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.ABC;
using DomainService.Interfaces.NPP;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
using Model.RequestModel;
using Model.ResponseModel;

namespace Infrastructure.Implements.NPPService
{
    public class AssignmentService : BaseService, IAssignmentService
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public AssignmentService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IUserService userService,
            IProjectService projectService
        ) : base(unitOfWork, memoryCache)
        {
            _userService = userService;
            _projectService = projectService;
        }

        public NPPAssignment? GetById(Guid id)
        {
            var assignment = _unitOfWork
                .Repository<NPPAssignment>()
                .Where(assignment => assignment.Id == id && !assignment.IsDeleted)
                .FirstOrDefault();

            return assignment ?? throw new ApplicationException(CommonMessage.Message_NotFound);
        }

        public async Task<Guid?> Create(CreateAssignmentRequest req, Guid userId, string username)
        {
            var assignee = _userService.GetById(req.AssigneeId);
            var project = _projectService.GetById(req.ProjectId);

            var assignment = new NPPAssignment
            {
                Description = req.Description,
                Note = req.Note ?? "",
                StartDate = req.StartDate,
                DueDate = req.DueDate,
                Assignee = assignee!.FullName,
                Project = project!.Name,
                Creator = username,
                CreatorId = userId,
                Modifier = username,
                ModifierId = userId
            };

            _unitOfWork.Repository<NPPAssignment>().Add(assignment);
            await _unitOfWork.SaveChangesAsync();

            return assignment.Id;
        }

        public async Task<Guid?> Update(Guid id, UpdateAssignmentRequest req, Guid userId, string username)
        {
            var assignment = GetById(id);

            if (assignment == null)
            {
                throw new ApplicationException(CommonMessage.Message_NotFound);
            }

            assignment.Description = req.Description ?? assignment.Description;
            assignment.Note = req.Note ?? assignment.Note;
            assignment.StartDate = req.StartDate ?? assignment.StartDate;
            assignment.DueDate = req.DueDate ?? assignment.DueDate;
            assignment.Modifier = username;
            assignment.ModifierId = userId;
            assignment.UpdatedDate = DateTime.Now;

            if (req.AssigneeId.HasValue)
            {
                var assignee = _userService.GetById(req.AssigneeId.Value);
                assignment.AssigneeId = req.AssigneeId.Value;
                assignment.Assignee = assignee!.FullName;
            }

            if (req.ProjectId.HasValue)
            {
                var project = _projectService.GetById(req.ProjectId.Value);
                assignment.ProjectId = req.ProjectId.Value;
                assignment.Project = project!.Name;
            }

            await _unitOfWork.SaveChangesAsync();

            return id;
        }

        public async Task<Guid?> Remove(Guid id)
        {
            var assignment = GetById(id);

            if (assignment == null)
            {
                throw new ApplicationException(CommonMessage.Message_NotFound);
            }

            assignment.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();

            return id;
        }

        public (int, List<AssignmentResponse>) GetMany(AssignmentFilterRequest req)
        {
            var assignmentDbSet = _unitOfWork.Repository<NPPAssignment>();
            var userDbSet = _unitOfWork.Repository<NPPUser>();

            var assignments = (from assignment
                               in assignmentDbSet
                               join user in userDbSet on assignment.AssigneeId equals user.Id
                               where !assignment.IsDeleted &&
                                    (req.Project == null || assignment.ProjectId == req.Project) &&
                                    (req.Department == null || user.DepartmentId == req.Department)
                               group assignment
                               by new { assignment.Assignee, assignment.AssigneeId }
                               into userAssignments
                               select new AssignmentResponse
                               {
                                   Assignee = userAssignments.Key.Assignee,
                                   AssigneeId = userAssignments.Key.AssigneeId,
                                   assignments = userAssignments.Select(assignment => new AssignmentItem
                                   {
                                       Id = assignment.Id,
                                       Description = assignment.Description,
                                       Project = assignment.Project,
                                       ProjectId = assignment.ProjectId,
                                       State = assignment.State,
                                       StartDate = assignment.StartDate,
                                       DueDate = assignment.DueDate,
                                       FinishDate = assignment.FinishDate,
                                       Creator = assignment.Creator,
                                       CreatorId = assignment.CreatorId,
                                       CreatedDate = assignment.CreatedDate
                                   }
                                   ).ToList()
                               }).ToList();

            return (
                assignments.Count,
                assignments
                    .Skip((req.Page - 1) * req.PageSize)
                    .Take(req.PageSize)
                    .ToList()
            );
        }
    }
}
