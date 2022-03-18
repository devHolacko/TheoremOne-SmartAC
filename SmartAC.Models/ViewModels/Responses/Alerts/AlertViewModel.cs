using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses.Alerts
{
    public class AlertViewModel
    {
        public string DeviceSerial { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }
        public string Code { get; set; }
        public DateTime AlertDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public AlertViewStatus ViewStatus { get; set; }
        public AlertResolutionStatus ResolutionStatus { get; set; }
    }
}
