using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.AdminAPI.Attributes.Auth;
using SmartAC.AdminAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace SmartAC.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SensorsController : BaseAuthController
    {
        private readonly ISensorsReadingService _sensorsReadingService;
        public SensorsController(ISensorsReadingService sensorsReadingService)
        {
            _sensorsReadingService = sensorsReadingService;
        }

        /// <summary>
        /// An api that gets sensor readings recorded by a device given the device's id within date range with pagination
        /// </summary>
        /// <param name="deviceId">Id of the device</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Required page number</param>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns></returns>
        [Route("devices/{device-id}/readings/size/{page-size}/number/{page-number}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<List<SensorReadingsResponseViewModel>>))]
        public IActionResult GetDeviceSensorReadingsByDateRange([FromRoute(Name = "device-id")] Guid deviceId, [FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber,
            [FromQuery(Name = "from")] DateTime? fromDate, [FromQuery(Name = "to")] DateTime? toDate)
        {
            DataGenericResponse<List<SensorReadingsResponseViewModel>> response = _sensorsReadingService.GetSensorReadings(deviceId, fromDate, toDate, pageNumber, pageSize);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that gets an aggregation of a device's sensors readings within a date range given the device's id
        /// </summary>
        /// <param name="deviceId">Device id</param>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns></returns>
        [Route("aggregation/devices/{device-id}/from/{from-date}/to/{to-date}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<List<BucketViewModel>>))]
        public IActionResult AggregateSensorReadingByDateRange([FromRoute(Name = "device-id")] Guid deviceId, [FromRoute(Name = "from-date")] DateTime fromDate, [FromRoute(Name = "to-date")] DateTime toDate)
        {
            DataGenericResponse<List<BucketViewModel>> response = _sensorsReadingService.AggregateSensorReadingByDateRange(deviceId, fromDate, toDate);
            return new OkObjectResult(response);
        }
    }
}
