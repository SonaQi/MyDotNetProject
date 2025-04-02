using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities
{
    public class PagingRequest
    {
        /// <summary>
        /// 每页容量
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;
    }
}
