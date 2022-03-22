using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.AdminAPI.Attributes.Auth;
using SmartAC.AdminAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;

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

        [Route("devices/{device-id}/readings/size/{page-size}/number/{page-number}")]
        [HttpGet]
        public IActionResult GetDeviceSensorReadingsByDateRange([FromRoute(Name = "device-id")] Guid deviceId, [FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber,
            [FromQuery(Name = "from")] DateTime? fromDate, [FromQuery(Name = "to")] DateTime? toDate)
        {
            DataGenericResponse<List<SensorReadingsResponseViewModel>> response = _sensorsReadingService.GetSensorReadings(deviceId, fromDate, toDate, pageNumber, pageSize);
            return new OkObjectResult(response);
        }

        [Route("aggregation/devices/{device-id}/from/{from-date}/to/{to-date}")]
        [HttpGet]
        public IActionResult AggregateSensorReadingByDateRange([FromRoute(Name = "device-id")] Guid deviceId, [FromRoute(Name = "from-date")] DateTime fromDate, [FromRoute(Name = "to-date")] DateTime toDate)
        {
            DataGenericResponse<BucketViewModel> response = _sensorsReadingService.AggregateSensorReadingByDateRange(deviceId, fromDate, toDate);
            return new OkObjectResult(response);
        }
    }
}
