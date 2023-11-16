using Entity.Entities;

namespace DomainService.Interfaces.NPP
{
    public interface IDepartmentService
    {
        public (int, List<NPPDepartment>) GetMany(int page, int pageSize);
    }
}
