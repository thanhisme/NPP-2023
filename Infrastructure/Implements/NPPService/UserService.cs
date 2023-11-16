using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.ABC;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
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

        public (int, List<UserResponse>) GetMany()
        {
            var users = _unitOfWork
                .Repository<NPPUser>()
                .Where(user => !user.IsDeleted)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Department = user.Department,
                    DepartmentId = user.DepartmentId,
                })
                .ToList();

            return (users.Count, users);
        }
    }
}
