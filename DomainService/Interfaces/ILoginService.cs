using Model.RequestModel;
using Model.ResponseModel;

namespace DomainService.Interfaces;

public interface ILoginService
{
    LoginResponse Login(LoginRequest model, DeviceInfoRequest deviceInfo, string ipAddress);
}