using SmartAC.Models.Data.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Data
{
    public interface ISensorsReadingDataService
    {
        public void CreateDevice(SensorsReading device);
        public void EditDevice(SensorsReading device);
        public SensorsReading GetDeviceById(Guid id);
        public IEnumerable<SensorsReading> GetDevices(Expression<Func<SensorsReading, bool>> expression);
    }
}
