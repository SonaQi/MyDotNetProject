using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyDotNetProject.Common.Authentication;
using System.Security.Claims;

namespace MyDotNetProject.WebApi.Authorize
{
    /// <summary>
    /// 接口Token验证
    /// </summary>
    public class TokenAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public readonly JwtHelper _jwtHelper;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jwtHelper"></param>
        public TokenAuthorizeAttribute(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (context.HttpContext.Request is null || context.HttpContext.Request.Headers is null || context.HttpContext.Request.Headers["Authorization"].Count <= 0)
                {
                    throw new Exception($@"Headers不能为空");
                }

                var token = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception($@"无权限,token为空");
                }

                ClaimsPrincipal principal;
                var isValid = _jwtHelper.ValidateJwtToken(token, out principal);
                if (!isValid)
                {
                    throw new Exception($@"无权限,token无效");
                }

                context.HttpContext.User = principal;
            }
            catch (Exception ex)
            {
                var statusCode = StatusCodes.Status401Unauthorized;
                string message = ex.Message;

                context.Result = new JsonResult(new { StatusCodeResult = statusCode, Msg = message });
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = statusCode;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.WriteAsync(message);
            }
        }
    }
}
