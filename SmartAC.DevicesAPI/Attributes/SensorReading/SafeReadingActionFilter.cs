using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.SensorsReading;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.DevicesAPI.Attributes.SensorReading
{
    public class SafeReadingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            object request = context.ActionArguments["request"];
            IAlertService alertService = context.HttpContext.RequestServices.GetService(typeof(IAlertService)) as IAlertService;
            if (request is ReportDeviceReadingsRequest)
            {
                ReportDeviceReadingsRequest castedRequest = request as ReportDeviceReadingsRequest;
                foreach (var reading in castedRequest.Readings)
                {
                    DataGenericResponse<bool> isTemperatureValid = alertService.ValidateSensorReading(reading.Temperature, AlertType.Temperature);
                    if (!isTemperatureValid.Data)
                    {
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Temperature value has exceeded danger limit",
                            Type = AlertType.Temperature,
                            DeviceId = castedRequest.DeviceId,
                            //SensorReadingId = castedRequest.sens
                        });
                    }
                    DataGenericResponse<bool> iscarbonMonoxideValid = alertService.ValidateSensorReading(reading.CarbonMonoxide, AlertType.CarbonMonoxide);
                    if (!isTemperatureValid.Data)
                    {
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "CO value has exceeded danger limit",
                            Type = AlertType.CarbonMonoxide,
                            DeviceId = castedRequest.DeviceId,
                            //SensorReadingId = castedRequest.sens
                        });
                    }
                    DataGenericResponse<bool> isHumidityValid = alertService.ValidateSensorReading(reading.Humidity, AlertType.Humidity);
                    if (!isTemperatureValid.Data)
                    {
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Humidity value has exceeded danger limit",
                            Type = AlertType.Humidity,
                            DeviceId = castedRequest.DeviceId,
                            //SensorReadingId = castedRequest.sens
                        });
                    }
                    DataGenericResponse<bool> isHealthStatusValid = alertService.ValidateSensorReading(reading.HealthStatus.ToString("d"), AlertType.HealthStatus);
                    if (!isTemperatureValid.Data)
                    {
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Device is reporting health problem",
                            Type = AlertType.HealthStatus,
                            DeviceId = castedRequest.DeviceId,
                            //SensorReadingId = castedRequest.sens
                        });
                    }
                }
            }
            else
            {

            }
        }

    }
}
