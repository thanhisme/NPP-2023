using Common.Utils;
using DomainService.Interfaces;
using DomainService.Interfaces.ABC;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModel;

namespace ABCAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public UserController(IHttpContextAccessor httpContextAccessor, ILoginService loginService, IUserService userService) : base(httpContextAccessor)
        {
            _loginService = loginService;
            _userService = userService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var deviceInfo = Utils.GetDeviceInfo(Request);
            var res = _loginService.Login(model, deviceInfo, IpAddress());
            return Ok(res, model);
        }

        [HttpGet]
        public IActionResult GetMany([FromQuery] KeywordWithPaginationRequest req)
        {
            var (totalRecords, users) = _userService.GetMany(req);

            return Ok(users, totalRecords);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}