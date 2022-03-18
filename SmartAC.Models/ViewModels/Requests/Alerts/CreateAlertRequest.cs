using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Requests.Alerts
{
    public class CreateAlertRequest
    {
        public Guid DeviceId { get; set; }
        public Guid SensorReadingId { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }
        public string Code { get; set; }
        public DateTime AlertDate { get; set; }
    }
}
