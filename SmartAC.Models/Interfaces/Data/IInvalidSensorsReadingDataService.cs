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
        public void CreateInvalidReading(InvalidSensorReading reading);
        public void EditInvalidReading(InvalidSensorReading reading);
        public InvalidSensorReading GetInvalidReadingById(Guid id);
        public IEnumerable<InvalidSensorReading> GetInvalidReadings(Func<InvalidSensorReading, bool> expression);
    }
}
