using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyDotNetProject.Entities;

namespace MyDotNetProject.WebApi.Filter
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                string path = context.HttpContext.Request.Path;
                this._logger.LogError($"异常信息：{path},{context.Exception}");

                var result = new BaseResponse();
                result.code = -1;
                result.message = context.Exception.Message;

                context.Exception = null;
                context.ExceptionHandled = true;
                context.Result = new ObjectResult(result);
            }

            return Task.CompletedTask;
        }
    }
}
