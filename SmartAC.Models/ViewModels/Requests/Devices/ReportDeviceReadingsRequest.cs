using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Requests.Devices
{
    public class ReportDeviceReadingsRequest
    {
        public Guid DeviceId { get; set; }
        public List<SensorReadingViewModel> Readings { get; set; }
    }
}
