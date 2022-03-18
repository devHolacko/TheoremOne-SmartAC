using AutoMapper;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.Alerts;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Alerts;
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
            if(reading == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_SENSOR_READING);
            }

            Alert alert = _mapper.Map<Alert>(request);
            alert.ViewStatus = AlertViewStatus.New;
            alert.ResolutionStatus = AlertResolutionStatus.New;

            _alertDataService.CreateAlert(alert);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }

        public DataGenericResponse<bool> ValidateSensorReading(decimal sensorReading, AlertType alertType)
        {
            throw new NotImplementedException();
        }
    }
}
