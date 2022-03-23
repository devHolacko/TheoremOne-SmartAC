using SmartAC.Models.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Devices
{
    public class DeviceRegisteration : BaseAuditedEntity
    {
        public Guid DeviceId { get; set; }
        public string FirmwareVersion { get; set; }

        public virtual Device Device { get; set; }

    }
}
