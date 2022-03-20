using AutoMapper;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.Alerts;
using SmartAC.Models.Validations.SensorsReading;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Responses.Alerts;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly IMapper _mapper;
        private readonly IAlertDataService _alertDataService;
        private readonly IDeviceDataService _deviceDataService;
        private readonly ISensorsReadingDataService _sensorsReadingDataService;
        public AlertService(IMapper mapper, IAlertDataService alertDataService, IDeviceDataService deviceDataService, ISensorsReadingDataService sensorsReadingDataService)
        {
            _mapper = mapper;
            _alertDataService = alertDataService;
            _deviceDataService = deviceDataService;
            _sensorsReadingDataService = sensorsReadingDataService;

        }
        public GenericResponse Create(CreateAlertRequest request)
        {
            GenericResponse response = new GenericResponse();
            CreateAlertValidator validator = new CreateAlertValidator();

            if (request == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_REQUEST);
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return response.CreateFailureResponse(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
            }

            Device selectedDevice = _deviceDataService.GetDeviceById(request.DeviceId);
            if (selectedDevice == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_DEVICE_ID);
            }

            SensorsReading reading = _sensorsReadingDataService.GetSensorReadingById(request.SensorReadingId);
            if (reading == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_SENSOR_READING);
            }

            bool duplicateAlert = _alertDataService.GetAlerts(c => c.DeviceId == request.DeviceId && c.SensorReadingId == request.SensorReadingId && c.Type == request.Type && ((c.ViewStatus == AlertViewStatus.New && c.ResolutionStatus == AlertResolutionStatus.New) || (c.ResolutionDate.HasValue && request.AlertDate.ToUniversalTime() < c.ResolutionDate))).Any();
            if (duplicateAlert)
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
            }



            Alert alert = _mapper.Map<Alert>(request);
            alert.ViewStatus = AlertViewStatus.New;
            alert.ResolutionStatus = AlertResolutionStatus.New;

            _alertDataService.CreateAlert(alert);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }

        public DataGenericResponse<bool> ValidateSensorReading(decimal sensorReading, AlertType alertType)
        {
            bool isValid = SensorsReadingValidator.Validate(sensorReading, alertType);

            return new DataGenericResponse<bool>().CreateSuccessResponse(ErrorCodesConsts.SUCCESS, isValid);
        }

        public DataGenericResponse<bool> ValidateSensorReading(string sensorReading, AlertType alertType)
        {
            DataGenericResponse<bool> response = new DataGenericResponse<bool>();

            bool isParsed = Enum.TryParse(sensorReading, out DeviceHealthStatus healthStatus);
            if (!isParsed)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_SENSOR_READING);
            }

            if (healthStatus != DeviceHealthStatus.OK)
            {
                string statusValue = Enum.GetName(typeof(DeviceHealthStatus), healthStatus);
                return response.CreateSuccessResponse(statusValue, false);
            }

            return new DataGenericResponse<bool>().CreateSuccessResponse(ErrorCodesConsts.SUCCESS, true);
        }

        public DataGenericResponse<List<AlertViewModel>> GetActiveAlerts(ActiveAlertsViewStatusFilter viewStatus, ActiveAlertsResolutionStatusFilter resolutionStatus, SortingType sortingType, int pageNumber = 10, int pageSize = 10)
        {
            DataGenericResponse<List<AlertViewModel>> response = new DataGenericResponse<List<AlertViewModel>>();
            IEnumerable<Alert> query = _alertDataService.GetAlerts(c => c.Active);
            switch (viewStatus)
            {
                case ActiveAlertsViewStatusFilter.Unviewed:
                    query = query.Where(c => c.ViewStatus == AlertViewStatus.New);
                    break;
                default:
                    break;
            }

            switch (resolutionStatus)
            {
                case ActiveAlertsResolutionStatusFilter.Unresolved:
                    query = query.Where(c => c.ResolutionStatus == AlertResolutionStatus.New);
                    break;
                default:
                    break;
            }

            switch (sortingType)
            {
                case SortingType.Ascending:
                    query = query.OrderBy(c => c.AlertDate);
                    break;
                case SortingType.Descending:
                    query = query.OrderByDescending(c => c.AlertDate);
                    break;
                default:
                    query = query.OrderBy(c => c.AlertDate);
                    break;
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!query.Any())
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, new List<AlertViewModel>());
            }

            List<Alert> alertsList = query.ToList();

            List<AlertViewModel> mappedAlerts = _mapper.Map<List<AlertViewModel>>(alertsList);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, mappedAlerts);
        }

        public GenericResponse ChangeAlertViewStatus(Guid alertId, AlertViewStatus viewStatus)
        {
            GenericResponse response = new GenericResponse();

            Alert selectedAlert = _alertDataService.GetAlertById(alertId);
            if (selectedAlert == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.NOT_FOUND);
            }

            if (selectedAlert.ViewStatus != viewStatus)
            {
                selectedAlert.ViewStatus = viewStatus;
                _alertDataService.EditAlert(selectedAlert);
            }

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }

        public GenericResponse ChangeAlertResolutionStatus(Guid alertId, AlertResolutionStatus resolutionStatus)
        {
            GenericResponse response = new GenericResponse();

            Alert selectedAlert = _alertDataService.GetAlertById(alertId);
            if (selectedAlert == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.NOT_FOUND);
            }

            if (selectedAlert.ResolutionStatus != resolutionStatus)
            {
                selectedAlert.ResolutionStatus = resolutionStatus;
                _alertDataService.EditAlert(selectedAlert);
            }

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }
    }
}
