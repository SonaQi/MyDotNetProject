using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Authentication
{
    public class AccessTokenInfo
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }

    public class JwtSecurityConfig
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 到期时间（分钟）
        /// </summary>
        public int ExpirationMinutes { get; set; }
    }
}
