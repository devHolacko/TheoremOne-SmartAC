using SmartAC.Models.Data.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Data
{
    public interface IDeviceDataService
    {
        public void CreateDevice(Device device);
        public void EditDevice(Device device);
        public Device GetDeviceById(Guid id);
        public IEnumerable<Device> GetDevices(Func<Device, bool> expression);
    }
}
