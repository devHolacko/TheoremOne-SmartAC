using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAC.Models.Interfaces.Common;

namespace SmartAC.Data
{
    public class InvalidSensorsReadingDataService : IInvalidSensorsReadingDataService
    {
        private readonly IGenericRepository<InvalidSensorReading> _invalidSensorReadingRepository;
        public InvalidSensorsReadingDataService(IGenericRepository<InvalidSensorReading> invalidSensorReadingRepository)
        {
            _invalidSensorReadingRepository = invalidSensorReadingRepository;

        }
        public void CreateInvalidReading(InvalidSensorReading reading)
        {
            _invalidSensorReadingRepository.Insert(reading);
        }

        public void EditInvalidReading(InvalidSensorReading reading)
        {
            _invalidSensorReadingRepository.Update(reading);
        }

        public InvalidSensorReading GetInvalidReadingById(Guid id)
        {
            return _invalidSensorReadingRepository.Get(id);
        }

        public IEnumerable<InvalidSensorReading> GetInvalidReadings(Func<InvalidSensorReading, bool> expression)
        {
            return _invalidSensorReadingRepository.GetAll().Where(expression);
        }
    }
}
