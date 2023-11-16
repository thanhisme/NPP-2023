using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.NPP;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Implements.NPPService
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : base(unitOfWork, memoryCache)
        {
        }

        public (int, List<NPPDepartment>) GetMany(int page, int pageSize)
        {
            var departments = _unitOfWork
                .Repository<NPPDepartment>()
                .ToList();

            int skip = (page - 1) * pageSize;

            return (departments.Count, departments.Skip(skip).Take(pageSize).ToList());
        }
    }
}
