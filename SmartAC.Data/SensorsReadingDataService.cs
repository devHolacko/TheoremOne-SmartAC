using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Data;

namespace SmartAC.Data
{
    public class SensorsReadingDataService : ISensorsReadingDataService
    {
        private readonly IGenericRepository<SensorsReading> _sensorReadingRepository;
        public SensorsReadingDataService(IGenericRepository<SensorsReading> sensorReadingRepository)
        {
            _sensorReadingRepository = sensorReadingRepository;

        }
        public void CreateSensorReading(SensorsReading reading)
        {
            _sensorReadingRepository.Insert(reading);
        }

        public void EditSensorReading(SensorsReading reading)
        {
            _sensorReadingRepository.Update(reading);
        }

        public SensorsReading GetSensorReadingById(Guid id)
        {
            return _sensorReadingRepository.Get(id);
        }

        public IEnumerable<SensorsReading> GetSensorReadings(Func<SensorsReading, bool> expression)
        {
            return _sensorReadingRepository.GetAll().Where(expression);
        }
    }
}
