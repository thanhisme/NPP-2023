using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces.NPP;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
using Model.ResponseModel;

namespace Infrastructure.Implements.NPPService
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : base(unitOfWork, memoryCache)
        {
        }

        public ProjectResponse? GetById(Guid id)
        {
            var project = _unitOfWork
                .Repository<NPPProject>()
                .Where(project => project.Id == id && !project.IsDeleted)
                .Select(project => new ProjectResponse
                {
                    Id = project.Id,
                    Code = project.Code,
                    Name = project.Name
                })
                .FirstOrDefault();

            return project;
        }

        public (int, List<ProjectResponse>) GetMany(int page, int pageSize)
        {
            var projects = _unitOfWork
                .Repository<NPPProject>()
                .Select(project => new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Code = project.Code,
                })
                .ToList();

            int skip = (page - 1) * pageSize;

            return (projects.Count, projects.Skip(skip).Take(pageSize).ToList());
        }
    }
}
