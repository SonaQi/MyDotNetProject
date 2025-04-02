using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyDotNetProject.Common.MemoryCache;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Authentication
{
    public class JwtHelper
    {
        private readonly JwtSecurityConfig _jwtSecurityInfo;
        public const string ExpKey = "ExpKey";

        public JwtHelper(IOptions<JwtSecurityConfig> options)
        {
            _jwtSecurityInfo = options.Value;
        }

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public AccessTokenInfo GenerateJwtToken(List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ExpKey, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecurityInfo.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var nowDate = DateTime.Now;
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSecurityInfo.Issuer,
                audience: _jwtSecurityInfo.Audience,
                claims: claims,
                notBefore: nowDate,
                expires: nowDate.AddMinutes(_jwtSecurityInfo.ExpirationMinutes),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(jwtToken);
            return new AccessTokenInfo { AccessToken = accessToken, ExpiredTime = _jwtSecurityInfo.ExpirationMinutes };
        }

        /// <summary>
        /// 验证JwtToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public bool ValidateJwtToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecurityInfo.SecretKey);
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSecurityInfo.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSecurityInfo.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                principal = tokenHandler.ValidateToken(token, parameters, out _);
                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                return false;
            }
            catch (SecurityTokenException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
