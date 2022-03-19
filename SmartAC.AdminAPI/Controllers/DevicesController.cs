using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.AdminAPI.Attributes.Auth;
using SmartAC.AdminAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Devices;
using System;
using System.Collections.Generic;

namespace SmartAC.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : BaseAuthController
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
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
        public IActionResult GetDevicesBySerialNumber([FromRoute(Name = "serial-number")] string serialNumber)
        {
            DataGenericResponse<List<DeviceViewModel>> response = _deviceService.FilterDevicesBySerial(serialNumber);
            return new OkObjectResult(response);
        }

        [Route("from/{from-date}/to/{to-date}")]
        [HttpGet]
        public IActionResult GetDevicesByRegisterationDate([FromRoute(Name = "from-date")] DateTime fromDate, [FromRoute(Name = "to-date")] DateTime toDate)
        {
            DataGenericResponse<List<DeviceViewModel>> response = _deviceService.GetDevicesByRegisterationDate(fromDate, toDate);
            return new OkObjectResult(response);
        }
    }
}
