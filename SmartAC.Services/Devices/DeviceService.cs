using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SmartAC.Common.Token;
using SmartAC.Models.Common;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.DeviceRegisterations;
using SmartAC.Models.Validations.Devices;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Devices
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceDataService _deviceDataService;
        private readonly IDeviceRegisterationDataService _deviceRegisterationDataService;
        private readonly ISensorsReadingDataService _sensorsReadingDataService;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public DeviceService(IConfiguration configuration, IDeviceDataService deviceDataService, IDeviceRegisterationDataService deviceRegisterationDataService, ISensorsReadingDataService sensorsReadingDataService, IMapper mapper, ICacheManager cacheManager)
        {
            _deviceDataService = deviceDataService;
            _deviceRegisterationDataService = deviceRegisterationDataService;
            _sensorsReadingDataService = sensorsReadingDataService;
            _cacheManager = cacheManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public GenericResponse Register(RegisterDeviceRequest request)
        {
            GenericResponse response = new GenericResponse();
            RegisterDeviceValidator validator = new RegisterDeviceValidator();

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return response.CreateFailureResponse(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
            }

            Device selectedDevice = _deviceDataService.GetDevices(c => c.Serial == request.Serial).FirstOrDefault();
            if (selectedDevice == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.NOT_FOUND);
            }

            if (selectedDevice.Secret != request.Secret)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_SECRET_SERIAL);
            }

            DeviceRegisteration registeration = new DeviceRegisteration(selectedDevice.Id, request.FirmwareVersion);

            _deviceRegisterationDataService.CreateDeviceRegisteration(registeration);

            string jwtToken = TokenHelper.GenerateJwtToken(_configuration.GetSection("AppSettings")["Secret"], registeration.Id, CommonConsts.ISSUER_DEVICES_API);

            _cacheManager.Add(CommonConsts.TOKEN, jwtToken);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }

        public GenericResponse ReportDeviceReadings(ReportDeviceReadingsRequest request)
        {
            GenericResponse response = new GenericResponse();
            ReportDeviceReadingsValidator validator = new ReportDeviceReadingsValidator();
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
                return response.CreateFailureResponse(ErrorCodesConsts.NOT_FOUND);
            }

            List<SensorsReading> readings = _mapper.Map<List<SensorsReading>>(request.Readings);

            int savedReadings = _sensorsReadingDataService.CreateBulkSensorReadings(readings);

            if (savedReadings == readings.Count)
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
            }

            return response.CreateFailureResponse(ErrorCodesConsts.NOT_ALL_READINGS_SAVED);
        }

        public DataGenericResponse<List<DeviceRegisterationViewModel>> GetRecentlyRegisteredDevices(int pageNumber = 1, int pageSize = 10)
        {
            DataGenericResponse<List<DeviceRegisterationViewModel>> response = new DataGenericResponse<List<DeviceRegisterationViewModel>>();

            string token = _cacheManager.Get(CommonConsts.TOKEN);

            DateTime issuedAt = TokenHelper.GetTokenIssuedAt(token);

            IEnumerable<DeviceRegisteration> query = _deviceRegisterationDataService.GetDeviceRegisterations(c => c.CreatedOn >= issuedAt);
            if (!query.Any())
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, new List<DeviceRegisterationViewModel>());
            }

            List<DeviceRegisteration> devicesRegisterations = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderByDescending(c => c.CreatedOn).ToList();

            List<DeviceRegisterationViewModel> registerationsVM = _mapper.Map<List<DeviceRegisterationViewModel>>(devicesRegisterations);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, registerationsVM);
        }

        public DataGenericResponse<List<DeviceViewModel>> FilterDevicesBySerial(string serialNumber)
        {
            DataGenericResponse<List<DeviceViewModel>> response = new DataGenericResponse<List<DeviceViewModel>>();
            if (string.IsNullOrEmpty(serialNumber))
            {
                return response.CreateFailureResponse(ErrorCodesConsts.NOT_FOUND);
            }

            List<Device> device = _deviceDataService.GetDevices(c => c.Serial.Contains(serialNumber)).ToList();
            List<DeviceViewModel> mappedDevice = _mapper.Map<List<DeviceViewModel>>(device);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, mappedDevice);
        }

        public DataGenericResponse<List<DeviceViewModel>> GetDevicesByRegisterationDate(DateTime from, DateTime to)
        {
            DataGenericResponse<List<DeviceViewModel>> response = new DataGenericResponse<List<DeviceViewModel>>();

            var devices = new List<(Device Device, DateTime RegisteredOn)>();

            var query = _deviceRegisterationDataService.GetDeviceRegisterations(c => c.CreatedOn >= from && c.CreatedOn <= to).OrderByDescending(c => c.CreatedOn);
            var queryResult = query.ToList();
            foreach (var item in queryResult)
            {
                if (!devices.Any(c => c.Device.Id == item.DeviceId))
                {
                    devices.Add((item.Device, item.CreatedOn));
                }
            }

            List<DeviceViewModel> mappedDevices = _mapper.Map<List<DeviceViewModel>>(devices.Select(c => c.Device));

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, mappedDevices);
        }
    }
}
