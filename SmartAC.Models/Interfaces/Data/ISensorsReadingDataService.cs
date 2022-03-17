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
        public void CreateSensorReading(SensorsReading reading);
        public void EditSensorReading(SensorsReading reading);
        public SensorsReading GetSensorReadingById(Guid id);
        public IEnumerable<SensorsReading> GetSensorReadings(Func<SensorsReading, bool> expression);
    }
}
