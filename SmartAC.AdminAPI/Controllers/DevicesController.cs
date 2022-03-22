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
using System.Net.Mime;

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

        /// <summary>
        /// An api that gets recently registered devices with pagination
        /// </summary>
        /// <param name="pageSize">Number of items in a page</param>
        /// <param name="pageNumber">Required page number</param>
        /// <returns></returns>
        [Route("recently-registered/size/{page-size}/number/{page-number}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(List<DeviceRegisterationViewModel>))]
        public IActionResult GetRecentlyRegisteredDevices([FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber)
        {
            DataGenericResponse<List<DeviceRegisterationViewModel>> response = _deviceService.GetRecentlyRegisteredDevices(pageSize, pageNumber);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that gets devices list that their serial number contains the given serial number value
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        [Route("serial/{serial-number}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<List<DeviceViewModel>>))]
        public IActionResult GetDevicesBySerialNumber([FromRoute(Name = "serial-number")] string serialNumber)
        {
            DataGenericResponse<List<DeviceViewModel>> response = _deviceService.FilterDevicesBySerial(serialNumber);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that gets list of devices registered within a date range
        /// </summary>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns></returns>
        [Route("from/{from-date}/to/{to-date}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<List<DeviceViewModel>>))]
        public IActionResult GetDevicesByRegisterationDate([FromRoute(Name = "from-date")] DateTime fromDate, [FromRoute(Name = "to-date")] DateTime toDate)
        {
            DataGenericResponse<List<DeviceViewModel>> response = _deviceService.GetDevicesByRegisterationDate(fromDate, toDate);
            return new OkObjectResult(response);
        }
    }
}
