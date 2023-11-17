using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.ABC;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
using Model.RequestModel;
using Model.ResponseModel;

namespace Infrastructure.Implements.NPPService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : base(unitOfWork, memoryCache)
        {
        }

        public UserResponse? GetById(Guid id)
        {
            var user = _unitOfWork
                .Repository<NPPUser>()
                .Where(user => user.Id == id && !user.IsDeleted)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Department = user.Department,
                    DepartmentId = user.DepartmentId,
                })
                .FirstOrDefault();

            return user;
        }

        public (int, List<UserResponse>) GetMany(KeywordWithPaginationRequest req)
        {
            bool isGuidForm = Guid.TryParse(req.Keyword, out Guid userId);
            var users = _unitOfWork
                .Repository<NPPUser>()
                .Where(user => !user.IsDeleted &&
                        isGuidForm ? user.Id == userId : user.FullName.Contains(req.Keyword.ToLower())
                )
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Department = user.Department,
                    DepartmentId = user.DepartmentId,
                })
            .ToList();

            int skip = (req.Page - 1) * req.PageSize;

            return (users.Count, users.Skip(skip).Take(req.PageSize).ToList());
        }
    }
}
