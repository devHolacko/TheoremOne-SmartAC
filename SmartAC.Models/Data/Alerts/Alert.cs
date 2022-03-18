using SmartAC.Models.Data.Base;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Alerts
{
    public class Alert : BaseAuditedEntity
    {
        public Guid DeviceId { get; set; }
        public Guid SensorReadingId { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }
        public string Code { get; set; }
        public DateTime AlertDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public AlertViewStatus ViewStatus { get; set; }
        public AlertResolutionStatus ResolutionStatus { get; set; }

        public virtual Device Device { get; set; }
        public virtual SensorsReading SensorsReading { get; set; }
    }
}
