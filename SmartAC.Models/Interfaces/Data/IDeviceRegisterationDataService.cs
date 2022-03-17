using SmartAC.Models.Data.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Data
{
    public interface IDeviceRegisterationDataService
    {
        public void CreateDevice(DeviceRegisteration device);
        public void EditDevice(DeviceRegisteration device);
        public DeviceRegisteration GetDeviceById(Guid id);
        public IEnumerable<DeviceRegisteration> GetDevices(Expression<Func<DeviceRegisteration, bool>> expression);
    }
}
