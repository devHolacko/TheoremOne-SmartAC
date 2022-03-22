using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.AdminAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Users;
using SmartAC.Models.ViewModels.Responses.Base;
using System.Net.Mime;

namespace SmartAC.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseAuthController
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// An api that authenticates the user if the credentials are correct. Generates JWT token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<string>))]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            DataGenericResponse<string> response = _userService.Login(request);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that deletes the user session. Any generated tokens will be invalid
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(GenericResponse))]
        public IActionResult Logout()
        {
            GenericResponse response = _userService.Logout();
            return new OkObjectResult(response);
        }
    }
}
