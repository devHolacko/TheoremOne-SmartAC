using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Sensors
{
    public class SensorsReadingService: ISensorsReadingService
    {
        private readonly ISensorsReadingDataService _sensorsReadingDataService;
        public SensorsReadingService(ISensorsReadingDataService sensorsReadingDataService)
        {
            _sensorsReadingDataService = sensorsReadingDataService;
        }
    }
}
