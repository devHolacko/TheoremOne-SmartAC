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
    public class DeviceDataService : IDeviceDataService
    {
        private readonly IGenericRepository<Device> _devicesRepository;
        public DeviceDataService(IGenericRepository<Device> devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }
        public void CreateDevice(Device device)
        {
            _devicesRepository.Insert(device);
        }

        public void EditDevice(Device device)
        {
            _devicesRepository.Update(device);
        }

        public Device GetDeviceById(Guid id)
        {
            return _devicesRepository.Get(id);
        }

        public IEnumerable<Device> GetDevices(Func<Device, bool> expression)
        {
            return _devicesRepository.GetAll().Where(expression);
        }
    }
}
