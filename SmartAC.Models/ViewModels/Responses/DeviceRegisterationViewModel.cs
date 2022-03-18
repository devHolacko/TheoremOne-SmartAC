using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses
{
    public class DeviceRegisterationViewModel
    {
        public Guid RegisterationId { get; set; }
        public Guid DeviceId { get; set; }
        public string Serial { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
