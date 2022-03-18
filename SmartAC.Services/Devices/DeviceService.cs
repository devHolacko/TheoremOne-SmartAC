using SmartAC.Models.Consts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.DeviceRegisterations;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;
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
        public DeviceService(IDeviceDataService deviceDataService,IDeviceRegisterationDataService deviceRegisterationDataService)
        {
            _deviceDataService = deviceDataService;
            _deviceRegisterationDataService = deviceRegisterationDataService;
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

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }
    }
}
