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
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("id", id.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create JWToken
            JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(issuer: issuer,
                audience: "AUTHORITY",
                subject: claims,
                expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);

            return tokenHandler.WriteToken(securityToken);
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

            SecurityToken token;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {

                IssuerSigningKey = key,
                ValidIssuer = issuer,
                ValidAudience = "AUTHORITY",
            };
            validationParameters.ValidIssuer = issuer;
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(stringToken, validationParameters, out token);
                if (token.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
