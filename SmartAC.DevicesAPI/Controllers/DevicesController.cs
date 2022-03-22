using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Attributes.Auth;
using SmartAC.DevicesAPI.Attributes.SensorReading;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Devices;
using System;
using System.Collections.Generic;
using System.Net.Mime;

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

        /// <summary>
        /// An api that registers a new device given some device details. A JWT token is generated if the request is valid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("registeration")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<string>))]
        public IActionResult Register([FromBody] RegisterDeviceRequest request)
        {
            DataGenericResponse<string> response = _deviceService.Register(request);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that records sensors readings per device
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("sensors/reporting")]
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(SafeReadingActionFilter))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(GenericResponse))]
        public IActionResult ReportSensorsReadings([FromBody] ReportDeviceReadingsRequest request)
        {
            GenericResponse response = _deviceService.ReportDeviceReadings(request);
            return new OkObjectResult(response);
        }
    }
}
