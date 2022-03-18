using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses.Alerts;
using SmartAC.Models.ViewModels.Responses.Base;
using System.Collections.Generic;

namespace SmartAC.DevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : BaseAuthController
    {
        private readonly IAlertService _alertService;
        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [Route("active/view-status/{view-status}/resolution-status/{resolution-status}/sorting/{sorting-type}/size/{page-size}/number/{page-number}")]
        [HttpGet]
        public IActionResult GetActiveAlerts([FromRoute(Name = "view-status")] ActiveAlertsViewStatusFilter viewStatus, [FromRoute(Name = "resolution-status")] ActiveAlertsResolutionStatusFilter resolutionStatus,
            [FromRoute(Name = "sorting-type")] SortingType sortingType, [FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber)
        {
            DataGenericResponse<List<AlertViewModel>> response = _alertService.GetActiveAlerts(viewStatus, resolutionStatus, sortingType, pageNumber, pageSize);

            return new OkObjectResult(response);
        }
    }
}
