using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using SmartAC.Common.Token;
using SmartAC.Models.Consts;
using SmartAC.Models.Interfaces.Common;
using System;

namespace SmartAC.DevicesAPI.Attributes.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool containsHeader = context.HttpContext.Request.Headers.ContainsKey("Authorization");
            if (!containsHeader)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                ICacheManager cacheManager = context.HttpContext.RequestServices.GetService(typeof(ICacheManager)) as ICacheManager;
                string storedToken = cacheManager.Get(CommonConsts.TOKEN);

                IConfiguration configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                var token = context.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(storedToken) || storedToken != token)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else
                {
                    bool isTokenValid = TokenHelper.ValidateToken(token, CommonConsts.ISSUER_DEVICES_API, configuration.GetSection("AppSettings")["Secret"]);
                    if (!isTokenValid)
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
            }
        }
    }
}
