using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartAC.AdminAPI.Attributes.Auth;
using SmartAC.AdminAPI.Controllers.Base;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Responses.Alerts;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;

namespace SmartAC.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [Route("{alert-id}/view")]
        [HttpPatch]
        public IActionResult MarkAlertViewed([FromRoute(Name = "alert-id")] Guid alertId)
        {
            GenericResponse response = _alertService.ChangeAlertViewStatus(alertId, AlertViewStatus.Viewed);
            return new OkObjectResult(response);
        }

        [Route("{alert-id}/ignore")]
        [HttpPatch]
        public IActionResult MarkAlertIgnored([FromRoute(Name = "alert-id")] Guid alertId)
        {
            GenericResponse response = _alertService.ChangeAlertResolutionStatus(alertId, AlertResolutionStatus.Ignored);
            return new OkObjectResult(response);
        }
    }
}
