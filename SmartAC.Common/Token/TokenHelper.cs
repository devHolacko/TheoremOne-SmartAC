using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Common.Token
{
    public class TokenHelper
    {
        public static string GenerateJwtToken(string secret, Guid id, string issuer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            DateTime currentDateTime = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", id.ToString())
                }),
                Expires = currentDateTime.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = currentDateTime,
                Issuer = issuer

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static DateTime GetTokenIssuedAt(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).IssuedAt;
        }

        public static string GetId(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "id").Value;
        }

        public static bool ValidateToken(string stringToken, string issuer, string secret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenValidations = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidIssuer = issuer
            };
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(stringToken, tokenValidations, out SecurityToken securityToken);
                if (securityToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
