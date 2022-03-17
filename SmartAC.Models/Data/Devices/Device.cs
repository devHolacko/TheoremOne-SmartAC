using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Base;
using SmartAC.Models.Data.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Devices
{
    public class Device : BaseAuditedEntity
    {
        public string Serial { get; set; }
        public string Secret { get; set; }

        public virtual IEnumerable<Alert> Alerts { get; set; }
        public virtual IEnumerable<DeviceRegisteration> DeviceRegisterations { get; set; }
        public virtual IEnumerable<SensorsReading> DeviceSensorReadings { get; set; }
        public virtual IEnumerable<InvalidSensorReading> DeviceInvalidReadings { get; set; }
    }
}
