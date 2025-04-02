using Microsoft.AspNetCore.Mvc.Filters;
using MyDotNetProject.Common.Extensions;

namespace MyDotNetProject.WebApi.Filter
{
    /// <summary>
    /// 请求日志
    /// </summary>
    public class RequestLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<RequestLoggingFilter> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var args = context.ActionArguments.Select(a => $"{a.Value.ToJson()}");
            _logger.LogInformation($"Request {request.Method} {request.Path} Param:【{args.ToJson()}】");
            base.OnActionExecuting(context);
        }
    }
}
