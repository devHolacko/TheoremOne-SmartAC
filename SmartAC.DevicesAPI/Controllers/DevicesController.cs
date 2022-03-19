using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Devices;
using System;
using System.Collections.Generic;

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

        [Route("sensors/reporting")]
        [HttpPost]
        public IActionResult ReportSensorsReadings([FromBody] ReportDeviceReadingsRequest request)
        {
            GenericResponse response = _deviceService.ReportDeviceReadings(request);
            return new OkObjectResult(response);
        }

        [Route("recently-registered/size/{page-size}/number/{page-number}")]
        [HttpGet]
        public IActionResult GetRecentlyRegisteredDevices([FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber)
        {
            DataGenericResponse<List<DeviceRegisterationViewModel>> response = _deviceService.GetRecentlyRegisteredDevices(pageSize, pageNumber);
            return new OkObjectResult(response);
        }

        [Route("serial/{serial-number}")]
        [HttpGet]
        public IActionResult GetDevicesBySerialNumber([FromRoute(Name ="serial-number")]string serialNumber)
        {
            DataGenericResponse<List<DeviceViewModel>> response = _deviceService.FilterDevicesBySerial(serialNumber);
            return new OkObjectResult(response);
        }
    }
}
