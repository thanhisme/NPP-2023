using Model.RequestModel;
using Model.ResponseModel;

namespace DomainService.Interfaces.ABC;

public interface IUserService
{
    public UserResponse? GetById(Guid id);

    public (int, List<UserResponse>) GetMany(KeywordWithPaginationRequest req);
}