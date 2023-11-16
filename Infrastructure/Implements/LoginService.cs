using Common.Authorization.Utils;
using Common.Settings;
using Common.UnitOfWork.UnitOfWorkPattern;
using DomainService.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Model.RequestModel;
using Model.ResponseModel;

namespace Infrastructure.Implements
{
    public class LoginService : BaseService, ILoginService
    {
        private IJwtUtils _jwtUtils;
        private readonly StrJWT _strJwt;
        private readonly AppSettings _appSettings;

        public LoginService(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IJwtUtils jwtUtils,
            IOptions<StrJWT> strJwt, IOptions<AppSettings> appSettings) : base(unitOfWork, memoryCache)
        {
            _jwtUtils = jwtUtils;
            this._strJwt = strJwt.Value;
            this._appSettings = appSettings.Value;
        }

        public LoginResponse Login(LoginRequest model, DeviceInfoRequest deviceInfo, string ipAddress)
        {
            var mockUserId = Guid.Parse("CE79DE25-01D4-42E7-969E-38B8E3A11FA9");
            var jwtToken = _jwtUtils.GenerateToken(mockUserId, "Quản lí 1", deviceInfo.UDID);
            string? skey = _strJwt.Key;
            string? issuer = _strJwt.Issuer;
            string? audience = _strJwt.Audience;
            var refreshToken = _jwtUtils.GenerateRefreshToken(mockUserId, "Quản lí 1", deviceInfo.UDID, skey,
                issuer, audience, ipAddress);


            var retUser = new LoginResponse();
            retUser.userId = mockUserId;
            retUser.SetToken(jwtToken);
            retUser.SetRefreshToken(refreshToken.Token);
            return retUser;
        }
    }
}