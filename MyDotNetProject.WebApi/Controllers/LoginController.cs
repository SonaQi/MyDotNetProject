using Microsoft.AspNetCore.Mvc;
using MyDotNetProject.Common.Authentication;
using MyDotNetProject.Entities;
using MyDotNetProject.Entities.Dto.Request;
using MyDotNetProject.WebApi.Authorize;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyDotNetProject.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {        
        private readonly ILogger<LoginController> _logger;
        private readonly JwtHelper _jwtHelper;

        public LoginController(ILogger<LoginController> logger,
            JwtHelper jwtHelper)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// 获取访问令牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseResponse<AccessTokenInfo>> GetAccessToken([FromBody] LoginRequest request)
        {
            // todo 验证账号密码

            // todo 获取账号信息

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sid, "userid"),
                new Claim(JwtRegisteredClaimNames.Name, "username"),
            };

            var result = _jwtHelper.GenerateJwtToken(claims);

            return new BaseResponse<AccessTokenInfo>(result);
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(TokenAuthorizeAttribute))]
        [HttpGet]
        public async Task<BaseResponse<AccessTokenInfo>> RefreshToken()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sid, "userid"),
                new Claim(JwtRegisteredClaimNames.Name, "username"),
            };

            var result = _jwtHelper.GenerateJwtToken(claims);
            return new BaseResponse<AccessTokenInfo>(result);
        }
    }
}
