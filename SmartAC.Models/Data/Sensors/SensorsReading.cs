using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Base;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Sensors
{
    public class SensorsReading : BaseAuditedEntity
    {
        public Guid DeviceId { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal CarbonMonoxide { get; set; }
        public DeviceHealthStatus HealthStatus { get; set; }
        public DateTime RecordedAt { get; set; }

        public virtual Device Device { get; set; }
        public virtual IEnumerable<Alert> Alerts { get; set; }
    }
}
