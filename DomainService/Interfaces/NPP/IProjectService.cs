using Model.RequestModel;
using Model.ResponseModel;

namespace DomainService.Interfaces.NPP
{
    public interface IProjectService
    {
        public ProjectResponse? GetById(Guid id);

        public (int, List<ProjectResponse>) GetMany(KeywordWithPaginationRequest req);
    }
}
