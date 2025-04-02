using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities.Dto.Request
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
