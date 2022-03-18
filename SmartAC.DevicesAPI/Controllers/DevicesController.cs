using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;

namespace SmartAC.DevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : BaseAuthController
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [Route("registeration")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterDeviceRequest request)
        {
            GenericResponse response = _deviceService.Register(request);
            return new OkObjectResult(response);
        }
    }
}
