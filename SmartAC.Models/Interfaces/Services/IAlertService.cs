using SmartAC.Models.Enums;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Responses.Alerts;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Services
{
    public interface IAlertService
    {
        GenericResponse Create(CreateAlertRequest request);
        DataGenericResponse<bool> ValidateSensorReading(decimal sensorReading, AlertType alertType);
        DataGenericResponse<bool> ValidateSensorReading(string sensorReading, AlertType alertType);
        DataGenericResponse<List<AlertViewModel>> GetActiveAlerts(ActiveAlertsViewStatusFilter viewStatus, ActiveAlertsResolutionStatusFilter resolutionStatus, SortingType sortingType, int pageNumber = 10, int pageSize = 10);
        GenericResponse ChangeAlertViewStatus(Guid alertId, AlertViewStatus viewStatus);
    }
}
