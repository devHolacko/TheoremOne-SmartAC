using SmartAC.Models.Data.Base;
using SmartAC.Models.Data.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Sensors
{
    public class InvalidSensorReading : BaseAuditedEntity
    {
        public Guid DeviceId { get; set; }
        public string Data { get; set; }

        public virtual Device Device { get; set; }
    }
}
