using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;

namespace SmartAC.DevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
