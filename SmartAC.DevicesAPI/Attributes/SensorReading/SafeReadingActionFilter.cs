using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SmartAC.Common.Token;
using SmartAC.Models.Consts;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.SensorsReading;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.DevicesAPI.Attributes.SensorReading
{
    public class SafeReadingActionFilter : ActionFilterAttribute
    {
        public object RequestBody { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            object request = context.ActionArguments["request"];
            RequestBody = request;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            object request = RequestBody;

            if (request is ReportDeviceReadingsRequest)
            {
                IAlertService alertService = context.HttpContext.RequestServices.GetService(typeof(IAlertService)) as IAlertService;
                ISensorsReadingService sensorReadingService = context.HttpContext.RequestServices.GetService(typeof(ISensorsReadingService)) as ISensorsReadingService;
                ReportDeviceReadingsRequest castedRequest = request as ReportDeviceReadingsRequest;
                foreach (var reading in castedRequest.Readings)
                {
                    DataGenericResponse<bool> isTemperatureValid = alertService.ValidateSensorReading(reading.Temperature, AlertType.Temperature);
                    if (!isTemperatureValid.Data)
                    {
                        var sensorReadings = sensorReadingService.GetSensorReadings(castedRequest.DeviceId, reading.RecordedAt, reading.RecordedAt);
                        var recordedReading = sensorReadings.Data.FirstOrDefault(c => Enum.Parse<DeviceHealthStatus>(c.HealthStatus) == reading.HealthStatus && c.Temperature == reading.Temperature && c.CarbonMonoxide == reading.CarbonMonoxide && c.Humidity == reading.Humidity);
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Temperature value has exceeded danger limit",
                            Type = AlertType.Temperature,
                            DeviceId = castedRequest.DeviceId,
                            SensorReadingId = recordedReading.SensorReadingId
                        });
                    }
                    else
                    {
                        DataGenericResponse<bool> response = alertService.CheckUnresolvedAlerts(castedRequest.DeviceId, AlertType.Temperature);
                        if (response.Data)
                        {
                            alertService.ResolveByAlertType(castedRequest.DeviceId, AlertType.Temperature);
                        }
                    }
                    DataGenericResponse<bool> isCarbonMonoxideValid = alertService.ValidateSensorReading(reading.CarbonMonoxide, AlertType.CarbonMonoxide);
                    if (!isCarbonMonoxideValid.Data)
                    {
                        var sensorReadings = sensorReadingService.GetSensorReadings(castedRequest.DeviceId, reading.RecordedAt, reading.RecordedAt);
                        var recordedReading = sensorReadings.Data.FirstOrDefault(c => Enum.Parse<DeviceHealthStatus>(c.HealthStatus) == reading.HealthStatus && c.Temperature == reading.Temperature && c.CarbonMonoxide == reading.CarbonMonoxide && c.Humidity == reading.Humidity);
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "CO value has exceeded danger limit",
                            Type = AlertType.CarbonMonoxide,
                            DeviceId = castedRequest.DeviceId,
                            SensorReadingId = recordedReading.SensorReadingId
                        });
                    }
                    else
                    {
                        DataGenericResponse<bool> response = alertService.CheckUnresolvedAlerts(castedRequest.DeviceId, AlertType.CarbonMonoxide);
                        if (response.Data)
                        {
                            alertService.ResolveByAlertType(castedRequest.DeviceId, AlertType.CarbonMonoxide);
                        }
                    }
                    DataGenericResponse<bool> isHumidityValid = alertService.ValidateSensorReading(reading.Humidity, AlertType.Humidity);
                    if (!isHumidityValid.Data)
                    {
                        var sensorReadings = sensorReadingService.GetSensorReadings(castedRequest.DeviceId, reading.RecordedAt, reading.RecordedAt);
                        var recordedReading = sensorReadings.Data.FirstOrDefault(c => Enum.Parse<DeviceHealthStatus>(c.HealthStatus) == reading.HealthStatus && c.Temperature == reading.Temperature && c.CarbonMonoxide == reading.CarbonMonoxide && c.Humidity == reading.Humidity);
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Humidity value has exceeded danger limit",
                            Type = AlertType.Humidity,
                            DeviceId = castedRequest.DeviceId,
                            SensorReadingId = recordedReading.SensorReadingId
                        });
                    }
                    else
                    {
                        DataGenericResponse<bool> response = alertService.CheckUnresolvedAlerts(castedRequest.DeviceId, AlertType.CarbonMonoxide);
                        if (response.Data)
                        {
                            alertService.ResolveByAlertType(castedRequest.DeviceId, AlertType.CarbonMonoxide);
                        }
                    }
                    DataGenericResponse<bool> isHealthStatusValid = alertService.ValidateSensorReading(reading.HealthStatus.ToString("d"), AlertType.HealthStatus);
                    if (!isHealthStatusValid.Data)
                    {
                        var sensorReadings = sensorReadingService.GetSensorReadings(castedRequest.DeviceId, reading.RecordedAt, reading.RecordedAt);
                        var recordedReading = sensorReadings.Data.FirstOrDefault(c => Enum.Parse<DeviceHealthStatus>(c.HealthStatus) == reading.HealthStatus && c.Temperature == reading.Temperature && c.CarbonMonoxide == reading.CarbonMonoxide && c.Humidity == reading.Humidity);
                        alertService.Create(new CreateAlertRequest
                        {
                            AlertDate = reading.RecordedAt,
                            Message = "Device is reporting health problem",
                            Type = AlertType.HealthStatus,
                            DeviceId = castedRequest.DeviceId,
                            SensorReadingId = recordedReading.SensorReadingId
                        });
                    }
                    else
                    {
                        DataGenericResponse<bool> response = alertService.CheckUnresolvedAlerts(castedRequest.DeviceId, AlertType.HealthStatus);
                        if (response.Data)
                        {
                            alertService.ResolveByAlertType(castedRequest.DeviceId, AlertType.HealthStatus);
                        }
                    }
                }
            }
            else
            {
                string requestJson = JsonConvert.SerializeObject(request);

                ISensorsReadingService sensorReadingService = context.HttpContext.RequestServices.GetService(typeof(ISensorsReadingService)) as ISensorsReadingService;
                ICacheManager cacheManager = context.HttpContext.RequestServices.GetService(typeof(ICacheManager)) as ICacheManager;

                string token = cacheManager.Get(CommonConsts.TOKEN);

                string deviceId = TokenHelper.GetId(token);

                sensorReadingService.CreateInvalidReading(new Models.ViewModels.Requests.Devices.Sensors.CreateInvalidSensorReadingRequest { DeviceId = Guid.Parse(deviceId), Data = requestJson });
            }
        }

    }
}
