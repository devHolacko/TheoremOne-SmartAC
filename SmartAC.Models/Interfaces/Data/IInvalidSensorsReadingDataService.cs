using SmartAC.Models.Data.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Data
{
    public interface IInvalidSensorsReadingDataService
    {
        public void CreateDevice(InvalidSensorReading device);
        public void EditDevice(InvalidSensorReading device);
        public InvalidSensorReading GetDeviceById(Guid id);
        public IEnumerable<InvalidSensorReading> GetDevices(Expression<Func<InvalidSensorReading, bool>> expression);
    }
}
