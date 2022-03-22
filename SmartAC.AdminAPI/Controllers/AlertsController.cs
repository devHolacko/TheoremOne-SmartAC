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
using System.Net.Mime;

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

        /// <summary>
        /// An api that gets active alerts based on view and resolution status ascending or descending with pagination
        /// </summary>
        /// <param name="viewStatus">The view status of the alert</param>
        /// <param name="resolutionStatus">The resolution status of the alert</param>
        /// <param name="sortingType">The sorting type of the alerts (Ascending - Descending)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Required page number</param>
        /// <returns></returns>
        [Route("active/view-status/{view-status}/resolution-status/{resolution-status}/sorting/{sorting-type}/size/{page-size}/number/{page-number}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DataGenericResponse<List<AlertViewModel>>))]
        public IActionResult GetActiveAlerts([FromRoute(Name = "view-status")] ActiveAlertsViewStatusFilter viewStatus, [FromRoute(Name = "resolution-status")] ActiveAlertsResolutionStatusFilter resolutionStatus,
            [FromRoute(Name = "sorting-type")] SortingType sortingType, [FromRoute(Name = "page-size")] int pageSize, [FromRoute(Name = "page-number")] int pageNumber)
        {
            DataGenericResponse<List<AlertViewModel>> response = _alertService.GetActiveAlerts(viewStatus, resolutionStatus, sortingType, pageNumber, pageSize);

            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that marks an alert as viewed given alert's id
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        [Route("{alert-id}/view")]
        [HttpPatch]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(GenericResponse))]
        public IActionResult MarkAlertViewed([FromRoute(Name = "alert-id")] Guid alertId)
        {
            GenericResponse response = _alertService.ChangeAlertViewStatus(alertId, AlertViewStatus.Viewed);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// An api that marks an alert as ignored given alert's id
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        [Route("{alert-id}/ignore")]
        [HttpPatch]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(GenericResponse))]
        public IActionResult MarkAlertIgnored([FromRoute(Name = "alert-id")] Guid alertId)
        {
            GenericResponse response = _alertService.ChangeAlertResolutionStatus(alertId, AlertResolutionStatus.Ignored);
            return new OkObjectResult(response);
        }
    }
}
