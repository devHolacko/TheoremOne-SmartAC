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
        public void CreateDeviceRegisteration(DeviceRegisteration deviceRegisteration);
        public void EditDeviceRegisteration(DeviceRegisteration deviceRegisteration);
        public DeviceRegisteration GetDeviceRegisterationById(Guid id);
        public IEnumerable<DeviceRegisteration> GetDeviceRegisterations(Func<DeviceRegisteration, bool> expression);
    }
}
