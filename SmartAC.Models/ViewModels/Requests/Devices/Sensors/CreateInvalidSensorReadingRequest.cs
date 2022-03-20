using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Requests.Devices.Sensors
{
    public class CreateInvalidSensorReadingRequest
    {
        public Guid DeviceId { get; set; }
        public string Data { get; set; }
    }
}
