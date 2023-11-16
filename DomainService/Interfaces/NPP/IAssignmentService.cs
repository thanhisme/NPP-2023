using Entity.Entities;
using Model.RequestModel;
using Model.ResponseModel;

namespace DomainService.Interfaces.NPP
{
    public interface IAssignmentService
    {
        public NPPAssignment? GetById(Guid id);

        public (int, List<AssignmentResponse>) GetMany(AssignmentFilterRequest req);

        public Task<Guid?> Create(CreateAssignmentRequest req, Guid userId, string username);

        public Task<Guid?> Update(Guid id, UpdateAssignmentRequest req, Guid userId, string username);

        public Task<Guid?> Remove(Guid id);
    }
}
