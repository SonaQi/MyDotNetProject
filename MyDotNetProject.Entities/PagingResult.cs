using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T> where T : class
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public PagingResult()
        {
            this.TotalCount = 0;
            this.Data = new List<T>();
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 应用集合
        /// </summary>
        public List<T> Data { get; set; }
    }
}
