using SmartAC.Models.Enums;
using SmartAC.Models.ViewModels.Requests.Alerts;
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
    }
}
