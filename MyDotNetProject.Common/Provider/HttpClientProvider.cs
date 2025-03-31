using Microsoft.Extensions.Logging;
using MyDotNetProject.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Provider
{
    public class HttpClientProvider
    {
        private readonly ILogger<HttpClientProvider> _logger;

        public HttpClientProvider(ILogger<HttpClientProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///    异步 PostAsync 请求.
        /// </summary>
        /// <param name="url">url.</param>
        /// <param name="data">data.</param>
        /// <param name="mediaType">请求内容类型，默认 application/json</param>
        /// <param name="headers">请求头，默认 null</param>
        /// <returns>string</returns>
        public async Task<string> PostAsync(string url, object data, string mediaType = "application/json", Dictionary<string, string>? headers = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requestData = string.Empty;
            var responseData = string.Empty;
            try
            {
                if (data != null)
                {
                    requestData = data.GetType().Name == "String" ? data.ToString() : data.ToJson();
                }
                var content = new StringContent(requestData ?? string.Empty, Encoding.UTF8, mediaType);

                // 跳过HTTPS验证
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var _httpClient = new HttpClient(clientHandler);

                if (headers.IsNotNullOrEmpty())
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var response = await _httpClient.PostAsync(url, content);
                responseData = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;

                stopwatch.Stop();
                _logger.LogInformation($"Post: {url} success, elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}");

            }
            catch (Exception ex)
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }
                var errMsg = $"Post: {url}异常，error msg:{ex.Message}; elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}";
                _logger.LogError($"{errMsg}\n exception:{ex}");
                throw;
            }
            return responseData;
        }

        /// <summary>
        ///     Get 请求.
        /// </summary>
        /// <param name="url">url.</param>
        /// <param name="data">data.</param>
        /// <param name="mediaType">请求内容类型，默认 application/json</param>
        /// <param name="headers">请求头，默认 null</param>
        /// <param name="timeoutSeconds">超时时间(秒)</param>
        /// <returns>string</returns>
        public async Task<T> Get<T>(string url, object? data = null, string mediaType = "application/json", Dictionary<string, string>? headers = null, int timeoutSeconds = 60)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requestData = string.Empty;
            var responseData = string.Empty;
            var result = default(T);
            try
            {
                var uriBuilder = new UriBuilder(url);
                if (data != null)
                {
                    uriBuilder.Query = MakeQueryingString(data);
                }
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri)
                {
                    Content = new StringContent(requestData, Encoding.UTF8, mediaType),
                };
                if (headers.IsNotNullOrEmpty())
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                var _httpClient = new HttpClient();
                _httpClient.Timeout = TimeSpan.FromSeconds((double)timeoutSeconds);
                var response = await _httpClient.SendAsync(request);
                responseData = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
                result = JsonConvert.DeserializeObject<T>(responseData);
                stopwatch.Stop();
                _logger.LogInformation($"Get: {url} success, elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}");

            }
            catch (Exception ex)
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }
                var errMsg = $"Get: {url}异常，error msg:{ex.Message};elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}";
                _logger.LogError($"{errMsg}\n exception:{ex}");
                throw;
            }
            return result;
        }

        /// <summary>
        ///     Get 请求.
        /// </summary>
        /// <param name="url">url.</param>
        /// <param name="data">data.</param>
        /// <param name="mediaType">请求内容类型，默认 application/json</param>
        /// <param name="headers">请求头，默认 null</param>
        /// <returns>string</returns>
        public async Task<string> Get(string url, object? data = null, string mediaType = "application/json", Dictionary<string, string>? headers = null, int timeoutSeconds = 60)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requestData = string.Empty;
            var responseData = string.Empty;
            try
            {
                var uriBuilder = new UriBuilder(url);
                if (data != null)
                {
                    uriBuilder.Query = MakeQueryingString(data);
                }
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri)
                {
                    Content = new StringContent(requestData, Encoding.UTF8, mediaType),
                };
                if (headers.IsNotNullOrEmpty())
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                // 跳过HTTPS验证
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var _httpClient = new HttpClient(clientHandler);
                _httpClient.Timeout = TimeSpan.FromSeconds((double)timeoutSeconds);
                var response = await _httpClient.SendAsync(request);
                responseData = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
                stopwatch.Stop();
                _logger.LogInformation($"Get: {url} success, elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}");
            }
            catch (Exception ex)
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }
                var errMsg = $"Get: {url}异常，error msg:{ex.Message};elapsed time:{stopwatch.ElapsedMilliseconds}ms; request:{requestData},response:{responseData}";
                _logger.LogError($"{errMsg}\n exception:{ex}");
                throw;
            }
            return responseData;
        }

        /// <summary>
        /// 构建请求参数
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static string MakeQueryingString(object arguments)
        {
            if (arguments == null)
                return null;

            IDictionary args = TypeDescriptor.GetProperties(arguments).Cast<PropertyDescriptor>()
              .ToDictionary(p => p.Name, p => p.GetValue(arguments));

            if (args == null || args.Count == 0)
                return null;

            var dic = args.Keys.Cast<object>().ToDictionary(key => key.ToString(), key => args[key] == null ? null : args[key].ToString());

            return string.Join("&", dic.Select(item => WebUtility.UrlEncode(item.Key) + "=" + WebUtility.UrlEncode(item.Value)));
        }
    }
}
