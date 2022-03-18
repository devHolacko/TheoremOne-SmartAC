using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;
using System;

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

        [Route("{device-id}/sensors/reporting")]
        [HttpPost]
        public IActionResult ReportSensorsReadings([FromRoute(Name = "device-id")] Guid deviceId, [FromBody] ReportDeviceReadingsRequest request)
        {
            request.DeviceId = deviceId;
            GenericResponse response = _deviceService.ReportDeviceReadings(request);
            return new OkObjectResult(response);
        }
    }
}
