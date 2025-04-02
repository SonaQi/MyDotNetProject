using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Common
{
    public class PollyHelper
    {
        /// <summary>
        /// 重试执行(有返回值)
        /// </summary>
        /// <typeparam name="TResponse">响应结果类型</typeparam>
        /// <param name="func">方法</param>
        /// <param name="isRetry">是否重试</param>
        /// <returns>响应结果</returns>
        public static TResponse Excute<TResponse>(Func<TResponse> func, bool isRetry = true)
        {
            var result = default(TResponse);
            if (!isRetry)
            {
                return func();
            }

            var waitAndRetry = Policy
                   .Handle<Exception>()
                   .WaitAndRetry(new[]
                       {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5)
                       });
            waitAndRetry.Execute(() =>
            {
                result = func();
            });
            return result;
        }

        /// <summary>
        /// 重试执行(无返回值)
        /// </summary>
        /// <param name="func">方法</param>
        /// <param name="isRetry">是否重试</param>
        public static void Excute(Action func, bool isRetry = true)
        {
            if (!isRetry)
            {
                func();
                return;
            }

            var waitAndRetry = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(new[]
                        {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5)
                        });
            waitAndRetry.Execute(() =>
            {
                func();
            });
        }

        /// <summary>
        /// 网络请求重试
        /// </summary>
        /// <param name="func">方法</param>
        /// <param name="isRetry">是否重试</param>
        /// <returns>返回结果</returns>
        public static string WebRequest(Func<string> func, bool isRetry = true)
        {
            if (!isRetry)
            {
                return func();
            }

            string result = string.Empty;
            var waitAndRetry = Policy
                   .Handle<WebException>(x => IsCanRetryStatus(x.Status))
                   .WaitAndRetry(new[]
                       {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5)
                       });
            waitAndRetry.Execute(() =>
            {
                result = func();
            });
            return result;
        }

        /// <summary>
        /// 是否可以重试
        /// </summary>
        /// <param name="status">异常状态</param>
        /// <returns>是否</returns>
        public static bool IsCanRetryStatus(WebExceptionStatus status)
        {
            return status == WebExceptionStatus.Timeout ||
                   status == WebExceptionStatus.ConnectionClosed ||
                   status == WebExceptionStatus.ConnectFailure ||
                   status == WebExceptionStatus.SendFailure ||
                   status == WebExceptionStatus.ReceiveFailure ||
                   status == WebExceptionStatus.RequestCanceled ||
                   status == WebExceptionStatus.KeepAliveFailure;
        }
    }
}
