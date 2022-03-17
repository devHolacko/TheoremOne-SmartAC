using SmartAC.Models.Data.Base;
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
    }
}
