using Entity.Entities;
using Model.RequestModel;

namespace DomainService.Interfaces.NPP
{
    public interface IDepartmentService
    {
        public (int, List<NPPDepartment>) GetMany(KeywordWithPaginationRequest req);
    }
}
