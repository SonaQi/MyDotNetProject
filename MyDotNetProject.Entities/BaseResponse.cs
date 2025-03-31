using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities
{
    public class BaseResponse
    {
        /// <summary>
        /// 状态码 0：正常返回 -1：异常返回
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }

    /// <summary>
    /// 基础响应类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> : BaseResponse
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
