using AutoMapper;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.SensorsReading;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Sensors
{
    public class SensorsReadingService : ISensorsReadingService
    {
        private readonly ISensorsReadingDataService _sensorsReadingDataService;
        private readonly IInvalidSensorsReadingDataService _invalidSensorsReadingDataService;
        private readonly IAlertDataService _alertDataService;
        private readonly IMapper _mapper;
        public SensorsReadingService(ISensorsReadingDataService sensorsReadingDataService, IMapper mapper, IAlertDataService alertDataService, IInvalidSensorsReadingDataService invalidSensorsReadingDataService)
        {
            _sensorsReadingDataService = sensorsReadingDataService;
            _mapper = mapper;
            _alertDataService = alertDataService;
            _invalidSensorsReadingDataService = invalidSensorsReadingDataService;
        }

        public DataGenericResponse<List<SensorReadingsResponseViewModel>> GetSensorReadings(Guid deviceId, DateTime? from, DateTime? to, int pageNumber = 1, int pageSize = 10)
        {
            DataGenericResponse<List<SensorReadingsResponseViewModel>> response = new DataGenericResponse<List<SensorReadingsResponseViewModel>>();
            IEnumerable<SensorsReading> query = _sensorsReadingDataService.GetSensorReadings(c => c.DeviceId == deviceId);

            if (from.HasValue && to.HasValue)
            {
                query = query.Where(c => c.RecordedAt >= from.Value.ToUniversalTime() && c.RecordedAt <= to.Value.ToUniversalTime());
            }
            else if (from.HasValue)
            {
                query = query.Where(c => c.RecordedAt >= from.Value.ToUniversalTime());
            }
            else if (to.HasValue)
            {
                query = query.Where(c => c.RecordedAt <= to.Value.ToUniversalTime());
            }

            query = query.Skip((pageSize - 1) * pageNumber).Take(pageSize);

            if (!query.Any())
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, new List<SensorReadingsResponseViewModel>());
            }

            var readingsList = query.ToList();

            List<SensorReadingsResponseViewModel> mappedReadings = _mapper.Map<List<SensorReadingsResponseViewModel>>(readingsList);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, mappedReadings);
        }

        public GenericResponse CreateInvalidReading(CreateInvalidSensorReadingRequest request)
        {
            GenericResponse response = new GenericResponse();
            CreateInvalidSensorReadingValidator validator = new CreateInvalidSensorReadingValidator();

            if (request == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_REQUEST);
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return response.CreateFailureResponse(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
            }

            if (request.Data.Length > 500)
            {
                request.Data = request.Data.Substring(0, 500);
            }

            InvalidSensorReading reading = _mapper.Map<InvalidSensorReading>(request);

            _invalidSensorsReadingDataService.CreateInvalidReading(reading);

            int deviceInvalidReadingsCount = _invalidSensorsReadingDataService.GetInvalidReadings(c => c.DeviceId == request.DeviceId).Count();
            if(deviceInvalidReadingsCount > 500)
            {
                bool deviceHasAlert = _alertDataService.GetAlerts(c => c.DeviceId == request.DeviceId && c.Type == Models.Enums.AlertType.InvalidData && c.ResolutionStatus != Models.Enums.AlertResolutionStatus.Resolved).Any();
                if (!deviceHasAlert)
                {
                    _alertDataService.CreateAlert(new Alert
                    {
                        AlertDate=DateTime.UtcNow,
                        DeviceId=request.DeviceId,
                        Message = "Device sending unintelligible data",
                    });
                }
            }

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }
    }
}
