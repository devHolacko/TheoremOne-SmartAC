using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Requests.Devices
{
    public class RegisterDeviceRequest
    {
        public string Serial { get; set; }
        public string Secret { get; set; }
    }
}
