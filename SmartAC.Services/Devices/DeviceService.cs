using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
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
        private readonly IDeviceDataService _deviceService;
        public DeviceService(IDeviceDataService deviceDataService)
        {
            _deviceService = deviceDataService;
        }

        public GenericResponse Register(RegisterDeviceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
