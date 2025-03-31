using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyDotNetProject.Common.Extensions;
using MyDotNetProject.Entities;

namespace MyDotNetProject.WebApi.Filter
{
    public class GlobalResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // 参数验证失败时返回
            if (!context.ModelState.IsValid)
            {
                var listMsg = new List<string>();
                foreach (var item in context.ModelState.Values)
                {
                    if (item.Errors.Count > 0)
                    {
                        listMsg.Add(item.Errors[0].ErrorMessage);
                    }
                }

                var result = new BaseResponse();
                result.code = -1;
                result.message = string.Format("参数验证失败:{0}", string.Join(";", listMsg));

                context.Result = new ContentResult
                {
                    Content = result.ToJson(),
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }

            await next();
        }
    }
}
