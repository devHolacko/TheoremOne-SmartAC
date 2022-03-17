using SmartAC.Models.Data.Base;
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
        public string Message { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public DateTime AlertDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string ViewStatus { get; set; }
        public string ResolutionStatus { get; set; }
    }
}
