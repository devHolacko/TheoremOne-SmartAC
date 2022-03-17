using SmartAC.Models.Data.Devices;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Data
{
    public class DeviceRegisterationDataService : IDeviceRegisterationDataService
    {
        private readonly IGenericRepository<DeviceRegisteration> _deviceRegisterationRepository;
        public DeviceRegisterationDataService(IGenericRepository<DeviceRegisteration> deviceRegisterationRepository)
        {
            _deviceRegisterationRepository = deviceRegisterationRepository;
        }
        public void CreateDeviceRegisteration(DeviceRegisteration deviceRegisteration)
        {
            _deviceRegisterationRepository.Insert(deviceRegisteration);
        }

        public void EditDeviceRegisteration(DeviceRegisteration deviceRegisteration)
        {
            _deviceRegisterationRepository.Update(deviceRegisteration);
        }

        public DeviceRegisteration GetDeviceRegisterationById(Guid id)
        {
            return _deviceRegisterationRepository.Get(id);
        }

        public IEnumerable<DeviceRegisteration> GetDeviceRegisterations(Func<DeviceRegisteration, bool> expression)
        {
            return _deviceRegisterationRepository.GetAll().Where(expression);
        }
    }
}
