using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Requests.Devices.Sensors
{
    public class SensorReadingViewModel
    {
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal CarbonMonoxide { get; set; }
        public DeviceHealthStatus HealthStatus { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
