using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.Common.Token;

namespace SmartAC.AdminAPI.Controllers.Base
{
    public class BaseAuthController : ControllerBase
    {
        public string Token
        {
            get
            {
                var headers = HttpContext.Request.Headers;
                return headers["Authorization"];
            }
        }

        public string LoggedInUserId
        {
            get
            {
                return TokenHelper.GetId(Token);
            }
        }
    }
}
