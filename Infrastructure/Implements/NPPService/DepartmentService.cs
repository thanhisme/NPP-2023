using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.NPP;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
using Model.RequestModel;

namespace Infrastructure.Implements.NPPService
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : base(unitOfWork, memoryCache)
        {
        }

        public (int, List<NPPDepartment>) GetMany(KeywordWithPaginationRequest req)
        {
            var departments = _unitOfWork
                .Repository<NPPDepartment>()
                .Where(department => department.Name!.Contains(req.Keyword.ToLower()))
                .ToList();

            int skip = (req.Page - 1) * req.PageSize;

            return (departments.Count, departments.Skip(skip).Take(req.PageSize).ToList());
        }
    }
}
